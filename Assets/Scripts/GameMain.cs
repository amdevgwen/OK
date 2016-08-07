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
    public Transform Spawner;
    
    
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
                if (!fillids.Contains(i))
                {
                    FillThis(i);
                }
            }
        }

    }
    List<int> fillids = new List<int>();

    void FillThis(int ID)
    {
        fillids.Add(ID);
        StartCoroutine(Spawner.GetComponent<Reproducer>().createReplace(ID));
    }
   public void FinishID(int id, MinionController replacement)
    {
        fillids.Remove(id);
        if (AllMinions[id] == null)
        {
            AllMinions[id] = replacement;
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
