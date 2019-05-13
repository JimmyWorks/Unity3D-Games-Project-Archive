using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private CameraAnchor player;
    private Follower follower;

    private void Start()
    {
        follower = GetComponent<Follower>();
        if (player != null)
            follower.setFollowing(player);
    }

    public void setPlayer(CameraAnchor anchor)
    {
        this.player = anchor;
        if(follower != null)
            follower.setFollowing(anchor);
    }


}
