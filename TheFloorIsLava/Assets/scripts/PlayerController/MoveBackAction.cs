using UnityEngine;

public class MoveBackAction : MonoBehaviour, IAction
{
    public bool DEBUG;
    private CharacterController cc;
    private CharacterState cs;
    private CharacterMovement cm;
    private KeyCode defaultKey = KeyCode.S;
    private string id = "Back";

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
        if (DEBUG) Debug.Log("Moved back\n");
        cm.deltaZ -= cs.speed * Time.deltaTime;
    }

    public string getId()
    {
        return id;
    }
}
