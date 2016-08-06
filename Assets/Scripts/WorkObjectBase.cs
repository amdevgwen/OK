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

    public Transform[] MinionPositions;
    public int MinionTarget;


    NavMeshAgent _agent;

    public List<Transform> MinionsOwned;
    

    public void CollideWith(Transform minions){
        if(MinionsOwned.Contains(minions)){
            //do nothing
        }else{
            minions.GetComponent<MinionController>().SendToWork(MinionPositions[MinionsOwned.Count - 1], transform);
        }
    }

    public void RemoveFromWork(Transform minion)
    {
        MinionsOwned.Remove(minion);

    }


}
