using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float MovementDeadzone; //deadzone so that the player doesn't move until the input hits this threshhold, this allows players to move the dongle.
    public float PlayerSpeed;
    public float DongleSpeed;

    public bool isCalling;

    public static PlayerController instance{
     get{return _instance;}   
    }
    private static PlayerController _instance;

    void Awake()
    {
        _instance = this;
    }

    Vector2 leftInput;
    Vector2 rightInput;


    //checks if button is down
    bool interactDown;
    bool callDown;
    bool dismissDown;


    //calls on first frame
    bool interactTrigger;
    bool callTrigger;
    bool dismissTrigger;

    public void ControllerUpdate()
    {
        GetInputs();
        DongleStuff();
        MovementStuff();

    }


    void GetInputs()
    {
        interactDown = Input.GetButton("Interact");
        callDown = Input.GetButton("Call");

        interactTrigger = Input.GetButtonDown("Interact");
        callTrigger = Input.GetButtonDown("Call");

        leftInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void CheckAbility()
    {


    }
    void DongleStuff()
    {
        Transform temp = PlayerMovement.PlayerInstance.transform.FindChild("Dongle");
        temp.GetComponent<PlayerDongle>().MoveDongle(leftInput * DongleSpeed);
    }

    void MovementStuff()
    {
        Vector2 movements = new Vector2(leftInput.x, leftInput.y);
        
        
        

        
        if (PlayerMovement.PlayerInstance.playerCanMove && ((Mathf.Abs(movements.x) >= MovementDeadzone)||(Mathf.Abs(movements.y) >= MovementDeadzone) ))
        {
            PlayerMovement.PlayerInstance.MoveCharacter(movements * PlayerSpeed);
        }
    }
}
