using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RivalScore : MonoBehaviour
{
    private int score;
    //public TextMeshProUGUI scoreText;
    public TextMeshPro scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + score;
    }
    public void scoreUp(int up)
    {
        score += up;
    }
}
