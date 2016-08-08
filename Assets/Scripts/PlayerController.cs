using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    [SerializeField] bool interactDown, callDown, dismissDown, sendDown;
    public Transform PlayerModel;

    //calls on first frame
    [SerializeField]
    bool interactTrigger, callTrigger, dismissTrigger, sendTrigger;

    public void ControllerUpdate()
    {
        GetInputs();
        DongleStuff();
        TargetStuff();
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

    public Transform targetReticle;

    bool lastframe;

    List<MinionController> heldDown;
    void TargetStuff()
    {
        heldDown = GameMain.instance.CurrentMinions;

        if (Input.GetButton("SendButton"))
        {
            lastframe = true;
            targetReticle.transform.SetParent(PlayerMovement.PlayerInstance.transform.FindChild("Dongle"));
            targetReticle.transform.localPosition = Vector3.zero;
            targetReticle.gameObject.SetActive(true);
            if (GameMain.instance.CurrentMinions.Count != 0)
            {
                foreach (MinionController k in heldDown.ToArray())
                {
                    k.SendToTarget(targetReticle.position);
                } 
            }
        }
        else if(lastframe)
        {
            lastframe = false;
            if (heldDown.Count != 0)
            {
                foreach (MinionController k in heldDown.ToArray())
                {
                    GameMain.instance.RemoveMinion(k.transform);
                }
                heldDown = new List<MinionController>();
            }
            StartCoroutine(waitTarget());
        }
    }
    
    IEnumerator waitTarget()
    {
        targetReticle.SetParent(transform.parent);
        yield return new WaitForSeconds(1.5f);

        // I don't know why this would not be true, but it's preventing the dongle from reseting.
        //if (targetReticle.parent == transform.parent)
        //{
            targetReticle.gameObject.SetActive(false);
            targetReticle.SetParent(PlayerMovement.PlayerInstance.transform.FindChild("Dongle"));
            targetReticle.transform.localPosition = Vector3.zero;
        //}
    }

    void MovementStuff()
    {
        Vector3 junk = new Vector3(leftInput.x, 0, leftInput.y);
        junk = Quaternion.Euler(0, rotationsjunk.eulerAngles.y, 0) * junk;

        Vector2 movements = new Vector2(junk.x, junk.z);
        PlayerMovement.PlayerInstance.transform.FindChild("Dongle");
        

        
    
        
        

        
        if (PlayerMovement.PlayerInstance.playerCanMove && ((Mathf.Abs(movements.x) >= MovementDeadzone)||(Mathf.Abs(movements.y) >= MovementDeadzone) ))
        {
            PlayerModel.GetComponent<KnightAnim>().isMoving = true;
            
            Vector3 temppos = PlayerMovement.PlayerInstance.transform.FindChild("Dongle").position;
            temppos.y = PlayerModel.position.y;
            Vector3 relativePos = temppos - PlayerModel.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            PlayerModel.rotation = rotation;
            PlayerMovement.PlayerInstance.MoveCharacter(movements * PlayerSpeed);
        }
        else
        {
            PlayerModel.GetComponent<KnightAnim>().isMoving = false;
        }
    }
}
