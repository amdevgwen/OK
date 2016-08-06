using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class PlayerDongle : MonoBehaviour {
    //make sure this is a child of the character

    public bool DongleCanMove;
    public float LocalRadius;

    public float localhover = 2;
    public LayerMask maskingjunk;

    public void MoveDongle(Vector2 dir)
    {
        Vector3 localPosition = transform.localPosition;
        Vector2 twoFactorLocal = new Vector2(localPosition.x, localPosition.z);

        Vector3 pos = transform.position;
        pos.y += 40;

        Ray m = new Ray(pos, Vector3.down);
        RaycastHit hit;
        float y = 0 ;
        if(Physics.Raycast(m, out hit, Mathf.Infinity, maskingjunk))
        {
            y = hit.point.y;
        }

        twoFactorLocal += dir *  Time.deltaTime;
        if((Vector2.Distance(new Vector2(0,0), twoFactorLocal) > LocalRadius)){
            twoFactorLocal = twoFactorLocal.normalized * LocalRadius;
        }
        localPosition.x = twoFactorLocal.x;
        localPosition.z = twoFactorLocal.y;
         
        transform.localPosition = localPosition;
        Vector3 globaly = transform.position;
        globaly.y = y + localhover;
        transform.position = globaly;

    }

    public float baseRadius;
    public float maxRadius;

    float currentRadius;

    public float chargeTime;

    public Transform WhistleChild;
    public Transform CursorChild;

    public void DoWhistle()
    {
        Debug.Log("Whistle Called");
        if (!beingheld)
        {
            Debug.Log("WhistleSucess");
            StartCoroutine(HoldWhistle());
        }
    }

    bool beingheld = false;
    IEnumerator HoldWhistle()
    {
        currentRadius = baseRadius;
        float timesince = Time.time;
        while (Input.GetButton("Call"))
        {
            Debug.Log("DoingStuff");
            beingheld = true;
            float percentagetoone = (Time.time - timesince) / chargeTime;
            
            
            currentRadius = baseRadius + ((maxRadius - baseRadius) * percentagetoone);

            if (currentRadius >= maxRadius)
            {
                currentRadius = maxRadius;
            }
            //showwhistle
            WhistleChild = transform.FindChild("CallPips");
            WhistleChild.gameObject.SetActive(true);
            WhistleChild.GetComponent<DonglePips>().DoIt(currentRadius);
            CallMinions(currentRadius);
            yield return null;
        }
        WhistleChild.gameObject.SetActive(false);
        beingheld = false;
        currentRadius = 0;
        //hideWhistle
        yield return null;
    }

    void CallMinions(float dist)
    {

        foreach (MinionController mooogle in GameMain.instance.AllMinions)
        {
            if (!GameMain.instance.CurrentMinions.Contains(mooogle) && (Vector3.Distance(new Vector3(mooogle.transform.position.x, transform.position.y, mooogle.transform.position.z), transform.position) <= dist))
            {
                mooogle.TryFollowPlayer();
            }
        }
    }
}
