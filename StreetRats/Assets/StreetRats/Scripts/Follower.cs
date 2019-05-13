using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

    public float smoothing;
    private CameraAnchor target = null;

    public void setFollowing(CameraAnchor target)
    {
        this.target = target;
    }
	
    public void stopFollowing()
    {
        target = null;
    }

	// Update is called once per frame
	void FixedUpdate () {
		if(target != null  && (transform.position - target.CameraAnchor().position).magnitude > smoothing)
        {
            transform.position = target.CameraAnchor().position;
            transform.rotation = target.CameraAnchor().rotation;
        }
	}
}
