using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public float attackDuration = 0.1F; //How long the attack will last
    public float attackCooldown = 1F; //How long till the player can do another attack
    public float attackTimer = 0F;     //Timer for Attack Cooldown
    public float attackTimerDur = 0F;  //Timer for Attack Duration
    public bool meleeAttacking = false,
                rangeAttacking = false;
    public Vector3 attackDirection;
    public float attackAngle,
        attackSpeed,
        attackRotUp,
        attackRotLow,
        rangedCooldown = 0.5f;
    private PlayerController player;
    public Transform projectile,
                    projIndicator,
                    meleeAttack;

    void Start()
    {
       player = gameObject.GetComponent<PlayerController>();
    }

    private void SetAttackDirection()
    {
        attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //Get the direction towards the mouse
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
        meleeAttacking = true;
        meleeAttack.GetComponent<Collider2D>().enabled = true;
        meleeAttack.GetComponent<SpriteRenderer>().enabled = true;

        SetAttackDirection();

        attackAngle = Utility._util.RotateTowards(attackDirection, meleeAttack.transform);
        meleeAttack.transform.Rotate(new Vector3(0, 0, player.transform.position.z), attackAngle);
        //Debug.Log(string.Format("Controller angle:{0} and the angle from Utility: {1} ", attackAngle, Utility._util.RotateTowards(attackDirection,attackTrigger.transform)));
        //Debug.DrawLine(attackTrigger.transform.position, attackDirection, Color.red, 2.0f);
        if (meleeAttacking)
        {
            Debug.Log("Attacking = true");
            meleeAttack.GetComponent<Melee>().attackTimerDur = Time.time + attackDuration;
            meleeAttack.GetComponent<Melee>().attackTimer = Time.time + attackCooldown;
        }
    }

    public void MeleeDuration()
    {
        if (Time.time > attackTimer + attackCooldown && meleeAttacking == true)
        {
            meleeAttack.GetComponent<Melee>().weaponTrigger.enabled = false;
            Debug.Log("Attacking = false");
            meleeAttacking = false;
            player.actionAvailable = true;
        }
    }
    
    public void RangedSetup()
    {
        SetAttackDirection();
        attackAngle = Utility._util.RotateTowards(attackDirection, projIndicator.transform);
        projIndicator.transform.Rotate(0,0, attackAngle); //TODO make sure the indicator rotates around the player
        //Debug.Log("The attack angle for the porjectile indicicator: " + attackAngle);

        projectile.GetComponent<Projectile>().moveDir = attackDirection;

        Instantiate(projectile,
            new Vector3(projIndicator.transform.position.x, projIndicator.transform.position.y, projIndicator.transform.position.z), 
            Quaternion.Euler(new Vector3(0, 0, Utility._util.RotateTowards(attackDirection, projectile))));
        rangedCooldown = projectile.GetComponent<Projectile>().fireRate;
        StartCoroutine(RangedCooldown());
    }

    public IEnumerator RangedCooldown()
    {
        yield return new WaitForSeconds(rangedCooldown);
        player.actionAvailable = true;
    }
}