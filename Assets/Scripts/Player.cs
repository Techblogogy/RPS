using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float vel = 6.0f; //Velocity
	public float grv = 20.0f; //Gravity

	private Vector3 dir; //Movement Direction Vector
	private CharacterController charC; //Character Controller

	private float hit;
	private Vector3 tmpHt;

	private Plane pl;

//	public GameObject pOG;
//	public GameObject plOb;

	void Start () 
	{
		charC = this.gameObject.AddComponent<CharacterController> ();

		pl = new Plane(Vector3.up, new Vector3(0,1.205f,0));
	}

	void Update () 
	{
//		plOb.transform.position = this.transform.position;

		Rotate ();
		Move ();
		Fire ();
	}

	private void Rotate()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		Debug.DrawRay(Vector3.up, Vector3.zero, Color.red, 100);

//		Debug.Log (Camera.main.ScreenToWorldPoint (Input.mousePosition));

		if (pl.Raycast(ray, out hit) )
		{
			tmpHt = ray.GetPoint(hit);//hit.point;
//			tmpHt = new Vector3(tmpHt.x, this.transform.position.y, tmpHt.z);
			this.transform.LookAt(new Vector3(tmpHt.x, this.transform.position.y, tmpHt.z));

//			pOG.transform.position = tmpHt;
		}

//		Debug.Log ();
//		hit = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//		Debug.Log (hit);
//		this.transform.LookAt(new Vector3(hit.x, this.transform.position.y, hit.z));
	}

	private void Move()
	{
		if (charC.isGrounded)
		{
			dir = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
//			dir = transform.TransformDirection(dir);
			dir *= vel;
		}

		dir.y -= grv * Time.deltaTime;
		charC.Move (dir * Time.deltaTime);
	}

	private void Fire()
	{
		Debug.Log (Input.GetButtonDown("Fire1"));

		if (Input.GetButtonDown("Fire1"))
		{
			Vector3 head = tmpHt - this.transform.position;
			float dist = head.magnitude;
			Vector3 direct = head / dist;

			Debug.DrawRay(this.transform.position,
			              direct*100, 
			              Color.green, 10);

//			Debug.DrawRay(this.transform.position,
//			              transform.TransformDirection(Vector3.forward)*10, 
//			              Color.green, 10);
		}
	}
}
