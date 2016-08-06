using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public float start;
    public float stop;

    bool moving;

    public float xdist;
    public float smoothTime = 0.3f;

    Transform rotator;

    public float rotateSpeed;

    void Start()
    {
        rotator = transform.FindChild("Rotator");
    }
    Vector3 velocit = Vector3.zero;


    void Update()
    {
        if (!moving && Vector3.Distance(PlayerMovement.PlayerInstance.transform.position, transform.position) > start)
        {
            moving = true;
        }
        else if (moving && Vector3.Distance(PlayerMovement.PlayerInstance.transform.position, transform.position) < stop)
        {
            moving = false;
        }
        if (moving)
        {
            transform.position = Vector3.SmoothDamp(transform.position, PlayerMovement.PlayerInstance.transform.position, ref velocit, smoothTime);
        }


    }







}
