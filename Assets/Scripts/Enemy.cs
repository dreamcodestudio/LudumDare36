using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public EnemyEnergy unitEnergy;
    public NavMeshAgent navMeshAgent;
    public Transform target;

	void Start ()
	{
	    navMeshAgent.SetDestination(target.position);
	}
}
