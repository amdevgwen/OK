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
    [SerializeField] bool interactDown, callDown, dismissDown;


    //calls on first frame
    [SerializeField]
    bool interactTrigger, callTrigger, dismissTrigger;

    public void ControllerUpdate()
    {
        GetInputs();
        DongleStuff();
        MovementStuff();

    }

    public Quaternion rotationsjunk;
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
        Vector3 junk = new Vector3(leftInput.x, 0, leftInput.y);
        junk = Quaternion.Euler(0, rotationsjunk.eulerAngles.y, 0) * junk;
        Vector2 movements = new Vector2(junk.x, junk.z);

        

        Transform temp = PlayerMovement.PlayerInstance.transform.FindChild("Dongle");
        if (callDown)
        {
            temp.GetComponent<PlayerDongle>().DoWhistle();
            
        }
        temp.GetComponent<PlayerDongle>().MoveDongle(movements * DongleSpeed);
    }

    void MovementStuff()
    {
        Vector3 junk = new Vector3(leftInput.x, 0, leftInput.y);
        junk = Quaternion.Euler(0, rotationsjunk.eulerAngles.y, 0) * junk;

        Vector2 movements = new Vector2(junk.x, junk.z);
        
        
        

        
        if (PlayerMovement.PlayerInstance.playerCanMove && ((Mathf.Abs(movements.x) >= MovementDeadzone)||(Mathf.Abs(movements.y) >= MovementDeadzone) ))
        {
            PlayerMovement.PlayerInstance.MoveCharacter(movements * PlayerSpeed);
        }
    }
}
