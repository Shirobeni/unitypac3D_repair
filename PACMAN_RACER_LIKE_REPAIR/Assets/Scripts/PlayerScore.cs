using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    private int score;
    //private GameObject textObject;
    //public TextMeshProUGUI scoreText;
    public TextMeshPro scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //this.scoreText = this.GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //textObject = GameObject.FindWithTag("1pScore");
        scoreText.text = ""+score;
    } 
    public void scoreUp(int up)
    {
        score += up;
    }
}
