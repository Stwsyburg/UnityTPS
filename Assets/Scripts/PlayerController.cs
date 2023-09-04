using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasePlayer
{
    private Rigidbody rig;
    private Animator ani;
    private Vector3 moveDir;
    private float horizontal;
    private float vertical;
    private float speed;
    LayerMask floorMask;
    private RaycastHit raycastHit;
    private Vector3 faceVec;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        speed = 6;
        floorMask = LayerMask.GetMask("floor");
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
}
