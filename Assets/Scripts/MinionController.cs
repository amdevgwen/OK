 using UnityEngine;
using System.Collections;


public class MinionController : MonoBehaviour {
    public enum MinionState
    {
        Working,
        GoingToWork,
        FollowPlayer,
        Wait,
        GoToPoint
    }
    public MinionState currentState;

    Transform CurrentTarget;
    public float MaxSpeed;
    public float stoppingdist;

    NavMeshAgent _agent;

    public float snapdistance;
    public bool isMoving;
    void Start()
    {
        _agent = transform.GetComponent<NavMeshAgent>();
        GameMain.instance.AllMinions.Add(this);
        transform.tag = "Minion";
    }
    void Update()
    {
        MinionUpdate();
    }

    Transform WorkLocal;
    public void MinionUpdate()
    {
        isMoving = (_agent.remainingDistance <= _agent.stoppingDistance);
        
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
                
                if(Vector3.Distance(_agent.destination, transform.position) <= snapdistance){
                    _agent.enabled = false;
                    currentState = MinionState.Working;
                    CurrentTarget.GetComponent<WorkObjectBase>().JoinWorkForce(transform);
                }
                break;
            case MinionState.GoToPoint:
                               
                if(Vector3.Distance(_agent.destination, transform.position) <= snapdistance){
                    
                    currentState = MinionState.Wait;
                    
                }
                break;
            case MinionState.Working:
                if (transform.parent != WorkLocal)
                {
                    transform.SetParent(WorkLocal);
                }
                transform.localRotation = Quaternion.identity;
                transform.localPosition = new Vector3();
                break;
        }
    }

    public void SendToWork(Transform targetPos, Transform TargetObj)
    {
        GameMain.instance.RemoveMinion(transform);
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

    public void clockInToWork(Transform workpos)
    {
        _agent.enabled = false;
        WorkLocal = workpos;
        transform.SetParent(workpos);
        currentState = MinionState.Working;
    }

    public void StopFollowingPlayer()
    {
        GameMain.instance.RemoveMinion(this.transform);
        Debug.Log(this.transform.name + " has stopped following the player");
    }
    public void TryFollowPlayer()
    {
        if (currentState == MinionState.Working)
        {
            CurrentTarget.GetComponent<WorkObjectBase>().RemoveFromWork(transform);
            Debug.Log(transform.name + " has gotten off of work after a hard day of being really dumb and bumping into sally from finances");
        }
        GameMain.instance.TryAddMinion(this.transform);
    }
    public void StartFollowPlayer()
    {
        currentState = MinionState.FollowPlayer;
    }

    public void SendToTarget(Vector3 destination)
    {
        _agent.SetDestination(destination);
        _agent.stoppingDistance = stoppingdist;
        currentState = MinionState.GoToPoint;
    }
}
