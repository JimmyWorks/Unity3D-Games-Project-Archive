using UnityEngine;
using System.Collections;

public class JumpAction : MonoBehaviour, IAction
{
    private CharacterController cc;
    private CharacterState cs;
    private CharacterMovement cm;
    private KeyCode defaultKey = KeyCode.Space;
    private bool jumping = false;
    private string id = "Jump";

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        cs = GetComponent<CharacterState>();
        cm = GetComponent<CharacterMovement>();
    }

    public KeyCode getDefaultKey()
    {
        return defaultKey;
    }

    public void Perform()
    {
        if (cc.isGrounded && cm.deltaY < 0)
        {
            Debug.Log("Jumped\n");
            cm.deltaY += cs.jumpForce;
        }
    }

    public string getId()
    {
        return id;
    }
}
