using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
    CharacterController control;

    public bool playerCanMove = true;


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
        Vector3 dirk = new Vector3(dir.x, 0, dir.y);
        control.Move(dirk * Time.deltaTime);

    }
}
