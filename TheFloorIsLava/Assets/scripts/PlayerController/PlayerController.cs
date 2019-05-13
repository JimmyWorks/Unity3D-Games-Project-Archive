using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Dictionary<KeyCode, Command> keyToCommand;

    private void Start()
    {
        Debug.Log("Creating Player Controller");
        keyToCommand = new Dictionary<KeyCode, Command>();
        initializeCommands();
        
    }

    private void initializeCommands()
    {
        Debug.Log("Initializing Player Controller...");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        IAction[] actions = new IAction[10];
        actions = player.GetComponents<IAction>();

        foreach (IAction action in actions)
        {

            Debug.Log("Found" + action.getId() + "\n");
            Command command = new Command(action);
            addCommand(command, action.getDefaultKey());
        }




    }

    public void addCommand(Command action, KeyCode key)
    {
        keyToCommand.Add(key, action);
    }

    public void removeCommand(KeyCode key)
    {
        if (keyToCommand.ContainsKey(key))
            keyToCommand.Remove(key);
        else
        {
            string msg = "Failed to remove action bound to " + key.ToString() + "\n";
            Debug.Log(msg);
        }
    }

    private void Update()
    {
        executePlayerInput();
    }
    public void executePlayerInput()
    {
        foreach(var tuple in keyToCommand)
        {
            if(Input.GetKey(tuple.Key))
                tuple.Value.Execute();
        }
    }

}
