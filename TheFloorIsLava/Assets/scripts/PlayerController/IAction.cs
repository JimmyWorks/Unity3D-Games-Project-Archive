using UnityEngine;

public interface IAction
{
    void Perform();
    KeyCode getDefaultKey();
    string getId();
}
