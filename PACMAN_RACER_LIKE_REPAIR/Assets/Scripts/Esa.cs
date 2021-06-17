using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esa : MonoBehaviour
{
    private Rigidbody rigidbody;
    private GameObject enemy1;
    private GameObject enemy2;
    private GameObject enemy3;
    private GameObject enemy4;
    private GhostRed ghostRed;
    private GhostBlue ghostBlue;
    private GhostPink ghostPink;
    private GhostOrange ghostOrange;
    private GameObject canvasObject;
    private GameObject score1p;
    private PlayerScore playerScoreScript;
    private GameObject score2p;
    private RivalScore rivalScoreScript;
    public EsaS esaS;
    public ItemS itemS;
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        score1p = GameObject.FindWithTag("1pScore");
        playerScoreScript = score1p.GetComponent<PlayerScore>();
        score2p = GameObject.FindWithTag("2pScore");
        rivalScoreScript = score2p.GetComponent<RivalScore>();

    }

    // Update is called once per frame
    void Update()
    {
        enemy1 = GameObject.Find("RedGhostMain");
        if (enemy1)
        {
            ghostRed = enemy1.GetComponent<GhostRed>();
        }
        enemy2 = GameObject.Find("BlueGhostMain");
        if (enemy2)
        {
            ghostBlue = enemy2.GetComponent<GhostBlue>();
        }
        enemy3 = GameObject.Find("PinkGhostMain");
        if (enemy3)
        {
            ghostPink = enemy3.GetComponent<GhostPink>();
        }
        enemy4 = GameObject.Find("OrangeGhostMain");
        if (enemy4) {
            ghostOrange = enemy4.GetComponent<GhostOrange>();
        }
        transform.Rotate(new Vector3(0, 5, 0));
    }
    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Player") || (collision.gameObject.tag == "PacMiss"))
        {
            Destroy(gameObject);
            if (this.gameObject.tag == "PowerEsa")
            {
                ghostRed.beIjike();
                ghostBlue.beIjike();
                ghostPink.beIjike();
                ghostOrange.beIjike();
            }
            if (collision.gameObject.name == "PacmanMain")
            {
                if ((this.gameObject.tag == "Esa") || (this.gameObject.tag == "PowerEsa"))
                {
                    Debug.Log("TouchEsa Player");
                    playerScoreScript.scoreUp(esaS.score);
                }
                else if (this.gameObject.tag == "Item")
                {
                    playerScoreScript.scoreUp(itemS.score);
                }
            }
            else if (collision.gameObject.name == "RivalPacman")
            {
                if ((this.gameObject.tag == "Esa") || (this.gameObject.tag == "PowerEsa"))
                {
                    Debug.Log("TouchEsa Rival");
                    rivalScoreScript.scoreUp(esaS.score);
                }
                else if (this.gameObject.tag == "Item")
                {
                    rivalScoreScript.scoreUp(itemS.score);
                }
            }

        }
        
    }

}
