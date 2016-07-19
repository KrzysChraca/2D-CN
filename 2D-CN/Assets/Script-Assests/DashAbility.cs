using UnityEngine;
using System.Collections;

public class DashAbility : playerController  {

	public float dash_Cooldown = .02F;
	public float dash_StartTime = 0F;
	
	public float  dash_Speed = 0.1F;
	
	public bool dashed;
	
	public float dashDistance;
	public float dashIFrames; //invincible frames
	
	public Vector2 savedVelocity;
	public int energyUsed;

	public int currentEnergy;

	public bool canMove;

	public float movespeed;

	void Start(){
		currentEnergy = 100;
	}
	void Update () {


	}

	public void Dash(){
		if (Input.GetKeyDown (KeyCode.Space) && Time.time > dash_StartTime) {
			
			if(energyUsed <= currentEnergy){
				dashed = true;
				canMove = false;
				
				movespeed += dash_Speed;
				var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				targetPos.z = transform.position.z;
				transform.position = Vector3.Lerp(transform.position, targetPos, dash_Speed);
				
				
				dash_StartTime = Time.time + dash_Cooldown;
				
				Debug.Log("Dashing");
				
				energyUsed = 20;
				//useEnergy(energyUsed);
				
				;
				
			}
		}
		if (Time.time > dash_StartTime + .1F && dashed == true) {
			movespeed -= dash_Speed;
			Debug.Log ("Cooldown");
			dashed = false;
			canMove = true;
		}
	}

}


