using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMain : MonoBehaviour {
    public List<MinionController> CurrentMinions;
    public int DebugMaxMinions;
    public List<MinionController> AllMinions;

    public static GameMain instance{
     get{return _instance;}   
    }

    private static GameMain _instance;

    public void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        PlayerController.instance.ControllerUpdate();
        for (int i = 0; i < AllMinions.Count; i++)
        {
            AllMinions[i].MinionUpdate();
        }

    }

    public void TryAddMinion(Transform Minion)
    {
        if(CurrentMinions.Count < DebugMaxMinions){
            if (!CurrentMinions.Contains(Minion.GetComponent<MinionController>()))
            {
                CurrentMinions.Add(Minion.GetComponent<MinionController>());
                Minion.GetComponent<MinionController>().StartFollowPlayer();
            }
        }else{
            
        }        
    }

    public void RemoveMinion(Transform Minion)
    {
        if (CurrentMinions.Contains(Minion.GetComponent<MinionController>()))
        {
            CurrentMinions.Remove(Minion.GetComponent<MinionController>());
            Minion.GetComponent<MinionController>().StopFollowingPlayer();
        }
        else
        {
            Debug.Log("Tried to remove a minion that they player didn't have");
        }

    }

}
