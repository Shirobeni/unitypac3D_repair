using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class GhostRed : BaseGhost {
    /*
    protected float speed = 5f;

    private Vector3 enemypos;
    protected Rigidbody rigidbody;
    private Vector3 playerPos;
    private GameObject player;
    private NavMeshAgent m_navMeshAgent;
    protected Transform target;
    private float distance;
    public bool tracking;

    public float moveSpeed { set; get; }
    public bool ijike = false;
    public float ijikeTime;
    public int destinationIndex = 0;
    public Transform[] navPointsObj;
    private new Collider collider;

    public GhostS ghostS;
    */

    public Transform[] navPointsObj;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        /*
        ghostS.limitRange = 30f;
        ghostS.quitRange = 40f;
        tracking = false;
        //enemypos = GetComponent<Transform>().position;
        m_navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        ijikeTime = 0f;
        */
        GetNextPoint();
        m_navMeshAgent.autoBraking = false;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        enemypos = this.transform.position;
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        distance = Vector3.Distance(enemypos,playerPos);
        //moveSpeed = speed;
        //m_navMeshAgent.speed = moveSpeed;
        rigidbody.velocity = m_navMeshAgent.desiredVelocity;
        if (tracking)
        {
            if(distance > ghostS.limitRange)
            {
                tracking = false;
            }
            m_navMeshAgent.destination = playerPos;
            if (ijike == false)
            {
                ijikeTime = 0f;
                transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(255f, 0f, 0f);
                //enemypos = n_navMeshAgent.desiredVelocity;
            }
            else if (ijike == true)
            {
                //ijikeTime = 0f;
                ijikeTime += Time.deltaTime;
                Vector3 dir = enemypos - playerPos;
                Vector3 pos = enemypos + dir * 1.0f;
                m_navMeshAgent.destination = pos;
               
            }
        }
        else if(!tracking)
        {
            if(distance < ghostS.quitRange)
            {
                tracking = true;
            }
            if (!m_navMeshAgent.pathPending && m_navMeshAgent.remainingDistance < 1f)
            {
                GetNextPoint();
            }
            
        }
        if (ijike == false)
        {
            ijikeTime = 0f;
        }
        else if (ijike == true)
        {
            //ijikeTime = 0f;
            ijikeTime += Time.deltaTime;
            if ((ijikeTime > 5.0f) && (ijikeTime < 7.0f))
            {
                StartCoroutine("mahi");
            }
            else if (ijikeTime > 7.0f)
            {
                ijike = false;
                //ijikeTime = 0f;
                this.gameObject.tag = "Enemy";
                transform.GetChild(0).gameObject.tag = "Enemy";
                gameObject.layer = LayerMask.NameToLayer("Enemy");
                transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Enemy");
                transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(255f, 0f, 0f);
            }
        }
       


    }



   /* public void beIjike() {
        ijikeTime = 0f;
        this.gameObject.layer = LayerMask.NameToLayer("Ijike");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ijike");
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(0f, 0f,255f);
        ijike = true;
        this.gameObject.tag = "Ijike";
        transform.GetChild(0).gameObject.tag = "Ijike";
    }*/
    /*void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.tag == "Player")&&(this.gameObject.tag == "Ijike"))
        {
            speed = 0f;
            StartCoroutine("Eaten");
        }
    }*/

    void GetNextPoint()
    {
        if(navPointsObj.Length == 0)
        {
            return;
        }
        m_navMeshAgent.destination = navPointsObj[destinationIndex].position;
        //target = navPointsObj[destinationIndex].transform;
        //m_navMeshAgent.SetDestination(target.position);
        destinationIndex = (destinationIndex + 1) % navPointsObj.Length;
    } 

    void OnDrawGizmos()
    {
        if(m_navMeshAgent && m_navMeshAgent.enabled)
        {
            Gizmos.color = Color.red;
            var prefPos = transform.position;
            foreach (var enemypos in m_navMeshAgent.path.corners)
            {
                Gizmos.DrawLine(prefPos, enemypos);
                prefPos = enemypos;
            }
            Gizmos.DrawWireSphere(transform.position, ghostS.limitRange);
            Gizmos.DrawWireSphere(transform.position, ghostS.quitRange);
        }
    }

    IEnumerator mahi()
    {
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(0f, 0f, 255f);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator Eaten()
    {
        Destroy(gameObject);
        yield return null;
        /*transform.GetChild(0).gameObject.tag = "EatenIjike";
        this.gameObject.tag = "EatenIjike";
        gameObject.layer = LayerMask.NameToLayer("EatenIjike");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("EatenIjike");
        collider = transform.GetChild(0).GetComponent<Collider>();
        collider.isTrigger = true;
        int count = 10;
        while (count > 0)
        {
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(1,1,1,1);
            yield return new WaitForSeconds(0.2f);
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(1,1,1,0);
            yield return new WaitForSeconds(0.2f);
            count--;
        }
        yield return new WaitForSeconds(0.2f);
        speed = 5f;
        collider.isTrigger = false;
        this.gameObject.tag = "Enemy";
        transform.GetChild(0).gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Enemy");
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(255f, 0f, 0f);
        ijikeTime = 7.0f;
        */
    }


    



}
