using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(InputManager))]
public class PlayerController : MonoBehaviour {
	//---Player_Variables
	private bool canMove;
    public bool actionAvailable, energyRegain;
	public int startingEnergy = 100;
	public int currentEnergy, energyGainRate, energyMax;
    public Slider energySlider;
	public float energyCD = 2.5F;
	public float energyCDStart = 0;
	public float movespeed = 0.15F;

	//---Dash_Variables
	public float dash_Cooldown = 0.85F;
	private float  dash_Speed = 0.5F;
	public bool dashing;
	public float dashDistance = 4F;
	public float dashIFrames; //invincible frames
	private Vector3 dashDirection, dashStart;

    PlayerAttack pAttack;
    InputManager inputMan;

    void Start () {
        energyMax = startingEnergy;
        energyRegain = false;
        energyGainRate = 1;
        inputMan = gameObject.GetComponent<InputManager>();
        pAttack = gameObject.GetComponent<PlayerAttack>();
		currentEnergy = startingEnergy;
		//energySlider.value = currentEnergy;
		canMove = true;
        actionAvailable = true;
        GameManager.GetInstance.enAmount = currentEnergy;
    }

	void Update () {
        //energySlider.value = currentEnergy;
    }

    void FixedUpdate()
    {
        Movement();
        if (currentEnergy < energyMax && !energyRegain)
            StartCoroutine(EnergyRepletion());
        if (dashing)
            Dash();
    }

    public IEnumerator EnergyRepletion()
    {
        energyRegain = true;
        yield return new WaitForSeconds(energyCD);
        if (currentEnergy != energyMax)
        {
            currentEnergy += energyGainRate;
            energyRegain = true;
            GameManager.GetInstance.enAmount = currentEnergy;
        }
        energyRegain = false;
    }

	public void useEnergy(int amount)
	{
		currentEnergy -= amount;
        GameManager.GetInstance.enAmount = currentEnergy;
    }

	public void gainEnergy(int amount){
        currentEnergy += amount;
        GameManager.GetInstance.enAmount = currentEnergy;
    }

	public void Movement(){
		if(canMove){
            if(inputMan.moveVector.x != 0 || inputMan.moveVector.y != 0)
                transform.Translate(inputMan.moveVector*movespeed);

            if (inputMan.dashPressed && !dashing)
            {
                StartCoroutine(StartDash());
            }

            if (inputMan.meleePressed && actionAvailable)
            {
                actionAvailable = false;
                if (inputMan.controllerActive)
                    pAttack.controllerDirection = inputMan.controllerAttackDirection;
                pAttack.SetAttackDirection(inputMan.controllerActive);
                pAttack.MeleeSetup();
            }

            if (inputMan.rangedPressed && actionAvailable)
            {
                actionAvailable = false;
                if(inputMan.controllerActive)
                    pAttack.controllerDirection = inputMan.controllerAttackDirection;
                pAttack.SetAttackDirection(inputMan.controllerActive);
                pAttack.RangedSetup();
            }
        }
	}

	IEnumerator StartDash(){
        if (currentEnergy - 10 > 0)
        {
            actionAvailable = false;
            if (inputMan.controllerActive)
                dashDirection = inputMan.controllerAttackDirection;
            else dashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            dashDirection.z = 0;
            dashStart = transform.position;
            dashing = true;
            useEnergy(10);
            yield return new WaitForSeconds(dash_Cooldown);
        }
            
        else Debug.Log("Not enough energy to dash");
        yield return new WaitForSeconds(dash_Cooldown/2);
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

    //for locating where player is
    private static Location location;
    public static Location GetLocation
    {
        get { return location; }
    }
}