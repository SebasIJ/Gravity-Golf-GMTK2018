using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] private Transform finishParticleSystem;
    [SerializeField] private Transform holeComplete;
    [SerializeField] private Transform fadeDiamond;
    [SerializeField] private Transform swapFX;

    [SerializeField] private Transform jumpAudio;
    [SerializeField] private Transform loseAudio;
    [SerializeField] private Transform collideAudio;

    [SerializeField] private float collideMinimumPitch;

    private AudioSource collideAudioSource;

    private Transform canvas;
    private Score score;
    private Restart restart;

    //[SerializeField] private string nextScene;

    private Rigidbody2D rb2d;

    private Animator anim;

    [HideInInspector] public bool canAct = true;

    [HideInInspector] public int strokes;


    

	private void Start () {

        //finds objects and components and assigns them to their variables
        rb2d = GetComponent<Rigidbody2D>();
        canvas = FindObjectOfType<Canvas>().transform;
        score = FindObjectOfType<Score>();
        restart = FindObjectOfType<Restart>();
        anim = GetComponent<Animator>();


        collideAudioSource = collideAudio.GetComponent<AudioSource>();

        //instantiates the fade effect
        Instantiate(fadeDiamond, Camera.main.transform);


	}
	

	private void Update () {
		
        //checks for the jump buttons and if the player can act
        if ( (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) && canAct && /*disables when paused*/ !FindObjectOfType<Restart>().isPaused) {

            //flips the gravity scale
            rb2d.gravityScale = -rb2d.gravityScale;

            //adds one stroke to the score
            strokes++;

            //updates the score
            score.UpdateScore(strokes);

            //plays jump sound
            jumpAudio.GetComponent<AudioSource>().Play();

            //plays animations and effects
            Instantiate(swapFX, transform.position, Quaternion.Euler(Vector3.zero), null);
            anim.SetTrigger("swap");
        }


        //New dash ability
        if(canAct && Input.GetKeyDown(KeyCode.X) && !FindObjectOfType<Restart>().isPaused)
        {
            //float to contain the last direction the ball was going in
            float direction;

            //does not calculate direction if velocity was 0
            if (rb2d.velocity.x == 0)
            {
                direction = 1;
            }
            else
            {
                //gets direction by dividing x velocity by its absolute value
                direction = rb2d.velocity.x / Mathf.Abs(rb2d.velocity.x);
            }
            
            //Resets velocity
            rb2d.velocity = Vector2.zero;

            //adds impulse force in a horizontal direction
            rb2d.AddForce(Vector2.right * 3 * direction, ForceMode2D.Impulse);

            //adds one stroke to the score
            strokes += 2;

            //updates the score
            score.UpdateScore(strokes);
        }

	}


    private void OnTriggerEnter2D(Collider2D collision) {
        
        //if the ball touches a goal trigger finishes the level
        if (collision.tag == "Finish" && canAct) {

            FinishLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        //if the ball touches a respawn object disables controls and restarts the level
        if (collision.gameObject.tag == "Respawn" && canAct) {
            canAct = false;
            restart.RestartScene();
            loseAudio.gameObject.SetActive(true);
        }

        /*if (Mathf.Abs(rb2d.velocity.x) > 3f || Mathf.Abs(rb2d.velocity.y) > 3f) {

            collideAudio.GetComponent<AudioSource>().Play();
        }*/


        //adjusts pitch and plays collision sound
        collideAudioSource.pitch = (Mathf.Abs(rb2d.velocity.x) + Mathf.Abs(rb2d.velocity.y)) / 10f;

        if (collideAudioSource.pitch > collideMinimumPitch) {
            collideAudioSource.Play();
        }
        



    }

    //when the level finishes
    void FinishLevel() {

        //disables controls
        canAct = false;

        //plays victory effect
        Transform particleSystem = Instantiate(finishParticleSystem, transform.position, Quaternion.Euler(Vector3.zero), null);     //Instantiate Particle System
        particleSystem.GetComponent<FollowObject>().followTransform = this.transform;                                               //Particle system will follow this transform

        Instantiate(holeComplete, canvas);

        //updates total score
        //ScoreCard.TotalScore += strokes;
        //Modified line
        ScoreCard.TotalScore = strokes;

        //prints the current score to the console
        Debug.Log("Score Card: " + ScoreCard.TotalScore);

    }
}
