using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private float speed =  1.05f;
    private Vector3 velocity;
    private Vector3 playerpos;
    private GameObject esa;
    public GameObject playerMiss;
    private Rigidbody rigidbody;
    private GameObject mainCameraObj;
    static public Player instance;
    // Use this for initialization
    void Start () {
        StartCoroutine("awaken");
        playerpos = GetComponent<Transform>().position;
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        velocity = Vector3.zero;
        Vector2 diff = transform.position - playerpos;
        if (((Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.J))) || (Input.GetKey(KeyCode.UpArrow)))
        {
            velocity = transform.forward * speed;
        }
        else if (((Input.GetKey(KeyCode.X)) && (Input.GetKey(KeyCode.N))) || (Input.GetKey(KeyCode.DownArrow)))
        {
            velocity = transform.forward * -speed;

        }
        else if (((Input.GetKey(KeyCode.X)) && (Input.GetKey(KeyCode.J))) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.Rotate(new Vector3(0, -12, 0));
        }
        else if (((Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.N))) ||(Input.GetKey(KeyCode.RightArrow)))
        {
            transform.Rotate(new Vector3(0, 12, 0));
        }
        //velocity = velocity.normalized * (speed * 2) * Time.deltaTime;

        if(velocity.magnitude > 0)
        {
            transform.position += velocity;
        }
        if(diff.magnitude > 0.01f)
        {
            Quaternion q = Quaternion.LookRotation(diff);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, speed * Time.deltaTime);
        }
        playerpos = transform.position;
	}
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.tag == "Enemy")&&(this.gameObject.tag == "Player"))
        {
            Destroy(gameObject);
            Instantiate(playerMiss, transform.position,transform.rotation);
        }
    }
    IEnumerator awaken()
    {
        this.gameObject.tag = "PacMiss";
        int count = 20;
        for (int i = 0; i < count; i++)
        {
            i++;
            yield return new WaitForSeconds(0.5f);
            if (i > 0)
            {
                this.gameObject.tag = "Player";
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
