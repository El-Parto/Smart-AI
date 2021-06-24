using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMaxwell : MonoBehaviour
{
    protected NavMeshAgent agentMax; //Maxwell's very own Agent
    protected Waypoints[] waypoints; //Referencing the waypointscript as an array.
    protected CoinWayPoints[] crayPoints; //
    protected SwitchWaypoints[] swayPoints;

    [SerializeField]
    protected Animator anim;//the Animator so that Maxwell can be animated.

    // Will give us a random waypoint in the array as a variable everytime I access it
    protected Waypoints RandomPoint => waypoints[Random.Range(0, waypoints.Length)];

    // setting a number for waypoints so that it may travel in a straight path
    protected Waypoints PathToGoal => waypoints[waypoints.Length];
    protected CoinWayPoints PathToCoins => crayPoints[crayPoints.Length];

    [SerializeField]
    protected GameObject door;//to hold the door in the inspector
    [SerializeField]
    protected GameObject coin;//to hold the coin in the inspector
    protected SwitchWaypoints PathToSwitch => swayPoints[Random.Range(0,swayPoints.Length)];//This is to set up a "choice" for the AI

    protected int currentWaypoint = 0;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agentMax = gameObject.GetComponent<NavMeshAgent>();
        // FindObjectsOfType gets every instance of this component in the scene
        waypoints = FindObjectsOfType<Waypoints>();
        crayPoints = FindObjectsOfType<CoinWayPoints>();
        swayPoints = FindObjectsOfType<SwitchWaypoints>();
        waypoints = waypoints.OrderBy(waypoint => waypoint.name).ToArray();        
        crayPoints = crayPoints.OrderBy(_craypoint => _craypoint.name).ToArray();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //sets a bool in the animator to turn on running if the agent is not pending a path
        anim.SetBool("Run", !agentMax.pathPending && agentMax.remainingDistance > 0.1f);
        Debugging();
        
        ////Logs out if a new path has been set - checking for "freezing" on path select
        //if (agentMax.pathPending)
        //    Debug.LogWarning("Going to new path");

        //if (agentMax.isPathStale)
        //{
        //    // if the status of the path is invalid, set a new destination
        //    agentMax.SetDestination(RandomPoint.Position);
        //    Debug.LogWarning("The path was partial, and i chose a new destination");
        //}
        //// Has the agent reached it's position?
        //if (!agentMax.pathPending && agentMax.remainingDistance < 0.1f)
        //{
        //    // Tell the agent to move to a random position in the scene waypoints
        //    agentMax.SetDestination(RandomPoint.Position);
        //}
    }



    private void Debugging()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (agentMax.isPathStale == true)
            {
                Debug.LogError("It'll never work!");
            }
            else
                Debug.Log("But it's certainly worth a try!?");


        }
    }
    //protected virtual void Progress()
    //{

    //}
    //protected virtual void ProgressToCoin()
    //{

    //}
    //protected virtual void ProgressToSwitch()
    //{

    //}



}