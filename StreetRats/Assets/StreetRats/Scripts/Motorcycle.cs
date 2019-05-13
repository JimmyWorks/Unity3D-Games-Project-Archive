using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle : MonoBehaviour, CameraAnchor {

    public Transform TiltAxis;
    [SerializeField]
    private Transform CameraAnchor;

    Transform CameraAnchor.CameraAnchor()
    {
        return CameraAnchor;
    }
}
