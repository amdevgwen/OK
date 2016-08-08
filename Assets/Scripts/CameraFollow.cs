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
        if (Input.GetAxisRaw("CHS") != 0)
        {
            PlayerController.instance.rotationsjunk = rotator.rotation;
            rotator.Rotate(Vector3.up * Input.GetAxisRaw("CHS") * (rotateSpeed * Time.deltaTime));

        }

        if (Input.GetButton("CameraLeft") != Input.GetButton("CameraRight"))
        {
            int posneg = 1;
            if (Input.GetButton("CameraRight"))
            {
                posneg = -1;
            }
            PlayerController.instance.rotationsjunk = rotator.rotation;
             rotator.Rotate(Vector3.up * posneg * (rotateSpeed * Time.deltaTime));

        }

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
