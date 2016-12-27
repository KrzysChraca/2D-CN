using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public static bool meleeAttacking = false,
                rangeAttacking = false;
    public Vector3 attackDirection, controllerDirection;
    public float attackAngle;
    public int rangedCost, meleeCost;
    private PlayerController player;
    public Transform projectile,
                    projIndicator,
                    meleeAttack;

    void Start()
    {
       player = gameObject.GetComponent<PlayerController>();
    }

    public void SetAttackDirection(bool controller)
    {
        if (controller)
        {
            attackDirection = controllerDirection;
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
        Debug.Log("Started the Melee attack ");
        meleeAttacking = true;
        attackAngle = Utility._util.RotateTowards(attackDirection, meleeAttack.transform);
        meleeAttack.transform.RotateAround(player.transform.position, Vector3.forward, attackAngle - 90);
        StartCoroutine(meleeAttack.GetComponent<Melee>().Attack(attackDirection));
        StartCoroutine(MeleeCooldown());

        //Debug.DrawLine(meleeAttack.transform.position, attackDirection, Color.red, 2.0f);
    }

    public IEnumerator MeleeCooldown()
    {
        yield return new WaitForSeconds(meleeAttack.GetComponent<Melee>().attackCooldown);
        player.actionAvailable = true;
    }

    public void RangedSetup()
    {
        projectile.GetComponent<Projectile>().moveDir = attackDirection;

        Instantiate(projectile,
            player.transform.position, 
            Quaternion.Euler(new Vector3(0, 0, Utility._util.RotateTowards(attackDirection, projectile))));        
        StartCoroutine(RangedCooldown(projectile.GetComponent<Projectile>().fireRate));
    }

    public IEnumerator RangedCooldown(float rangedCooldown)
    {
        yield return new WaitForSeconds(rangedCooldown);
        player.actionAvailable = true;
    }
}