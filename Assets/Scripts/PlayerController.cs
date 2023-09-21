using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rig;
    private Animator ani;
    private Vector3 moveDir;
    private float horizontal;
    private float vertical;
    private float speed;
    public  int HP;
    LayerMask floorMask;
    private RaycastHit raycastHit;
    private Vector3 faceVec;
    private float Exp_val;
    private float Level_up_val;
    private int Level;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        BloodManager.CurrentBlood = HP;
        BloodManager.MaxBlood = HP;
        BulletFly.Damage = 10;
        speed = 6;
        floorMask = LayerMask.GetMask("floor");
        Exp_val = 0;
        Level = 1;
        Level_up_val = 2 * Level ^ 3 + 20 * Level ^ 2 - 10 * Level + 100;
        ExpManager.CurrentExp = Exp_val;
        ExpManager.MaxExp = Level_up_val;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerMove();
        ani.SetFloat("x",horizontal);
        ani.SetFloat("y",vertical);
        PlayerRotate();
    }

    void PlayerMove()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 playerVel = new Vector3(horizontal* speed,0,vertical*speed);
        rig.velocity = playerVel;
        bool HasSpeed = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon || Mathf.Abs(rig.velocity.z) > Mathf.Epsilon;
        ani.SetBool("isRun", HasSpeed);
        //if (horizontal != 0 || vertical != 0)
        //{
        //    moveDir = new Vector3(horizontal, 0, vertical);
        //    moveDir.Normalize();
        //    rig.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);
        //    ani.SetBool("isRun", true);
        //}
        //else
        //{
        //    ani.SetBool("isRun", false);
        //}
    }

    void PlayerRotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool result = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, floorMask);
        if(result)
        {
            faceVec = raycastHit.point - transform.position;
            Quaternion faceDir = Quaternion.LookRotation(faceVec);
            rig.MoveRotation(faceDir);
        }
    }

    public bool isAlive()
    {
        return HP > 0;
    }

    public void GetDamage(int dmg)
    {
        HP -= dmg;
        if(HP < 0)
        {
            HP = 0;
        }
        BloodManager.CurrentBlood = HP;
        if (!isAlive())
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public void Get_Exp(int exp)
    {
        Exp_val += exp;
        if(Exp_val < Level_up_val)
            ExpManager.CurrentExp = Exp_val;
        else
        {
            ExpManager.CurrentExp = Level_up_val;
            Exp_val = Exp_val - Level_up_val;
            Level += 1;
            Level_UP();
            Level_up_val = 2 * Level ^ 3 + 20 * Level ^ 2 - 10 * Level + 100;
            ExpManager.CurrentExp = Exp_val;
            ExpManager.MaxExp = Level_up_val;
        }
    }

   private void Level_UP()
    {
        HP += 10*Level;
        BloodManager.CurrentBlood = HP;
        BloodManager.MaxBlood = HP;
        //Debug.Log(BloodManager.CurrentBlood);
        BulletFly.Damage += Level;
    }

}
