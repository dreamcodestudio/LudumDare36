using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;

	void Start ()
	{
	    //navMeshAgent.SetDestination(target.position);
	}
	

	void Update ()
    {
	
	}
}
