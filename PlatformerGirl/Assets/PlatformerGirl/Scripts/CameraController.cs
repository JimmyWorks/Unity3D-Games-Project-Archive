using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Camera myCamera;
    GameObject mainCharacter;
    public float rotateSpeed = 1f;
    public float defaultX;
    public float defaultY;
    public float defaultZ;

	// Use this for initialization
	void Start () {
        myCamera = GetComponent<Camera>();
        mainCharacter = StageManager.GetPlayer();
        transform.position = mainCharacter.transform.position + new Vector3(defaultX, defaultY, defaultZ);
        transform.LookAt(mainCharacter.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.right, -15f);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log(Input.GetAxis("Mouse X"));
            transform.RotateAround(StageManager.GetPlayer().transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed);
            transform.LookAt(StageManager.GetPlayer().transform.position);
        }
	}
}
