using UnityEngine;
using System.Collections.Generic;


public class Sword : Melee{
    public override void Awake()
    {
        attackDuration = 0.1F; //How long the attack will last
        attackCooldown = 1F; //How long till the player can do another attack
        attackTimer = 0F;     //Timer for Attack Cooldown
        attackTimerDur = 0F;
        attackDmg = 10f;
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        weaponTrigger.enabled = false;
        weaponTrigger.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void Attack()
    {
        weaponTrigger.enabled = true;
    }

    void Update()
    {

    }
}
