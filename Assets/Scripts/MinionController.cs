using UnityEngine;
using System.Collections;


public class MinionController : MonoBehaviour {
    public enum MinionState
    {
        Working,
        GoingToWork,
        FollowPlayer,
        Wait
    }
    public MinionState currentState;

    Transform CurrentTarget;
    public float MaxSpeed;
    public float stoppingdist;

    NavMeshAgent _agent;



    void Start()
    {
        _agent = transform.GetComponent<NavMeshAgent>();
        transform.tag = "Minion";
    }

    public void MinionUpdate()
    {
        _agent.enabled = (MinionState.Working != currentState);
        switch (currentState)
        {
            case MinionState.FollowPlayer:
                CurrentTarget = PlayerMovement.PlayerInstance.transform;
                _agent.SetDestination(CurrentTarget.position);
                _agent.stoppingDistance = stoppingdist;
                break;
            case MinionState.Wait:

                _agent.SetDestination(transform.position);
                break;
            case MinionState.GoingToWork:
                
                _agent.stoppingDistance = 0;
                break;
        }
    }

    public void SendToWork(Transform targetPos, Transform TargetObj)
    {
        currentState = MinionState.GoingToWork;
        CurrentTarget = TargetObj;
        _agent.SetDestination(targetPos.position);
    }
    public void finishWork()
    {
        _agent.enabled = true;
        currentState = MinionState.Wait;
        CurrentTarget = null;
        _agent.SetDestination(transform.position);
    }
    public void quitWork()
    {
        _agent.enabled = true;
        currentState = MinionState.FollowPlayer;
        CurrentTarget.GetComponent<WorkObjectBase>().RemoveFromWork(transform);

    }

    public void StopFollowingPlayer()
    {
        GameMain.instance.RemoveMinion(this.transform);
        Debug.Log(this.transform.name + " has stopped following the player");
    }
    public void TryFollowPlayer()
    {
        GameMain.instance.TryAddMinion(this.transform);
    }
    public void StartFollowPlayer()
    {
        currentState = MinionState.FollowPlayer;
    }
}
