using UnityEngine;
using System.Collections;


public class Sword : Melee{
    public PlayerController player;
    float offSet, startAngle, targetAngle, endAngle;
    public float lastStep, smoothStep, step;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        weaponTrigger.enabled = false;
        weaponTrigger.GetComponent<SpriteRenderer>().enabled = false;
        attackDuration = 1.0F; //How long the attack will last
        attackCooldown = 0.5F; //How long till the player can do another attack
        attackDmg = 10;
        attackSpeed = 7.5f;
        targetAngle = 100;
        offSet = targetAngle / 2;
        startAngle =  endAngle = 0;
    }

    IEnumerator RotateSword()
    {
        step = smoothStep = lastStep = 0;
        while (step < 2)
        {
            step += Time.deltaTime * attackSpeed; //unsmoothe rate at which the rotation happens
            smoothStep = Mathf.SmoothStep(0, 1, step);
            transform.RotateAround(player.transform.position, Vector3.forward, endAngle * (smoothStep - lastStep)); //gets the smooth step
            lastStep = smoothStep; 
            yield return null;
        }
        if (step > 2)
        {
            transform.RotateAround(player.transform.position, Vector3.forward, endAngle * (1 - lastStep));
            StartCoroutine(Attack());
        }
            
    }

    public override void AttackStart(float rotation, Vector3 origin)
    {
        attacking = true;
        weaponTrigger.enabled = true;
        weaponTrigger.GetComponent<SpriteRenderer>().enabled = true;

        transform.RotateAround(origin, Vector3.forward, rotation + offSet);
        startAngle = gameObject.transform.rotation.z;
        endAngle = startAngle - targetAngle;

        StartCoroutine(RotateSword());
    }

    public override IEnumerator Attack()
    {
        yield return new WaitForEndOfFrame();
        attacking = false;
        weaponTrigger.GetComponent<SpriteRenderer>().enabled = false;
        weaponTrigger.enabled = false;
        PlayerAttack.meleeAttacking = false;
    }
}
