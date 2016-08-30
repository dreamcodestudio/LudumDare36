using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public EnemyEnergy unitEnergy;
    public NavMeshAgent navMeshAgent;
    public Transform target;

	private BoxCollider _collider;

	void Start ()
	{
		_collider = GetComponent<BoxCollider>();
	    navMeshAgent.SetDestination(target.position);
	}

	public void Stop()
	{
		_collider.enabled = false;
		navMeshAgent.Stop();
	}
}
