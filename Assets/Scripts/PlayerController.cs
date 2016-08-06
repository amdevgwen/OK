using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float MovementDeadzone; //deadzone so that the player doesn't move until the input hits this threshhold, this allows players to move the dongle.
    public float PlayerSpeed;
    public float DongleSpeed;

    public bool isCalling;


    Vector2 leftInput;
    Vector2 rightInput;


    //checks if button is down
    bool button1Down;
    bool button2Down;
    bool button3Down;


    //calls on first frame
    bool button1Trigger;
    bool button2Trigger;
    bool button3Trigger;

    void Update()
    {
        GetInputs();
        DongleStuff();
        MovementStuff();

    

    }


    void GetInputs()
    {
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
        Vector2 movements = new Vector2();
        //if (leftInput.x >= MovementDeadzone)
       // {
            movements.x = leftInput.x;
       // }
       // if (leftInput.y >= MovementDeadzone)
       // {
            movements.y = leftInput.y;
        //}
        if (PlayerMovement.PlayerInstance.playerCanMove)
        {
            PlayerMovement.PlayerInstance.MoveCharacter(movements * PlayerSpeed);
        }
    }
}
