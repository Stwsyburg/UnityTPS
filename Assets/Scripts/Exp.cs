using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    //private Rigidbody ExpRig;
    private int Exp_num;
    // Start is called before the first frame update
    void Start()
    {
        Exp_num = 10;
        //ExpRig = GetComponent<Rigidbody>();
        StartCoroutine("DestroySelf");
    }

    // Update is called once per frame
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(collision.gameObject.CompareTag("Player"));
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Get_Exp(Exp_num);
            Destroy(this.gameObject);
        }
    }
}
