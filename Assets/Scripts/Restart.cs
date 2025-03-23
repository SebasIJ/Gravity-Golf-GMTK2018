using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour {

    [SerializeField] private Transform fadeDiamond;
    [SerializeField] private float clickRange;

    private Transform score;
    private RectTransform rectTransform;

    //New
    [SerializeField] private GameObject pause;
    [HideInInspector]public bool isPaused = false;

    private void Start() {

        rectTransform = GetComponent<RectTransform>();

        score = FindObjectOfType<Score>().transform;

        //New
        pause.SetActive(false);
    }

    private void Update() {
        
        if (Input.GetMouseButtonDown(0) /* added to prevent resetting after level completion */ && FindObjectOfType<Ball>().canAct) {

            Vector3 mousePos = Input.mousePosition;

            if (Vector3.Distance(mousePos, rectTransform.position) < clickRange) {

                RestartScene();
            }   
            
        }
        else if (Input.GetKeyDown(KeyCode.R) /* added to prevent resetting after level completion */ && FindObjectOfType<Ball>().canAct) {
            RestartScene(); 
        }

        //New Code
        if (Input.GetKeyDown(KeyCode.P) && FindObjectOfType<Ball>().canAct)
        {
            PauseGame();
        }

    }

    public void RestartScene() {

        FindObjectOfType<Ball>().canAct = false;

        Transform fade = Instantiate(fadeDiamond, Camera.main.transform);   //Instantiate fade out
        fade.GetComponent<FadeDiamond>().restartScene = true;               //restartScene = true
        fade.GetComponent<FadeDiamond>().fadeIn = false;                    //fadeIn = false

        score.GetComponent<ScoreFadeIn>().enabled = true;
        score.GetComponent<ScoreFadeIn>().FadeOut();
    }

    //New Code
    public void PauseGame()
    {
        if (isPaused)
        {            
            Time.timeScale = 1;
            pause.SetActive(false);
            isPaused = false;
        }
        else
        {
            pause.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}
