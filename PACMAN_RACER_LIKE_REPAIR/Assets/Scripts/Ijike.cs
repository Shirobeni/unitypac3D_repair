using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Ijike : MonoBehaviour
{
    protected float speed = 2f;

    private Vector3 enemypos;
    public GameObject enemy;
    protected Rigidbody rigidbody;
    private Vector3 playerPos;
    private GameObject player;
    private float ijikeTime = 0f;
    protected UnityEngine.AI.NavMeshAgent m_navMeshAgent;

    public float moveSpeed { set; get; }
    // Start is called before the first frame update
    void Start()
    {
        m_navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        moveSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        Vector3 dir = this.transform.position - playerPos;
        Vector3 pos = this.transform.position + dir * 1.0f;
        m_navMeshAgent.SetDestination(pos);
        rigidbody.velocity = m_navMeshAgent.desiredVelocity;
        ijikeTime += Time.deltaTime;
        if(ijikeTime > 7.0f)
        {
            Destroy(gameObject);
            Instantiate(enemy,transform.position,transform.rotation);
        }
    }
}
