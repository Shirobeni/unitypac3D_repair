using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class GhostBlue : BaseGhost
{
    public Transform[] navPointsObj;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        m_navMeshAgent.autoBraking = false;
        GetNextPoint();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        enemypos = this.transform.position;
        //renderer = GetComponent<Renderer>();
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        distance = Vector3.Distance(enemypos, playerPos);
        /*if(navPointsObj.Length == 0)
        {
            return;
        }*/
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
                transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(0f, 1f, 255f);
                //rigidbody.velocity = m_navMeshAgent.desiredVelocity;
            }
            else if (ijike == true)
            {
                //ijikeTime = 0f;
                ijikeTime += Time.deltaTime;
                Vector3 dir = enemypos - playerPos;
                Vector3 pos = enemypos + dir * 1.0f;
                m_navMeshAgent.destination = pos;
                //rigidbody.velocity = m_navMeshAgent.desiredVelocity;
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
        if(ijike == false)
        {
            ijikeTime = 0f;
        }else if(ijike == true)
        {
            ijikeTime += Time.deltaTime;
            if ((ijikeTime > 5.0f) && (ijikeTime < 7.0f))
            {
                StartCoroutine("mahi");
            }
            else if (ijikeTime > 7.0f)
            {
                ijike = false;
                this.gameObject.tag = "Enemy";
                transform.GetChild(0).gameObject.tag = "Enemy";
                gameObject.layer = LayerMask.NameToLayer("Enemy");
                transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Enemy");
                transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0f, 1f, 255f);
            }
        }
       

    }

   

    void OnDrawGizmos()
    {
        if (m_navMeshAgent && m_navMeshAgent.enabled)
        {
            Gizmos.color = Color.blue;
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

    void GetNextPoint()
    {
        if(navPointsObj.Length == 0)
        {
            return;
        }
        m_navMeshAgent.destination = navPointsObj[destinationIndex].position;
        destinationIndex = (destinationIndex + 1) % navPointsObj.Length;
    }

    IEnumerator mahi()
    {
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(0f, 0f, 255f);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(0f, 1f, 255f);
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator Eaten()
    {
        Destroy(gameObject);
        yield return null;
        /*this.gameObject.tag = "EatenIjike";
        transform.GetChild(0).gameObject.tag = "EatenIjike";
        gameObject.layer = LayerMask.NameToLayer("EatenIjike");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("EatenIjike");
        int count = 10;
        collider = transform.GetChild(0).GetComponent<Collider>();
        collider.isTrigger = true;
        while (count > 0)
        {
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.5f);
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.5f);
            count--;
        }
        collider.isTrigger = false;
        speed = 5f;
        this.gameObject.tag = "Enemy";
        transform.GetChild(0).gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Enemy");
        ijikeTime = 7.0f;*/
    }
}
