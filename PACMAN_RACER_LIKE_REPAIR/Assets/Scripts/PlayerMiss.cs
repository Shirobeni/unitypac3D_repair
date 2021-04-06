using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiss : MonoBehaviour
{
    public GameObject player;
    private GameObject mainCameraObj;
    private float awakeTime;
    // Start is called before the first frame update
    void Start()
    {
        awakeTime = 0f;
        mainCameraObj = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        awakeTime += Time.deltaTime;
        if(awakeTime > 1.0f)
        {
            Destroy(gameObject);
            //Instantiate(player, transform.position, transform.rotation);
            GameObject newPlayer = Instantiate(player, transform.position, transform.rotation);
            newPlayer.name = player.name;

        }
    }
}
