using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMain : MonoBehaviour {
    public List<MinionController> CurrentMinions;
    public int DebugMaxMinions;

    public static GameMain instance{
     get{return _instance;}   
    }

    private static GameMain _instance;

    public void Awake()
    {
        _instance = this;
    }



    public void TryAddMinion(Transform Minion)
    {
        if(CurrentMinions.Count < DebugMaxMinions){
            CurrentMinions.Add(Minion.GetComponent<MinionController>());
            Minion.GetComponent<MinionController>().StartFollowPlayer();
        }else{
            
        }        
    }

    public void RemoveMinion(Transform Minion)
    {
        if (CurrentMinions.Contains(Minion.GetComponent<MinionController>()))
        {
            Minion.GetComponent<MinionController>().StopFollowingPlayer();
        }
        else
        {
            Debug.Log("Tried to remove a minion that they player didn't have");
        }

    }

}
