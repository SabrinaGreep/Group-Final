using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float randomRadius = 10f; 
    public float randomWaitTimeMin = 2f; 
    public float randomWaitTimeMax = 5f; 

    private bool isWaiting = false;

    void Start()
    {
        SetRandomDestination();
    }

    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && !isWaiting)
        {
            StartCoroutine(RandomWaitBeforeMoving());
        }
    }

    private IEnumerator RandomWaitBeforeMoving()
    {

        isWaiting = true;

        float waitTime = Random.Range(randomWaitTimeMin, randomWaitTimeMax);
        yield return new WaitForSeconds(waitTime);

        SetRandomDestination();

        isWaiting = false;
    }


    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * randomRadius;
        randomDirection += transform.position; 

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, randomRadius, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            SetRandomDestination();
        }
    }
}
