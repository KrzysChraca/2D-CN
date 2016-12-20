using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float followSpeed, followOffset;
    private Transform target;
    public Transform deadZone;
    // Vector3 followOffset;
    public bool blnTransition, blnFollow, 
        blnFzeBotLetft, blnFzeBotRight, blnFzeTopLeft, blnFzeTopRight;

	void Start(){
        blnFollow = true;
        blnTransition = false;
        followSpeed = 0.025f;
        followOffset = 1.05f;
		target = GameObject.FindGameObjectWithTag("Player").transform;
        
        //followOffset = new Vector3(0, 0, this.transform.position.z);
	}

	void LateUpdate()
	{
        if (blnFollow)
        {
            if (target.GetComponent<PlayerController>().dashing)
                followSpeed = 0.075f;
            else followSpeed = 0.025f;
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), followSpeed);
        }
        else transform.position = new Vector3(target.position.x * followOffset, target.position.y * followOffset, this.transform.position.z);
    }
}
