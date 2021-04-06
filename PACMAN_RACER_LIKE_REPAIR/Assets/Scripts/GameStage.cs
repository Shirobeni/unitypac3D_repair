using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameStage : MonoBehaviour
{
    private bool nextStageFlag = false;
    public static int stageCount = 0;
    private GameObject esa;
    private GameObject powerEsa;
    private GameObject item;
    private GameObject playerSet;
    private StartArea startArea;
    private GameObject rivalSet;
    private RivalArea rivalArea;
    private GameObject anaunce;
    private TextMeshPro anaunceText;
    private float startTime;
    private GameObject fadeObject;
    private Fade fade;
    // Start is called before the first frame update
    void Start()
    {
        startTime = 0f;


        if (SceneManager.GetActiveScene().name == "Title")
        {
            stageCount = 0;
        }
        else if (SceneManager.GetActiveScene().name == "Stage1")
        {
            stageCount = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            stageCount = 2;
        }
        Debug.Log("stageCount:" + stageCount);

    }

    // Update is called once per frame
    void Update()
    {
        //fadeObject = GameObject.FindWithTag("FadeScene");

        if (GameObject.FindWithTag("Annaunce"))
        {
            anaunce = GameObject.FindWithTag("Annaunce");
            anaunceText = anaunce.GetComponent<TextMeshPro>();
        }
        if (SceneManager.GetActiveScene().name == "Title")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadScene();
            }
        }
        if (SceneManager.GetActiveScene().name == "StartStandby")
        {
            playerSet = GameObject.FindWithTag("PlayerSet");
            startArea = playerSet.GetComponent<StartArea>();
            rivalSet = GameObject.FindWithTag("RivalSet");
            rivalArea = rivalSet.GetComponent<RivalArea>();
            SceneChange();
        }
        if ((SceneManager.GetActiveScene().name == "Stage1") || (SceneManager.GetActiveScene().name == "Stage2") ||
            (SceneManager.GetActiveScene().name == "Stage3"))
        {
            if (GameObject.FindWithTag("Annaunce"))
            {
                anaunceText.enabled = true;
                anaunceText.text = "START!";
            }
            //anaunceText.enabled = true;
            if (startTime < 3f)
            {
                startTime += Time.deltaTime;
            }
            else if (startTime > 3f)
            {
                if (GameObject.FindWithTag("Annaunce"))
                {
                    anaunceText.enabled = false;
                }
            }
            fadeObject = GameObject.FindWithTag("FadeScene");
            fade = fadeObject.GetComponent<Fade>();
            if (GameObject.FindWithTag("Esa"))
            {
                esa = GameObject.FindWithTag("Esa");
            }
            if (GameObject.FindWithTag("PowerEsa"))
            {
                powerEsa = GameObject.FindWithTag("PowerEsa");
            }
            if (GameObject.FindWithTag("Item"))
            {
                item = GameObject.FindWithTag("Item");
            }
            if ((!esa) && (!powerEsa) && (!item))
            {
                LoadScene();
            }
        }
        if (SceneManager.GetActiveScene().name == "Clear")
        {
            playerSet = GameObject.FindWithTag("PlayerSet");
            startArea = playerSet.GetComponent<StartArea>();
            rivalSet = GameObject.FindWithTag("RivalSet");
            rivalArea = rivalSet.GetComponent<RivalArea>();
            SceneChange();

        }

    }
    public void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            SceneManager.LoadScene("StartStandby");
        }
        if (SceneManager.GetActiveScene().name == "StartStandby")
        {
            SceneManager.LoadScene("Stage1");
        }
        if ((SceneManager.GetActiveScene().name == "Stage1") || (SceneManager.GetActiveScene().name == "Stage2") ||
            (SceneManager.GetActiveScene().name == "Stage3"))
        {
            SceneManager.LoadScene("Clear");
        }
        if (SceneManager.GetActiveScene().name == "Clear")
        {
            if (stageCount == 1)
            {
                SceneManager.LoadScene("Stage2");
            }
            else if (stageCount == 2)
            {
                SceneManager.LoadScene("Stage3");
            }
        }
    }

    public void SceneChange()
    {
        fadeObject = GameObject.FindWithTag("FadeScene");
        fade = fadeObject.GetComponent<Fade>();
        anaunceText.enabled = false;
        if ((startArea.readyFlag == true) && (rivalArea.readyFlag == true))
        {
            anaunceText.enabled = true;
            startTime += Time.deltaTime;
            anaunceText.text = "READY?" + Mathf.CeilToInt(5.0f - startTime);
            //Invoke("LoadScene", 0.05f);
            if (startTime > 5.0f)
            {
                fade.FadeIn(1, () =>
                {
                    Invoke("LoadScene", 0.05f);
                });
                //SceneManager.LoadScene("Stage1");
                anaunceText.enabled = false;
                startTime = 0f;
            }
        }
    }

}
