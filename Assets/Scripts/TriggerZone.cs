using UnityEngine;
using System.Collections;



public class TriggerZone : MonoBehaviour {
    public enum TriggerType
    {
        Player,
        Object
    }

    public TriggerType trigType;
    void OnTriggerEnter(Collider other)
    {
       
        switch (trigType)
        {
            case TriggerType.Player:
                Debug.Log("this is working");
                if (other.gameObject.tag == "Minion")
                {
                    Debug.Log("Has Collided With Minion");
                    other.GetComponent<MinionController>().TryFollowPlayer();
                }
                break;
            case TriggerType.Object:
                if (other.gameObject.tag == "Minion")
                {
                    transform.parent.GetComponent<WorkObjectBase>().CollideWith(other.transform);
                }
                break;

        }
    }
	
}
