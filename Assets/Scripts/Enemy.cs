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
    private Coroutine moveCoroutine;
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
            yield return new WaitForSeconds(Random.Range(0, 5));
            EnemyMesh.speed = 0;
            ani.SetBool("Move", false);
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
                    this.transform.LookAt(Player.transform.position);
                    EnemyMesh.SetDestination(Player.transform.position);
                    if ((Vector3.Distance(Player.transform.position, this.transform.position) <= AttDistance))
                    {
                        if (Player.GetComponent<PlayerController>() != null)
                        {
                            Debug.Log(Player.GetComponent<PlayerController>().HP);
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
                Destroy(this.gameObject);
            }
        }
    }
    void Start()
    {
        ani = GetComponent<Animator>();
        moveCoroutine =  StartCoroutine(Move());
        StartCoroutine(Attack());
        EnemyMesh.speed = walkSpeed;
        Player = GameObject.Find("Player");
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
