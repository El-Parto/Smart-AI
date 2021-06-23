using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Progressing,
    GotToCoin,
    GoToSwitch,

}

public delegate void StateDelegate();// refered to as " SD " in the script below


public class StateMachine : AgentMaxwell
{
    [SerializeField] private States currentState = States.Progressing; // something to store what state it's currently in.


    [SerializeField]
    private GameObject switchTrigger;
    [SerializeField]
    private GameObject coinTrigger;
    [SerializeField]
    private GameObject pickUpCoinTrigger;
    [SerializeField]
    private GameObject pressSwitchTrigger;



    // this dictionary holds a key that is the enum and runs a value that is the SD which is empty
    private Dictionary<States, StateDelegate> states = new Dictionary<States, StateDelegate>();

    [SerializeField]
    private StateDelegate stateDelegate;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        states.Add(States.Progressing, Progress);
        states.Add(States.GotToCoin, ProgressToCoin);
        states.Add(States.GoToSwitch, ProgressToSwitch);
    }

    public void ChangeState(States _newState)
    {
        if (_newState != currentState)
        {
            currentState = _newState;
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (states.TryGetValue(currentState, out StateDelegate state))
            state.Invoke();
    }
    
    public void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("ToSwitch"))
        {
            Debug.Log("I'm in");
            currentState = States.GoToSwitch;
            

        }
            
        if (collision.CompareTag("ToCoin"))
        {
            currentState = States.GotToCoin;
            coinTrigger.SetActive(false);
        }
            

        if (collision.CompareTag("GetCoin"))
        {
            switchTrigger.SetActive(true);
            coin.SetActive(false);
            currentState = States.Progressing;
            currentWaypoint = 0;

        }

        if (collision.CompareTag("PushSwitch"))
        {
            door.SetActive(false);
            currentWaypoint = 0;
            currentState = States.Progressing;

        }
    }


    //public IEnumerator Move()
    //{
    //    while (currentWaypoint < waypoints.Length) // while the current way point value is less than the length of the waypoint array
    //    {
    //        //MakeAIMove();
    //        // set desination based on waypoint array [ID'ing currentWaypoint then iterate].'s position
    //        agentMax.SetDestination(waypoints[currentWaypoint++].Position);

    //        // then wait until its not pending a path, and it's remaining distance is less than < .1f
    //        yield return new WaitUntil(() => !agentMax.pathPending && agentMax.remainingDistance < 0.5f);
    //    }
    //}


    public void Progress()
    {   
        
        if (currentWaypoint < waypoints.Length)
            agentMax.SetDestination(waypoints[currentWaypoint++].Position);
    }
    public void ProgressToCoin()
    {
        if (!agentMax.pathPending && agentMax.remainingDistance < 0.1f)
            agentMax.SetDestination(PathToCoins.Position);
    }
    public void ProgressToSwitch()
    {
        if (!agentMax.pathPending && agentMax.remainingDistance < 0.1f)
            agentMax.SetDestination(PathToSwitch.Position);
    }



}
