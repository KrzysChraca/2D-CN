using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;

	private Transform target;

	void Start(){
		target = GameObject.Find("Player").transform;
	}

	void LateUpdate()
	{
		transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);
	}
}
