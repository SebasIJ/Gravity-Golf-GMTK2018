using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeDiamond : MonoBehaviour
{

    public bool fadeIn;

    private float totalTime;
    [SerializeField] float fadeTime;

    private Camera cameraMain;

    private Vector3 scaleRatio = new Vector3(6f, 5f, 1f);
    private Vector3 scaleZero = new Vector3(0f, 0f, 1f);

    private Vector3 startScale;
    private Vector3 targetScale;

    [SerializeField] public bool restartScene;


    private void Start()
    {
        //gets the camera
        cameraMain = Camera.main;

        //sets start scale and target scale depending on if the scene is fading in or out
        if (fadeIn) {

            startScale = scaleRatio * cameraMain.orthographicSize;

            targetScale = scaleZero;
        }
        else {
            startScale = scaleZero;

            targetScale = scaleRatio * cameraMain.orthographicSize;
        }

        transform.localScale = startScale;

    }

    private void Update()
    {

        totalTime += Time.deltaTime;

        //adjusts the scale over time
        transform.localScale = Vector3.Lerp(startScale, targetScale, totalTime / fadeTime);

        //checks if the scale has reached the target scale
        if (transform.localScale == targetScale)
        {
            //if the scene is fading in destroys the fade object
            if (fadeIn)
            {
                Destroy(gameObject);
            }
            //else if the scene is restarting resets the level
            //otherwise goes to the next level
            else
            {
                if (restartScene)
                {
                    FindObjectOfType<LevelManager>().Reset();
                }
                else
                {
                    FindObjectOfType<LevelManager>().Next();
                }
            }
            
        }

    }

}
