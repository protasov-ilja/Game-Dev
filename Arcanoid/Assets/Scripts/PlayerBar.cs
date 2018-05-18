using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBar : MonoBehaviour {
    public float barSpeed = 1f;
    public Vector3 playerPos = new Vector3 (0, -3.5f, 0);
	
	void Update ()
    {
        float xPos = transform.position.x + (Input.GetAxis("Horizontal") * barSpeed);
        playerPos = new Vector3 (Mathf.Clamp(xPos, -8f, 8f), -3.5f, 0);
        transform.position = playerPos;
	}
}
