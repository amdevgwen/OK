using UnityEngine;
using System.Collections;

public class PlayerDongle : MonoBehaviour {
    //make sure this is a child of the character




    public bool DongleCanMove;

    public float LocalRadius;

    public void MoveDongle(Vector2 dir)
    {
        Vector3 localPosition = transform.localPosition;
        Vector2 twoFactorLocal = new Vector2(localPosition.x, localPosition.z);

        twoFactorLocal += dir *  Time.deltaTime;
        if((Vector2.Distance(new Vector2(0,0), twoFactorLocal) > LocalRadius)){
            twoFactorLocal = twoFactorLocal.normalized * LocalRadius;
        }
        localPosition.x = twoFactorLocal.x;
        localPosition.z = twoFactorLocal.y;

        transform.localPosition = localPosition;
    }

}
