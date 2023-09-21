using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent EnemyMesh;
    public float walkSpeed;
    public float waitTime;
    public float distance;
    public float FindDistance;
    public float AttDistance;
    public float DisToPlayer;
    public int damage;
    private GameObject Player;
    public float EnemyHP;
    private Animator ani;
    private Rigidbody rig;
    private Coroutine moveCoroutine;
    private GameObject EXP1s;
    public GameObject Exp1;
    //private PlayerController playerHealth;
    // Start is called before the first frame update

    IEnumerator Move()
    {
        while(true)
        {
            yield return null;
            yield return new WaitForSeconds(waitTime);
            EnemyMesh.speed = walkSpeed;
            Vector3 ranMove = new Vector3(Random.Range(this.transform.position.x - distance, this.transform.position.x + distance), this.transform.position.y, Random.Range(this.transform.position.z - distance, this.transform.position.z + distance));
            ani.SetBool("Move", true);
            this.transform.LookAt(ranMove);
            EnemyMesh.SetDestination(ranMove);
            //yield return new WaitForSeconds(Random.Range(0, 5));
            //EnemyMesh.speed = 0;
            //ani.SetBool("Move", false);
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return null;
            if(Player != null)
            {
                if (Vector3.Distance(Player.transform.position, this.transform.position) <= FindDistance)
                {
                    StopCoroutine(moveCoroutine);
                    //this.transform.LookAt(Player.transform.position);
                    Vector3 oldAngle = this.transform.eulerAngles;
                    this.transform.LookAt(Player.transform.transform);
                    float target = this.transform.eulerAngles.y;
                    float rotspeed = 120 * Time.deltaTime;
                    float angle = Mathf.MoveTowardsAngle(oldAngle.y, target, rotspeed);
                    this.transform.eulerAngles = new Vector3(0, angle, 0);
                    EnemyMesh.speed = walkSpeed;
                    EnemyMesh.SetDestination(new Vector3(Player.transform.position.x, 0, Player.transform.position.y));
                    ani.SetBool("Move", true);                    
                    if ((Vector3.Distance(Player.transform.position, this.transform.position) <= AttDistance))
                    {
                        if (Player.GetComponent<PlayerController>() != null)
                        {
                            EnemyMesh.speed = 0;
                            ani.SetBool("Move", false);
                            //Debug.Log(Player.GetComponent<PlayerController>().HP);
                            Player.GetComponent<PlayerController>().GetDamage(damage);
                            yield return new WaitForSeconds(1.0f);
                        }
                    }

                }
            }
        }
    }

    public void Hurt(int damage)
    {
        EnemyHP -= damage;
    }
    private void DesrtoyEnemy()
    {
        if (Player != null)
        {
            if (Vector3.Distance(Player.transform.position, this.transform.position) > DisToPlayer || EnemyHP <= 0)
            //if((Player.transform.position - this.transform.position).magnitude > DisToPlayer)
            {
                Vector3 newPos = new Vector3(this.transform.position.x, 0.4f , this.transform.position.z);
                Destroy(this.gameObject);
                GameObject e = Instantiate(Exp1.gameObject, newPos, Quaternion.identity);
                e.transform.SetParent(EXP1s.transform);
            }
        }
    }
    void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        moveCoroutine =  StartCoroutine(Move());
        StartCoroutine(Attack());
        EnemyMesh.speed = walkSpeed;
        Player = GameObject.Find("Player");
        EXP1s = GameObject.FindGameObjectWithTag("Exp1s");
        //playerHealth = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        DesrtoyEnemy();
    }

    //    private void OnTriggerEnter(Collider collision)
    //    {
    //        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider")
    //        {
    //            Debug.Log(playerHealth.HP);
    //            if (playerHealth != null)
    //            {
    //                playerHealth.GetDamage(damage);
    //            }
    //        }
    //    }
}
