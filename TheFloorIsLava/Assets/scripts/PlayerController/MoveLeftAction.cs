using UnityEngine;

public class MoveLeftAction : MonoBehaviour, IAction
{
    public bool DEBUG;
    private CharacterController cc;
    private CharacterState cs;
    private CharacterMovement cm;
    private KeyCode defaultKey = KeyCode.A;
    private string id = "Left";

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
        if (DEBUG) Debug.Log("Moved left\n");
        cm.deltaX -= cs.speed * Time.deltaTime;
    }

    public string getId()
    {
        return id;
    }
}
