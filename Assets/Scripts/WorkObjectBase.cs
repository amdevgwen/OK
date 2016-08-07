using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkObjectBase : MonoBehaviour {

    public enum WorkType
    {
        Wall,
        Dig,
        PressurePlate,
        Carry
    }
    public WorkType objectType;

    public Transform[] MinionPositions;
    public int MinionTarget;


    public Transform Target;
    public float timetoComplete;

    NavMeshAgent _agent;

    void Awake()
    {
        switch (objectType)
        {
            case WorkType.Carry:
                _agent = transform.GetComponent<NavMeshAgent>();
                break;
        }
    }


    public List<Transform> MinionsOwned;
    

    public void CollideWith(Transform minions){
        if(MinionsOwned.Contains(minions)){
            //do nothing already has minion
        }else{
            minions.GetComponent<MinionController>().SendToWork(MinionPositions[MinionsOwned.Count], transform);
        }
    }

    public void RemoveFromWork(Transform minion)
    {
        MinionsOwned.Remove(minion);

    }
    public void JoinWorkForce(Transform Minion)
    {
        MinionsOwned.Add(Minion);
        Minion.GetComponent<MinionController>().clockInToWork(MinionPositions[MinionsOwned.IndexOf(Minion)]);
       
        Minion.localPosition = new Vector3();
    }

    public bool working;
    void Update()
    {

        if (MinionsOwned.Count >= MinionTarget)
        {
            Debug.Log("Shit is fuckijn working");
            working = true;
            switch (objectType)
            {
                case WorkType.Carry:
                    CarryTo(0);
                    break;

            }
            //able to work
        }
        else if(working)
        {
            working = false;
            CeaseWork();
        }
    }

    void CeaseWork()
    {
        switch (objectType)
        {
            case WorkType.Carry:
                _agent.enabled = false;
                break;
        }
    }

    void CarryTo(float PercentOver)//max double the time
    {

        _agent.enabled = true;
        _agent.SetDestination(Target.position);
    }   


}
