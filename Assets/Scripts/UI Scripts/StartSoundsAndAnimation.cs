using UnityEngine;
using System.Collections;

public class StartSoundsAndAnimation : MonoBehaviour {
    public bool triggerFall;
    public bool triggercrash;

    public Transform bullshitThing;

    public AudioClip whistle;
    public AudioClip crash;

    bool called;
    bool secondcalled;
    void Update()
    {
        if (triggerFall)
        {
            if (!secondcalled)
            {
                secondcalled = true;
                transform.GetComponent<AudioSource>().PlayOneShot(whistle);
            }
            //
            triggerFall = false;
        }
        if (triggercrash)
        {
            if (!called)
            {
                called = true;
                transform.GetComponent<AudioSource>().PlayOneShot(crash);
            }
            bullshitThing.GetComponent<Animation>().CrossFade("Dead", 0.01f);
            
            triggercrash = false;

        }

    }
	

}
