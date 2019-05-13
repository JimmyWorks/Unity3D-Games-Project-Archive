using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour {

    private PlayerHUD playerHUD;

	// Use this for initialization
	void Start () {
        playerHUD = GameObject.FindGameObjectWithTag("PlayerHUD").GetComponent<PlayerHUD>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            playerHUD.checkpointON();
    }
}
