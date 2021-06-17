using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    private static int score;
    private static int totalScore;
    public TextMeshPro scoreText;
    // Start is called before the first frame update
    void Start()
    {
        if ((SceneManager.GetActiveScene().name == "Stage1") || 
            (SceneManager.GetActiveScene().name == "Stage2") ||
            (SceneManager.GetActiveScene().name == "Stage3") ||
            (SceneManager.GetActiveScene().name == "Goal"))
        {
            score = 0;
        }
        else if (SceneManager.GetActiveScene().name == "Title")
        {
            totalScore = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.scoreText = this.GetComponent<TextMeshPro>();
        if (SceneManager.GetActiveScene().name == "Goal")
        {
            scoreText.text = "" + totalScore;
        }
        else
        {
            scoreText.text = "" + score;
        }
    } 

    public void scoreUp(int up)
    {
        score += up;
        totalScore += up;
    }
}
