using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalPlayerMiss : MonoBehaviour
{
    public GameObject player;
    private float awakeTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        awakeTime += Time.deltaTime;
        if (awakeTime > 1.0f)
        {
            Destroy(gameObject);
            GameObject newRival = Instantiate(player, transform.position, transform.rotation);
            newRival.name = player.name;
            //Instantiate(player, transform.position, transform.rotation);
        }
    }
}
