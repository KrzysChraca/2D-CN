using UnityEngine;
using System.Collections;

public class PlayerCharController : ObjectScript {

	public float dashDistance;//Maximum distance the actor will travel
	public float dashSpeed;//Speed at which the actor will dodge
	private float dashTicker;
	public float dashIFrames;//Time for invincible dodge frames

	private bool attacked = true;

	private Animator myAnimator;

	private float baseAcceleration;
	private float baseTurnSpeed;
	private float baseSpeed;



	// Use this for initialization
	void Start () {
		//base.Start();

		navAgentRef = transform.GetComponent<NavMeshAgent>();
		myAnimator = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(flagStunned.value)
		{
			navAgentRef.speed = 0;
			moveSpeed.value = 0;
		}

	/*	if (myState == AIStates.Dashing)
		{
			//SUPER SPEED
			navAgentRef.speed = dashSpeed;
			navAgentRef.acceleration = baseAcceleration*5;
			navAgentRef.angularSpeed = baseTurnSpeed*5;
			dashTicker = dashTicker - Time.deltaTime;
			
			if(target != null && (target.transform.position-transform.position).magnitude<attackDistance)
			{
				dashTicker = 0;
			}
			if(dashTicker <= 0)
				endDash();
		}
*/

	}


	public void beginDash(Vector3 dashDestination)
	{

		dashTicker = dashIFrames;
		navAgentRef.destination = dashDestination;
		baseAcceleration = navAgentRef.acceleration;
		baseTurnSpeed = navAgentRef.angularSpeed;
		baseSpeed = navAgentRef.speed;
	}
	public void endDash()
	{
		navAgentRef.acceleration = baseAcceleration;
		navAgentRef.angularSpeed = baseTurnSpeed;
		navAgentRef.speed = baseSpeed;
		//If there are no targets, go into roaming. If my target still alive, continue attacking

	}
}
