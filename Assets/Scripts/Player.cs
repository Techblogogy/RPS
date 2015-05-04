using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    //Player Move
	public float m_vel = 6.0f; //Player Velocity
	public float m_grv = 20.0f; //Player Gravity

    private CharacterController m_controller; //Player Character Controller 
    private Vector3 m_direction; //Player Direction

    //Mouse Look
	private Vector3 r_hitPoint; //Raycast Hit Point
	private float r_distance; //Raycast Distance
	private Plane r_plane; //Raycast Plane
    private Ray r_ray; //Raycast ray

    //Player Fire
    public float f_distance = 10.0f; //Fire Distance
    private RaycastHit f_hit; //Fire Raycast Hit
    private int f_layer = 1 << 8; //Fire Layer

	void Awake () 
	{
        m_controller = this.gameObject.AddComponent<CharacterController>(); //Get Character Controller
        r_plane = new Plane(Vector3.up, new Vector3(0, 1.205f, 0)); //Get Raycast Plane
	}

	void Update () 
	{
		Rotate (); //Handle Rotation
		Move (); //Handle Movement
		Fire (); //Handle Shooting
	}

    //Player Rotation Logic
	private void Rotate()
	{
        r_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (r_plane.Raycast(r_ray, out r_distance))
		{
            r_hitPoint = r_ray.GetPoint(r_distance);
            this.transform.LookAt(new Vector3(
                r_hitPoint.x, 
                this.transform.position.y, 
                r_hitPoint.z));
		}
	}

    //Player Move Logic
	private void Move()
	{
        if (m_controller.isGrounded)
		{
            m_direction = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            m_direction *= m_vel;
		}

		m_direction.y -= m_grv * Time.deltaTime;
        m_controller.Move(m_direction * Time.deltaTime);
	}

    //Player Weapon Fire Logic
	private void Fire()
	{
		if (Input.GetButtonDown("Fire1"))
		{
            Vector3 head = r_hitPoint - this.transform.position;
			float dist = head.magnitude;
			Vector3 direct = head / dist;

            if (Physics.Raycast(this.transform.position, direct, out f_hit, f_distance, f_layer)) 
            {
                Destroy(f_hit.collider.gameObject);
            }

			Debug.DrawRay(this.transform.position,
			              direct*100,
                          Color.green, f_distance);
		}
	}
}
