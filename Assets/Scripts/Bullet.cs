using UnityEngine;
using System.Collections;

public class Bullet:MonoBehaviour
{
    public Vector3 b_direction; //Bullet Direction
    public float b_velocity = 10.0f; //Bullet Velocity

    private Rigidbody b_rig;

    void Start()
    {
        b_rig = this.GetComponent<Rigidbody>();
        b_direction = transform.TransformDirection(Vector3.left);
    }

    void FixedUpdate()
    {
        //b_rig.MovePosition(this.transform.position + b_direction*Time.deltaTime);
        //this.transform.Translate(b_direction*b_velocity * Time.deltaTime);
    }
}