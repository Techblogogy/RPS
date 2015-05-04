using UnityEngine;
using System.Collections;

public class PlayerCam : MonoBehaviour 
{
	public GameObject player;
	public float damp = 2.0f;
	public float offset = 1.0f;

	private Vector3 mVec;

	void Start () 
	{
		
	}

	void Update () 
	{
		mVec = new Vector3 (player.transform.position.x+offset, this.transform.position.y, player.transform.position.z-8.5f);
		this.transform.position = Vector3.Lerp (this.transform.position, mVec, damp * Time.deltaTime);

//		this.transform.LookAt (player.transform.position);
	}
}
