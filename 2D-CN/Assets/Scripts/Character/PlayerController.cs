using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(InputSupervisor))]
public class PlayerController : MonoBehaviour, IDamagable {
    //---Player_Variables
    public int startingHealth = 100;
    public int currentHealth;
    bool isDead, damaged;
    private bool canMove;
    public bool actionAvailable, energyRegain;
	public int startingEnergy = 100;
	public int currentEnergy, energyGainRate, energyMax;
    public Slider energySlider;
	public float energyCD = 2.5F;
	public float energyCDStart = 0;
	public float movespeed = 0.15F;
    public bool invincible = false;
	//---Dash_Variables
	public float dash_Cooldown = 0.85F;
	private float  dash_Speed = 0.5F;
	public bool dashing;
	public float dashDistance = 4F;
	private Vector3 dashDirection, dashStart;

    //---Attack_Variables
    public Vector3 attackDirection, controllerDirection;
    public float attackAngle;
    public int rangedCost, meleeCost;
    public Transform projectile,
                    meleeAttack;

    public bool Networked = false;

    InputSupervisor inputMan;
    public GameObject FollowCamera;

    void Start()
    {
        energyMax = startingEnergy;
        energyRegain = false;
        energyGainRate = 1;
        inputMan = gameObject.GetComponent<InputSupervisor>();
        currentEnergy = startingEnergy;
        //energySlider.value = currentEnergy;
        canMove = true;
        actionAvailable = true;
        GameManager.GetInstance.enAmount = currentEnergy;
        if (!Networked)
        { 
            FollowCamera = GameObject.FindGameObjectWithTag("MainCamera");
            FollowCamera.GetComponent<CameraMovement>().target = this.gameObject.transform;
        }
    }

	void Update () {
        //energySlider.value = currentEnergy;
    }

    void FixedUpdate()
    {
        if (!Networked)
        {
            Movement();
            if (currentEnergy < energyMax && !energyRegain)
                StartCoroutine(EnergyRepletion());
            if (dashing)
                Dash();
        }
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
                    controllerDirection = CheckInputVector();

                SetAttackDirection(inputMan.controllerActive);
                MeleeSetup();
            }

            if (inputMan.rangedPressed && actionAvailable)
            {
                actionAvailable = false;
                if(inputMan.controllerActive)
                    controllerDirection = CheckInputVector();

                SetAttackDirection(inputMan.controllerActive);
                RangedSetup();
            }
        }
	}

    Vector3 CheckInputVector()
    {
        if (inputMan.controllerAttackDirection != Vector3.zero)
            return inputMan.controllerAttackDirection;
        //else if (inputMan.moveVector != Vector3.zero)
        //    return inputMan.moveVector;
        else return inputMan.lastInputDirection;
    }

    #region Energy
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

    public void gainEnergy(int amount)
    {
        currentEnergy += amount;
        GameManager.GetInstance.enAmount = currentEnergy;
    }
    #endregion

    #region Dash
    IEnumerator StartDash(){
        if (currentEnergy - 10 > 0)
        {
            actionAvailable = false;
            StartCoroutine(Invincibility(0.025f, 0.085f));
            if (inputMan.controllerActive)
                dashDirection = CheckInputVector();
            else dashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            dashDirection.z = 0;
            dashStart = transform.position;
            dashing = true;
            useEnergy(10);
        }
            
        else Debug.Log("Not enough energy to dash");
        yield return new WaitForSeconds(dash_Cooldown);
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
    #endregion

    #region Attack
    public void SetAttackDirection(bool controller)
    {
        if (controller)
        {
            attackDirection = CheckInputVector();
        }
        else attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //Get the direction towards the mouse
        attackDirection.z = transform.position.z;
        attackDirection = attackDirection.normalized;
    }

    public void SetMeleeWeapon(Transform currWeapon)
    {
        meleeAttack = currWeapon;
    }

    public void SetRangedWeapon(Transform currWeapon)
    {
        projectile = currWeapon;
    }

    public void MeleeSetup()
    {
        attackAngle = Utility._util.RotateTowards(attackDirection, meleeAttack.transform);
        //StartCoroutine(meleeAttack.GetComponent<Melee>().Attack(attackDirection));
        meleeAttack.GetComponent<Melee>().AttackStart(attackAngle, this.transform.position);
        //meleeAttack.transform.RotateAround(transform.position, Vector3.forward, attackAngle);
        StartCoroutine(MeleeCooldown());

        //Debug.DrawLine(meleeAttack.transform.position, attackDirection, Color.red, 2.0f);
    }

    public IEnumerator MeleeCooldown()
    {
        yield return new WaitForSeconds(meleeAttack.GetComponent<Melee>().attackCooldown);
        actionAvailable = true;
    }

    public void RangedSetup()
    {
        projectile.GetComponent<Projectile>().moveDir = attackDirection;

        Instantiate(projectile,
            transform.position,
            Quaternion.Euler(new Vector3(0, 0, Utility._util.RotateTowards(attackDirection, projectile))));
        StartCoroutine(RangedCooldown(projectile.GetComponent<Projectile>().fireRate));
    }

    public IEnumerator RangedCooldown(float rangedCooldown)
    {
        yield return new WaitForSeconds(rangedCooldown);
        actionAvailable = true;
    }
    #endregion

    IEnumerator Invincibility(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }

    public void RecieveDamage(int dmg)
    {
        if (!invincible)
            currentHealth -= dmg;
        if (currentHealth <= 0 && !isDead)
            isDead = true;
    }

    //for locating where player is
    private static Location location;
    public static Location GetLocation
    {
        get { return location; }
    }
}