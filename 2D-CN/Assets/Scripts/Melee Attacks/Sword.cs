using UnityEngine;
using System.Collections;


public class Sword : Melee{
    Vector3 start;
    public PlayerController player;
    float offRot, startAngle, targetRot;

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
        attackDuration = 2.0F; //How long the attack will last
        attackCooldown = 0.5F; //How long till the player can do another attack
        attackDmg = 10;
        attackSpeed = 150f;
        offRot = 80;
        startAngle = targetRot = 0;
    }

    void RotateSword()
    {
        Debug.Log("Target Rotation " + (startAngle + offRot));
        if(gameObject.transform.rotation.z < targetRot)
           transform.RotateAround(player.transform.position, Vector3.forward, Time.deltaTime * -attackSpeed);
    }

    public override void AttackStart(float rotation, Vector3 origin)
    {
        transform.RotateAround(origin, Vector3.forward, rotation);
        
        start = origin;
        startAngle = gameObject.transform.rotation.eulerAngles.z;
        targetRot = startAngle + offRot;
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
