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

    
    // this dictionary holds a key that is the enum and runs a value that is the SD which is empty
    private Dictionary<States, StateDelegate> states = new Dictionary<States, StateDelegate>(); 
    
    // Start is called before the first frame update
    void Start()
    {
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
    void Update()
    {
        if (states.TryGetValue(currentState, out StateDelegate state))
            state.Invoke();

    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Switch"))
        {
            door.SetActive(false);
        }
    }

    public void Progress()
    {
        if (!agentMax.pathPending && agentMax.remainingDistance < 0.1f)
            agentMax.SetDestination(PathToGoal.Position);
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
