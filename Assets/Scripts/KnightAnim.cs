using UnityEngine;
using System.Collections;

public class KnightAnim : MonoBehaviour {

    public float speedrun = 3;

    public bool isMoving;

    void Update()
    {
        if (isMoving)
        {
            transform.GetComponent<Animation>().Play("Walk", AnimationPlayMode.Mix);

        }
        else
        {
            transform.GetComponent<Animation>().Play("Wait", AnimationPlayMode.Mix);
            
        }
    }
	

}
