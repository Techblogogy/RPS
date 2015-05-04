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

	private void Rotate()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //Mouse Click Ray
        if (r_plane.Raycast(ray, out r_distance))
		{
            r_hitPoint = ray.GetPoint(r_distance);
            this.transform.LookAt(new Vector3(
                r_hitPoint.x, 
                this.transform.position.y, 
                r_hitPoint.z));
		}
	}

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

	private void Fire()
	{
		if (Input.GetButtonDown("Fire1"))
		{
            Vector3 head = r_hitPoint - this.transform.position;
			float dist = head.magnitude;
			Vector3 direct = head / dist;

			Debug.DrawRay(this.transform.position,
			              direct*100, 
			              Color.green, 10);
		}
	}
}
