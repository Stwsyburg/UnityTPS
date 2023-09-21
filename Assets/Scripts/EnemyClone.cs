using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClone : MonoBehaviour
{
    public Transform PlayerPos;
    public float DisToPlayer;
    public GameObject Enemy;
    public float waitTime;
    public GameObject Enemies;
    // Start is called before the first frame update
    IEnumerator Clone()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            if (PlayerPos!=null)
            {
                Vector3 newPos = new Vector3(Random.Range(PlayerPos.position.x - DisToPlayer, PlayerPos.position.x + DisToPlayer) + 6, 0, Random.Range(PlayerPos.position.z - DisToPlayer, PlayerPos.position.z + DisToPlayer) + 5);
                GameObject e = Instantiate(Enemy.gameObject, newPos, Quaternion.identity);
                e.transform.SetParent(Enemies.transform);
            }
        }
    }
    void Start()
    {
        StartCoroutine(Clone());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
