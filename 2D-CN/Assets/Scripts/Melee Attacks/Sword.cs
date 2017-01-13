﻿using UnityEngine;
using System.Collections;


public class Sword : Melee{
    Quaternion offRotation;
    Vector3 start;
    public PlayerController player;
    float offRot, startAngle, targetAngle;

    public override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if (attacking)
            RotateSword();
    }

    public override void Start()
    {
        base.Start();
        weaponTrigger.enabled = false;
        weaponTrigger.GetComponent<SpriteRenderer>().enabled = false;
        attackDuration = 5.0F; //How long the attack will last
        attackCooldown = 0.5F; //How long till the player can do another attack
        attackDmg = 10;
        attackSpeed = 180f;
        offRot = 100;
        startAngle = targetAngle = 0;
        //Debug.Log(string.Format("The sword trigger is {0}", weaponTrigger.name));
    }

    void RotateSword()
    {
        Debug.Log("Target Rotation " + (targetAngle));
        if(gameObject.transform.rotation.z <= targetAngle)
            transform.RotateAround(player.transform.position, Vector3.forward, Time.deltaTime * -attackSpeed);
    }

    public override void AttackStart(float rotation, Vector3 origin)
    {
        transform.RotateAround(origin, Vector3.forward, rotation-90);

        start = origin;
        startAngle = gameObject.transform.rotation.eulerAngles.z;
        targetAngle = (offRot + startAngle)%360;
        Debug.Log("Starting angle " + startAngle);
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
