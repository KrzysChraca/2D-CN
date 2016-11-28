using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerAttack))]
public class PlayerController : MonoBehaviour {
	//---Player_Variables
	private bool canMove;
    public bool actionAvailable;
	public int startingEnergy = 100;
	public int currentEnergy;
	public Slider energySlider;
	public float energyCD = 0.2F;
	public float energyCDStart = 0;
	public int energyUsed;
	public float movespeed = 0.15F;

	//---Dash_Variables
	public float dash_Cooldown = .25F;
	public float dash_StartTime = 0F;
	public float  dash_Speed = 0.5F;
	public bool dashed, dashing;
	public float dashDistance = 4F;
	public float dashIFrames; //invincible frames
	public Vector3 dashDirection, dashStart;

    PlayerAttack pAttack;

    // Use this for initialization
    void Start () {
        pAttack = gameObject.GetComponent<PlayerAttack>();
		currentEnergy = startingEnergy;
		//energySlider.value = currentEnergy;
		canMove = true;
        actionAvailable = true;
	}
	
	// Update is called once per frame
	void Update () {
		//energySlider.value = currentEnergy;

		gainEnergy ();

		Movement ();

        if (dashing)
			Dash ();
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

            if (Input.GetKeyDown(KeyCode.Mouse0) && actionAvailable)
            {
                actionAvailable = false;
                pAttack.MeleeSetup();   
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && actionAvailable)
            {
                actionAvailable = false;
                pAttack.RangedSetup();
            }
        }
	}

	IEnumerator StartDash(){
		dashed = true;
        actionAvailable = false;
		dashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		dashDirection.z = 0;
        dashStart = transform.position;
		dashing = true;
			
		yield return new WaitForSeconds (dash_Cooldown);
		dashed = false;
        actionAvailable = true;
	}

	public void Dash()
    {
        if (Vector2.Distance(dashStart, transform.position) < dashDistance)
        {
            canMove = false;
            transform.Translate(dashDirection.normalized * dash_Speed);
        } else
        {
            dashing = false;
            canMove = true;
        }
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

    //for locating where player is
    private static Location location;
    public static Location GetLocation
    {
        get { return location; }
    }
}