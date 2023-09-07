using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform follow;
    private Vector3 disVec;
    // Start is called before the first frame update
    void Start()
    {
        disVec = this.transform.position - follow.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (follow != null)
        {
            this.transform.position = follow.position + disVec;
        }
    }
}
