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

    public IEnumerator growGuy(int number)
    {
        
            for (int i = 0; i < number; i++)
            {
                float moveto = Time.time + TimeToGrow;
                GameObject h = GreenGuy;
                h.GetComponent<MinionController>().replace = false;
                GameObject k = Instantiate(h, transform.position, Quaternion.identity) as GameObject;
                k.layer = 10;
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
                k.layer = 8;
                k.GetComponent<MinionController>().enabled = true;
                k.GetComponent<NavMeshAgent>().enabled = true;

            }

        
    }

    public IEnumerator createReplace(int id)
     {
            float moveto = Time.time + TimeToGrow;
            GameObject h = GreenGuy;
            h.GetComponent<MinionController>().replace = true;
            GameObject k = Instantiate(h, transform.position, Quaternion.identity) as GameObject;
            k.layer = 10;
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
            k.layer = 8;
            k.GetComponent<MinionController>().enabled = true;
            k.GetComponent<NavMeshAgent>().enabled = true;
            GameMain.instance.FinishID(id, k.GetComponent<MinionController>());
    }

}
