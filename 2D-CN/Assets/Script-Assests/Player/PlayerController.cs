using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	//---Player_Variables
	private bool canMove;
	public int startingEnergy = 100;
	public int currentEnergy;
	public Slider energySlider;
	public float energyCD = 0.2F;
	public float energyCDStart = 0;
	public int energyUsed;
	public float movespeed = 0.009F;

	//---Dash_Variables
	public float dash_Cooldown = .02F;
	public float dash_StartTime = 0F;
	public float  dash_Speed = 0.1F;
	public bool dashed, dashing;
	public float dashDistance;
	public float dashIFrames; //invincible frames
	public Vector3 dashDirection, dashStart;

	//---Attack_Variables
	public float attackDuration = 0.1F; //How long the attack will last
	public float attackCooldown = 0.2F; //How long till the player can do another attack
	private float attackTimer = 0F;     //Timer for Attack Cooldown
	private float attackTimerDur = 0F;  //Timer for Attack Duration
	private bool attacking = false;
	private bool attacked;
	public Collider2D attackTrigger;
	public Vector3 attackDirection;
    public float attackAngle, 
        attackSpeed,
        attackRotUp,
        attackRotLow,
        attackOffset = 30;


    // Use this for initialization
    void Start () {
		currentEnergy = startingEnergy;
		//energySlider.value = currentEnergy;
		canMove = true;
		attackTrigger.enabled = false;
		attacked = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		//energySlider.value = currentEnergy;

		gainEnergy ();

		Movement ();
		Attack ();
		if(dashing)
			Dash ();
		if (attacking)
			Attacking ();

	}
	public void useEnergy(int amount)
	{
		currentEnergy -= amount;
		Debug.Log (amount + "minus" + currentEnergy);
	}

	public void gainEnergy(){
		if(currentEnergy != 100)
		{
			if(Time.time > energyCDStart) {
				energyCDStart = Time.time + energyCD;
				currentEnergy += 10;
				Debug.Log ("add 10 energy");
			}
		}
	}

	public void Movement(){
			if(canMove){
			if (Input.GetKey (KeyCode.A)) {
				transform.Translate(new Vector3(-movespeed,0,0));
			}
			
			if (Input.GetKey (KeyCode.D)) {
				transform.Translate(new Vector3(movespeed,0,0));
			}
			
			if (Input.GetKey (KeyCode.W)) {
				transform.Translate(new Vector3(0,movespeed,0));
			}
			
			if (Input.GetKey (KeyCode.S)) {
				transform.Translate(new Vector3(0,-movespeed,0));
			}
			if (Input.GetKeyDown (KeyCode.Space) && !dashed && !dashing) {
				StartCoroutine (StartDash());
			}
		}
	}

	public void Attack(){
		if (Input.GetKeyDown (KeyCode.Mouse0) && !attacking && Time.time > attackTimer) {
			attacking = true;

			attackTrigger.enabled = true;

            attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //Get the direction towards the mouse
            attackDirection.z = transform.position.z;
            attackDirection = attackDirection.normalized;

            //Vector3 relativeDirection = attackTrigger.transform.InverseTransformDirection(attackDirection); //Convert the direction into the local space
            //attackAngle = Mathf.Atan2(relativeDirection.y, relativeDirection.x) * Mathf.Rad2Deg;
            attackAngle = Utility._util.RotateTowards(attackDirection, attackTrigger.transform);
            /*if (attackDirection.x < 0)
                attackAngle -= attackOffset;
            else attackAngle += attackOffset;*/
            attackRotLow = attackAngle - attackOffset / 2;
            attackRotUp = attackAngle + attackOffset / 2;
			//Debug.Log(string.Format("Controller angle:{0} and the angle from Utility: {1} \nattackDirection: {2} ", attackAngle, Utility._util.RotateTowards(attackDirection,attackTrigger.transform),relativeDirection));
            attackTrigger.transform.Rotate(0, 0, attackAngle);
            //Debug.DrawLine(attackTrigger.transform.position, attackDirection, Color.red, 2.0f);

            if (attacking) {
				Debug.Log ("Attacking = true");
				attackTimerDur = Time.time + attackCooldown;
				attackTimer = Time.time + attackDuration;
				attacked = true;
			}
		}
		if(Time.time > attackTimerDur + attackDuration && attacked == true) {
			Debug.Log ("Attacking = false");

			attackTrigger.enabled = false;

		}
		if(Time.time > attackTimer + attackCooldown && attacked == true){
			attacked = false;
			attacking = false;
		}
	}

	private void Attacking(){
        //attackTrigger.transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0, 0, attackRotUp), Quaternion.Euler(0, 0, attackRotLow), attackSpeed*Time.deltaTime);
        //attackTrigger.transform.RotateAround (attackTrigger.transform.position, Vector3.forward, -attackSpeed*Time.deltaTime);
    }

	IEnumerator StartDash(){
		dashed = true;

		dashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		dashDirection.z = 0;
        dashStart = transform.position;
		dashing = true;
			
		yield return new WaitForSeconds (dash_Cooldown);
		dashed = false;
	}

	public void Dash(){
		if (Vector2.Distance (dashStart, transform.position) < dashDistance) {
			transform.Translate (dashDirection.normalized * dash_Speed);
		} else
			dashing = false;
	}

	/*public void Dash(){
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
	}*/

}