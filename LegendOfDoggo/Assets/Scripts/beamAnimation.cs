/// Project: The Legend of Doggo
/// Author: Jimmy Nguyen
/// Email: tbn160230@utdallas.edu | Jimmy@Jimmyworks.net

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Beam animation component which scales the flying saucer beam
/// </summary>
public class beamAnimation : MonoBehaviour {

    // Time before beam shrinks
    public float waitTime = 1f;

    // New scale of the beam
    public float newXScale = 0;

    // Accumulated time
    public float timeElapsed = 0f;

    // Time till beam is destroyed
    public float destroyTimer = 3f;

	/// <summary>
    /// Start intialization routine
    /// </summary>
	void Start () {
        StartCoroutine(shrinkBeam());
	}

    /// <summary>
    /// Update routine
    /// </summary>
    private void Update()
    {
        // Accumulate time until time exceeds the destroy timer
        timeElapsed += Time.deltaTime;
        if (timeElapsed > destroyTimer)
            Destroy(gameObject);
    }

    /// <summary>
    /// Shrink beam coroutine
    /// </summary>
    IEnumerator shrinkBeam()
    {
        // Wait designated time before starting
        yield return new WaitForSeconds(waitTime);

        // While the scale is greater than 0.1f, lerp to 0.1f scale in x-axis
        while(transform.localScale.x != 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 0.1f, 0.5f), transform.localScale.y);
            yield return null;
        } 
    }
}
