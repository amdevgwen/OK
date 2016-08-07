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
        for (int i = 0; i < AllMinions.ToArray().Length; i++)
        {
            if (AllMinions[i] != null)
            {

                AllMinions[i].MinionUpdate();
            }
            else
            {
                Debug.LogWarning("Something Messed Up");
            }
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


    public void TryCreateMinion(Transform Minion)
    {
        if (!AllMinions.Contains(Minion.GetComponent<MinionController>()))
        {
            AllMinions.Add(Minion.GetComponent<MinionController>());
        }
        List<MinionController> k = AllMinions;
        for (int i = 0; i < AllMinions.Count; i++)
        {
            while (k[i] == null)
            {
                k.Remove(k[i]);
            }
        }
        AllMinions = k;
    }
}
