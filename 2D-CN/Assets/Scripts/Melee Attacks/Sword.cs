using UnityEngine;
using System.Collections;


public class Sword : Melee{
    public override void Awake()
    {
        attackDuration = 0.6F; //How long the attack will last
        attackCooldown = 0.5F; //How long till the player can do another attack
        attackDmg = 10f;
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        weaponTrigger.enabled = false;
        weaponTrigger.GetComponent<SpriteRenderer>().enabled = false;
        //Debug.Log(string.Format("The sword trigger is {0}", weaponTrigger.name));
    }

    public override IEnumerator Attack(Vector3 atkDir)
    {
        weaponTrigger.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attacking = false;    
        weaponTrigger.GetComponent<SpriteRenderer>().enabled = false;
        weaponTrigger.enabled = false;
        PlayerAttack.meleeAttacking = false;
    }
}
