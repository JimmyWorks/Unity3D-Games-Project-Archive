using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private GameObject player;
    private static GameObject cameraGO;
    private PlayerCamera playerCamera;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        cameraGO = (GameObject)Instantiate(Resources.Load("Prefabs/Player Camera"));
        cameraGO.name = "Player Camera";
        cameraGO.transform.parent = transform.root.parent;

        playerCamera = cameraGO.GetComponent<PlayerCamera>();
        Motorcycle motorcycle = player.GetComponent<Motorcycle>();
        playerCamera.setPlayer(motorcycle);
    }

    public static void DestroyCamera()
    {
        Destroy(cameraGO);
    }

}
