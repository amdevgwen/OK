using UnityEngine;
using System.Collections;



public class Reproducer : MonoBehaviour {



    public float TimeToGrow = 3;
    public GameObject RedGuy;
    public GameObject GreenGuy;
    public GameObject BlueGuy;

    public Vector3 standardScale;

    public void FinishJob(Transform target)
    {
        StartCoroutine(growGuy(target.GetComponent<WorkObjectBase>().extraInfo));
        StartCoroutine(scaleout(target));
    }

    IEnumerator scaleout(Transform targ)
    {
        float moveto = Time.time + TimeToGrow;
        Vector3 oscale = targ.localScale;
        while (Time.time <= moveto)
        {

            float percenttonone = ((moveto - Time.time) / TimeToGrow);
            targ.localScale = oscale * percenttonone;
            yield return null;
        }
        Destroy(targ.gameObject);
    }

    IEnumerator growGuy(int number)
    {
        for (int i = 0; i < number; i++)
        {
            float moveto = Time.time + TimeToGrow;
            GameObject k = Instantiate(GreenGuy, transform.position, Quaternion.identity) as GameObject;
            Transform m = k.transform;
            m.SetParent(null);
            while (Time.time <= moveto)
            {
                Debug.Log("MakeDude");
                m.localScale = Vector3.zero;

                float percenttoone = 1 - ((moveto - Time.time) / TimeToGrow);
                m.localScale = standardScale * percenttoone;
                yield return null;
            }
            k.GetComponent<MinionController>().enabled = true;
           
            k.GetComponent<NavMeshAgent>().enabled = true;
            
        }

    }

}
