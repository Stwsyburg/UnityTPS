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
    public float DisToPlayer;
    private GameObject Player;
    private Vector3 disVec;
    public float EnemyHP;
    // Start is called before the first frame update

    IEnumerator Move()
    {
        while(true)
        {
            yield return null;
            yield return new WaitForSeconds(waitTime);
            EnemyMesh.speed = walkSpeed;
            Vector3 ranMove = new Vector3(Random.Range(this.transform.position.x - distance, this.transform.position.x + distance), this.transform.position.y, Random.Range(this.transform.position.z - distance, this.transform.position.z + distance));
            EnemyMesh.SetDestination(ranMove);
            yield return new WaitForSeconds(Random.Range(0, 5));
            EnemyMesh.speed = 0;
        }
    }

    public void Hurt(int damage)
    {
        EnemyHP -= damage;
    }
    private void DesrtoyEnemy()
    {
        if(Vector3.Distance(Player.transform.position , this.transform.position) > DisToPlayer || EnemyHP <= 0)
        //if((Player.transform.position - this.transform.position).magnitude > DisToPlayer)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(Move());
        EnemyMesh.speed = walkSpeed;
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DesrtoyEnemy();
    }
}
