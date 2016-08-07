using UnityEngine;
using System.Collections;

public class fuuuuuuk : MonoBehaviour {
    public Transform DoBoom;
    public void FinishJob(Transform target)
    {
        DestroyImmediate(target.gameObject);
        DoBoom.GetComponent<BlowShitUp>().Boom();

    }
	



}
