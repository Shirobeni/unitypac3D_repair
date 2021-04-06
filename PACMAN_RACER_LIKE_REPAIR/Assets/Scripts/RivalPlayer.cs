using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class RivalPlayer : MonoBehaviour
{
    protected float speed = 20f;
    private Rigidbody rigidbody;
    private Vector3 playerPos;
    private Vector3 esaPos;
    private Vector3 enemyPos;
    private GameObject esa;
    private GameObject nearEsa;
    private GameObject enemy;
    public GameObject rivalPlayerMiss;
    private GameObject rivalSet;
    private Vector3 rivalSetPos;
    protected Transform target;
    private float distance;
    protected NavMeshAgent m_navMeshAgent;
    public float moveSpeed { set; get; }
    static public RivalPlayer instance;

    private float limitRange = 30f;
    public bool tracking;

    public int destinationIndex = 0;
    public Transform[] navPointsObj;

    private float searchTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        tracking = false;
        StartCoroutine("awaken");
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        if ((SceneManager.GetActiveScene().name == "Stage1") || (SceneManager.GetActiveScene().name == "Stage2"))
        {
            NewTarget();
            Debug.Log("Start : " + nearEsa +  " esaPos:" + esaPos + "m_navMeshAgent.destination" + m_navMeshAgent.destination);
            m_navMeshAgent.autoBraking = false;
            /*if (GameObject.FindWithTag("Enemy"))
            {
                enemy = GameObject.FindWithTag("Enemy");
                enemyPos = enemy.transform.position;
                distance = Vector3.Distance(playerPos, enemyPos);
            }*/
            /*if(navPointsObj.Length == 0)
            {
                return;
            }*/
            /*            if ((tracking) && (GameObject.FindWithTag("Enemy")))
                        {
                            if (distance > limitRange)
                            {
                                tracking = false;
                            }
                            Vector3 dir = playerPos - enemyPos;
                            Vector3 pos = playerPos - dir * 1.0f;
                            //GetNextPoint();
                            //m_navMeshAgent.destination = esaPos;
                            m_navMeshAgent.destination = pos;
                        }
                        else
                        {
                            if (distance < limitRange)
                            {
                                tracking = true;
                            }
                            //esa = GameObject.FindWithTag("Esa");
                            esaPos = nearEsa.transform.position;
                            m_navMeshAgent.destination = esaPos;
                            /*if (!m_navMeshAgent.pathPending && m_navMeshAgent.remainingDistance < 1f)
                            {
                                GetNextPoint();
                            }*/
            //}
        }
        else if ((SceneManager.GetActiveScene().name == "Clear") || (SceneManager.GetActiveScene().name == "StartStandby"))
        {
            //m_navMeshAgent.enabled = false;
            rivalSet = GameObject.FindWithTag("RivalSet");
            rivalSetPos = rivalSet.transform.position;
            //m_navMeshAgent.enabled = true;
            //Debug.Log(rivalSetPos);
            m_navMeshAgent.SetDestination(rivalSetPos);
            rigidbody.velocity = m_navMeshAgent.desiredVelocity;
            m_navMeshAgent.autoBraking = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = speed;
        playerPos = this.transform.position;
        m_navMeshAgent.speed = moveSpeed;
        rigidbody.velocity = m_navMeshAgent.desiredVelocity;
        if ((SceneManager.GetActiveScene().name == "Stage1") || (SceneManager.GetActiveScene().name == "Stage2"))
        {
            searchTime += Time.deltaTime;
            /*if (searchTime >= 0.3f)
            {
                nearEsa = searchTag(gameObject, "Esa");
                searchTime = 0;
            }*/

            /*esa = GameObject.FindWithTag("Esa");
            esaPos = esa.transform.position;*/
            if (GameObject.FindWithTag("Enemy"))
            {
                enemy = GameObject.FindWithTag("Enemy");
                enemyPos = enemy.transform.position;
                distance = Vector3.Distance(playerPos, enemyPos);
            }
            /*if(navPointsObj.Length == 0)
            {
                return;
            }*/
            if ((tracking)&&(GameObject.FindWithTag("Enemy")))
            {
                if(distance > limitRange)
                {
                    tracking = false;
                }
                Vector3 dir = playerPos - enemyPos;
                Vector3 pos = playerPos + dir * 2.0f;
                m_navMeshAgent.destination = pos;
            }
            else 
            {
                if(distance < limitRange)
                {
                    tracking  = true;
                }
                if (searchTime >= 0.3f)
                {
                    NewTarget();
                    searchTime = 0;
                    Debug.Log("Update : " + nearEsa + " esaPos:" + esaPos + "m_navMeshAgent.destination" + m_navMeshAgent.destination);
                }

                /*if (!m_navMeshAgent.pathPending && m_navMeshAgent.remainingDistance < 1f)
                {
                    GetNextPoint();
                }*/
            }


        }
        else if ((SceneManager.GetActiveScene().name == "Clear")||(SceneManager.GetActiveScene().name == "StartStandby"))
        {
            //m_navMeshAgent.enabled = false;
            rivalSet = GameObject.FindWithTag("RivalSet");
            rivalSetPos = rivalSet.transform.position;
            //m_navMeshAgent.enabled = true;
            //Debug.Log(rivalSetPos);
            m_navMeshAgent.SetDestination(rivalSetPos);
            rigidbody.velocity = m_navMeshAgent.desiredVelocity;
        }

    }

    public GameObject searchTag(GameObject nowObj,string tagName)
    {
        float tmpDis = 0;
        float nearDis = 0;
        GameObject targetObj = null;
        foreach(GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            if(nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                targetObj = obs;
            }
        }
        return targetObj;
    }

    private void NewTarget()
    {
        nearEsa = searchTag(gameObject, "Esa");
        esaPos = nearEsa.transform.position;
        m_navMeshAgent.destination = esaPos;
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
        if ((collision.gameObject.tag == "Enemy") && (this.gameObject.tag == "Player"))
        {
            Destroy(gameObject);
            Instantiate(rivalPlayerMiss, transform.position, transform.rotation);
        }
        /*if (collision.gameObject.tag == "Esa")
        {
            Invoke("NewTarget", 0.05f);
        }*/
    }
    /*void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.tag == "Enemy") && (this.gameObject.tag == "Player"))
        {
            Destroy(gameObject);
            Instantiate(rivalPlayerMiss, transform.position, transform.rotation);
        }
    }*/
    void OnDrawGizmos()
    {
        if (m_navMeshAgent && m_navMeshAgent.enabled)
        {
            Gizmos.color = Color.green;
            var prefPos = transform.position;
            foreach (var playerPos in m_navMeshAgent.path.corners)
            {
                Gizmos.DrawLine(prefPos, playerPos);
                prefPos = playerPos;
            }
            Gizmos.DrawWireSphere(transform.position, limitRange);
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

    IEnumerator awaken()
    {
        this.gameObject.tag = "PacMiss";
        int count = 10;
        for (int i = 0; i < count; i++) {
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
