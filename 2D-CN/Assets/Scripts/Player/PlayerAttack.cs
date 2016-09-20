using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public float attackDuration = 0.1F; //How long the attack will last
    public float attackCooldown = 1F; //How long till the player can do another attack
    public float attackTimer = 0F;     //Timer for Attack Cooldown
    public float attackTimerDur = 0F;  //Timer for Attack Duration
    public bool meleeAttacking = false;
    public bool attacked;
    public Collider2D attackTrigger;
    public Vector3 attackDirection;
    public float attackAngle,
        attackSpeed,
        attackRotUp,
        attackRotLow,
        attackOffset = 30;
    private PlayerController player;

    void Start()
    {
       player = gameObject.GetComponent<PlayerController>();
    }

    public void MeleeSetup()
    {
        meleeAttacking = true;

        attackTrigger.enabled = true;

        attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //Get the direction towards the mouse
        attackDirection.z = transform.position.z;
        attackDirection = attackDirection.normalized;

        attackAngle = Utility._util.RotateTowards(attackDirection, attackTrigger.transform);
        attackRotLow = attackAngle - attackOffset / 2;
        attackRotUp = attackAngle + attackOffset / 2;
        attackTrigger.transform.Rotate(0, 0, attackAngle);
        //Debug.Log(string.Format("Controller angle:{0} and the angle from Utility: {1} ", attackAngle, Utility._util.RotateTowards(attackDirection,attackTrigger.transform)));
        //Debug.DrawLine(attackTrigger.transform.position, attackDirection, Color.red, 2.0f);
        if (meleeAttacking)
        {
            Debug.Log("Attacking = true");
            attackTimerDur = Time.time + attackDuration;
            attackTimer = Time.time + attackCooldown;
            attacked = true;
        }
    }

    public void MeleeDuration()
    {
        if (Time.time > attackTimer + attackCooldown && attacked == true)
        {
            attackTrigger.enabled = false;
            Debug.Log("Attacking = false");
            attacked = false;
            meleeAttacking = false;
            player.actionAvailable = true;
        }
    }

    private void Attacking()
    {
        //attackTrigger.transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0, 0, attackRotUp), Quaternion.Euler(0, 0, attackRotLow), attackSpeed*Time.deltaTime);
        //attackTrigger.transform.RotateAround (attackTrigger.transform.position, Vector3.forward, -attackSpeed*Time.deltaTime);
    }
}