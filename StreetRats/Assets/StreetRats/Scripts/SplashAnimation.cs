using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StreetRats;

public class SplashAnimation : MonoBehaviour {

    Vector3 finalPos;
    float offset = 2000f;
    bool headingRight = true;
    bool waiting = true;
    float next = 0f;
    public float speed = 0.2f;
    public float pauseTime = 1f;
    float lastPause = 2f;

	// Use this for initialization
	void Start () {
        finalPos = transform.position;
        transform.position += new Vector3(-offset, 0, 0);
        offset += finalPos.x;
	}
	
	// Update is called once per frame
	void Update () {
        if(headingRight)
        {
            next = Mathf.Lerp(next, finalPos.x + 10, speed * Time.deltaTime);
            transform.position += Vector3.right * next;

            if(transform.position.x > finalPos.x)
            {
                transform.position = new Vector3(finalPos.x, finalPos.y, finalPos.z);
                headingRight = false;
                next = 0;
            }
        }
        else
        {
            if(waiting)
            {
                next += Time.deltaTime;
                Debug.Log(next);

                if(next > pauseTime)
                {
                    waiting = false;
                    next = 0;
                }
            }
            else
            {
                transform.position += new Vector3(offset, offset, 0) * speed * Time.deltaTime;
                next += Time.deltaTime;
                if (next > lastPause)
                    SceneManager.ProcessEvent(GameTransition.LOAD_MAINMENU);
            }
                
        }
	}
}
