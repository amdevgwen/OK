using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
    CharacterController control;

    public bool playerCanMove = true;
    public float gravity = 4;

    public static PlayerMovement PlayerInstance
    {
        get {return _instance;}
    }
    private static PlayerMovement _instance;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        control = transform.GetComponent<CharacterController>();
    }

    public void MoveCharacter(Vector2 dir)
    {
        float g = 0;
        if (!control.isGrounded)
        {
            g = gravity * Time.deltaTime;
        }
        Vector3 dirk = new Vector3(dir.x, gravity, dir.y);
        control.Move(dirk * Time.deltaTime);

    }
}
