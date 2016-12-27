using UnityEngine;
using System.Collections;


public class Sword : Melee{
    Quaternion offRotation;
    Vector3 start;
    float offRot;

    public override void Awake()
    {
        attackDuration = 1.0F; //How long the attack will last
        attackCooldown = 0.5F; //How long till the player can do another attack
        attackDmg = 10f;
        attackSpeed = 10f;
        offRot = 45;
        base.Awake();
    }

    void Update()
    {
        //if (attacking)
            //transform.RotateAround(start, Vector3.forward, -offRot * Time.deltaTime * attackSpeed);
    }

    public override void Start()
    {
        base.Start();
        weaponTrigger.enabled = false;
        weaponTrigger.GetComponent<SpriteRenderer>().enabled = false;
        //Debug.Log(string.Format("The sword trigger is {0}", weaponTrigger.name));
    }

    public override void AttackStart(float rotation, Vector3 origin)
    {
        transform.RotateAround(origin, Vector3.forward, rotation-90);
        start = origin;
        offRotation = Quaternion.Euler(0,0,90);
    }

    public override IEnumerator Attack(Vector3 atkDir)
    {
        attacking = true;
        weaponTrigger.enabled = true;
        weaponTrigger.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attacking = false;    
        weaponTrigger.GetComponent<SpriteRenderer>().enabled = false;
        weaponTrigger.enabled = false;
        PlayerAttack.meleeAttacking = false;
    }
}
