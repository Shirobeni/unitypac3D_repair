using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    private static int score;
    public TextMeshPro scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.scoreText = this.GetComponent<TextMeshPro>();
        //textObject = GameObject.FindWithTag("1pScore");
        scoreText.text = ""+score;
    } 
    public void scoreUp(int up)
    {
        score += up;
    }
}
