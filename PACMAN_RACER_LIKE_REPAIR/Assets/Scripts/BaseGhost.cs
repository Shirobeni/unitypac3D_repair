using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class BaseGhost : MonoBehaviour
{
    protected float speed = 6f;

    protected Vector3 enemypos;
    protected Rigidbody rigidbody;
    protected Vector3 playerPos;
    protected GameObject player;
    protected NavMeshAgent m_navMeshAgent;
    protected Transform target;
    protected float distance;
    public bool tracking;

    public float moveSpeed { set; get; }
    public bool ijike = false;
    public float ijikeTime;
    public int destinationIndex = 0;
    protected new Collider collider;

    public GhostS ghostS;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        ghostS.limitRange = 20f;
        ghostS.quitRange = 30f;
        tracking = false;
        rigidbody = GetComponent<Rigidbody>();
        ijikeTime = 0f;
        m_navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_navMeshAgent.autoBraking = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        moveSpeed = speed;
        m_navMeshAgent.speed = moveSpeed;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Player") && (this.gameObject.tag == "Ijike"))
        {
            speed = 0f;
            StartCoroutine("Eaten");
        }
    }

    public virtual void beIjike()
    {
        ijikeTime = 0f;
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ijike");
        this.gameObject.layer = LayerMask.NameToLayer("Ijike");
        transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 0f, 255f);
        ijike = true;
        this.gameObject.tag = "Ijike";
        transform.GetChild(0).gameObject.tag = "Ijike";
    }
}
