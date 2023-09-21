using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    private Rigidbody bulletRig;
    public float BulletSpeed;
    public   static int Damage;
    private Transform target;
    private Vector3 lastTargetPos; //Ŀ��λ��
    private Vector3 startPos;//��ʼλ��
    private Vector3 midPos;//�м��
    private float percentSpeed;//��ֵ�ٶ�
    private float percent = 0;//���������߲�ֵ����
    private GameObject FirePos;
    private GameObject Player;
    public bool AutoAtt;
    // Start is called before the first frame update
    void Start()
    {
        bulletRig = GetComponent<Rigidbody>();
        if(AutoAtt)
        {
            Player = GameObject.Find("Player");
            FirePos = FindChildGameObject("FirePos", Player);
            Init();
            StartCoroutine(MoveMissile());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!AutoAtt)
        {
            bulletRig.MovePosition(this.transform.position + transform.forward * BulletSpeed * Time.deltaTime);
        }
        //else
        //{
        //    if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
    }

    private IEnumerator MoveMissile()
    {
        while (percent < 1)
        {
            if(target!=null)
            {
                lastTargetPos = target.position;
                percent += percentSpeed * Time.deltaTime;
                transform.position = Bezier(percent, startPos, midPos, lastTargetPos);
                yield return null;
            }
            else
            {
                Destroy(this.gameObject);
                yield return null;
            }
        }
    }

    public static Vector3 Bezier(float t, Vector3 a, Vector3 b, Vector3 c)
    {
        var ab = Vector3.Lerp(a, b, t);
        var bc = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(ab, bc, t);
    }
    public Vector3 GetMiddlePosition(Vector3 a, Vector3 b)
    {
        Vector3 m = Vector3.Lerp(a, b, 0.1f);
        //ab����
        //Vector3 normal = Vector2.Perpendicular(a - b).normalized;
        Vector3 normal = Vector3.Cross(a, b).normalized;
        float rd = Random.Range(-2f, 2f);
        float curveRatio = 0.3f;
        return m + (a - b).magnitude * curveRatio * rd * normal;
    }

    private void Init()
    {
        //����
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //����е���
        if (enemys.Length > 0)
        {
            int rdId = Random.Range(0, enemys.Length);
            target = enemys[rdId].transform;
        }
        //��ʼλ��,��Ϊ���λ��
        startPos = FirePos.transform.position;
        //�м��
        midPos = GetMiddlePosition(startPos, lastTargetPos);
        percentSpeed = BulletSpeed / (lastTargetPos - startPos).magnitude;
        transform.position = startPos;
    }
    public GameObject FindChildGameObject(string name, GameObject parent)
    {
        Transform[] trans = parent.GetComponentsInChildren<Transform>();
        //��transform�����ȡ����activepanel�µ�������
        foreach (Transform item in trans)
        {
            if (item.name == name)
            {
                return item.gameObject;
            }
        }
        return null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hurt(Damage);
            Destroy(this.gameObject);
        }
    }
}
