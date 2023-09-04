using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Rigidbody rig;
    public GameObject bullet;
    public GameObject FirePos;
    private GameObject BulletClone;
    private Ray ray;
    private RaycastHit hit;
    public bool AutoAtt;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        if (AutoAtt)
        {
            BulletClone = GameObject.Find("Bullets");
            InvokeRepeating("AutoAttack", 0.05f, 0.1f);
        }
        else
        {
            StartCoroutine(FireCoroutine());
            BulletClone = GameObject.Find("Bullets");
        }
    }
    private void AutoAttack()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemys.Length > 0)
        {
            //ray = Camera.main.ScreenPointToRay(enemys[Random.Range(0, enemys.Length)].transform.position);
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity) && rig != null)
            //{
            //    rig.transform.LookAt(hit.point);
            //}
            GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.transform.SetParent(BulletClone.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FireCoroutine()
    {
        while(true)
        {
            yield return null;
            if(Input.GetMouseButtonDown(0))
            {
                GameObject b = Instantiate(bullet.gameObject, FirePos.transform.position, FirePos.transform.rotation);
                b.transform.SetParent(BulletClone.transform);
                yield return new WaitForSeconds(0.05f);
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit , Mathf.Infinity) && b != null )
                {
                    b.transform.LookAt(hit.point);
                }
            }
        }
    }
}