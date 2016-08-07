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

    public int extraInfo;

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

    public float playerDist;

    public bool working;



    bool onetime;
    void Update()
    {
        TextMesh m = transform.FindChild("New Text").GetComponent<TextMesh>();
        if((Vector3.Distance(PlayerMovement.PlayerInstance.transform.position, transform.position) < playerDist) || MinionsOwned.Count  >=1){

            m.transform.gameObject.SetActive(true);

            m.text = MinionsOwned.Count.ToString() + "/" + MinionTarget.ToString();

        }
        else
        {
            m.transform.gameObject.SetActive(false);
        }
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
        if (working &&(Vector3.Distance(_agent.destination, transform.position) <= playerDist))
        {
            if (onetime)
            {

            }
            else
            {
                onetime = true;
                //stop working
                _agent.enabled = false;
                transform.FindChild("Capsule").gameObject.SetActive(false);
                foreach (Transform k in MinionsOwned.ToArray())
                {
                    RemoveFromWork(k);
                    k.GetComponent<MinionController>().finishWork();
                    k.transform.SetParent(null, true);
                }
                Target.SendMessage("FinishJob", this.transform);
                Debug.Log("This Did it");
                working = false;
                Destroy(this);
            }
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
