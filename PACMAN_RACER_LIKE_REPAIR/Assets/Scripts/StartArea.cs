using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartArea : MonoBehaviour
{
    public bool readyFlag;
    // Start is called before the first frame update
    void Start()
    {
        readyFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            readyFlag = true;
        }
    }
}
