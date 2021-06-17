using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ObstacleAgent : MonoBehaviour
{
    private NavMeshAgent fowlAgent; //
    private Waypoints[] waypoints; //Referencing the waypointscript as an array.

    [SerializeField]
    private Animator anim;//the Animator so that  can be animated.

    // Will give us a random waypoint in the array as a variable everytime it's accessed
    private Waypoints RandomPoint => waypoints[Random.Range(0, waypoints.Length)];

    // Start is called before the first frame update
    void Start()
    {
        fowlAgent = gameObject.GetComponent<NavMeshAgent>();
        // FindObjectsOfType gets every instance of this component in the scene
        waypoints = FindObjectsOfType<Waypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        //sets a bool in the animator to turn on running if the agent is not pending a path
        anim.SetBool("Run", !fowlAgent.pathPending && fowlAgent.remainingDistance > 0.1f);
        Debugging();

        //Logs out if a new path has been set - checking for "freezing" on path select
        if (fowlAgent.pathPending)
            Debug.LogWarning("Going to new path");

        if (fowlAgent.isPathStale)
        {
            // if the status of the path is invalid, set a new destination
            fowlAgent.SetDestination(RandomPoint.Position);
            Debug.LogWarning("The path was partial, and i chose a new destination");
        }
        // Has the agent reached it's position?
        if (!fowlAgent.pathPending && fowlAgent.remainingDistance < 0.1f)
        {
            // Tell the agent to move to a random position in the scene waypoints
            fowlAgent.SetDestination(RandomPoint.Position);
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if(agentMax != null)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawWireSphere(agentMax.destination, 1f);

    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawLine(agentMax.steeringTarget, transform.position);
    //        Gizmos.DrawWireSphere(agentMax.steeringTarget, 1f);
    //    }
    //}

    private void Debugging()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fowlAgent.isPathStale == true)
            {
                Debug.LogError("It'll never work!");
            }
            else
                Debug.Log("But it's certainly worth a try!?");


        }
    }

}
