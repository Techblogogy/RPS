using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public GameObject pl;
	private NavMeshAgent na;

	void Start () {
		na = GetComponent<NavMeshAgent> ();
	}

	void Update () {
        na.SetDestination(pl.transform.position);
	}
}
