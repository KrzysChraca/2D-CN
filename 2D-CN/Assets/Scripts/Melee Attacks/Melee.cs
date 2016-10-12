using UnityEngine;
using System.Collections;

public abstract class Melee : MonoBehaviour {

    public float attackDuration = 0.1F,
        attackCooldown = 1F,
        attackTimer = 0F,
        attackTimerDur = 0F,
        attackDmg = 1f;
    public bool attacking;
    public Collider2D weaponTrigger;
    public Vector3 attackDirection;

    public virtual void Awake()
    {
        attacking = false;
    }

    public virtual void Start()
    {
        if (gameObject.GetComponent<Collider2D>())
            weaponTrigger = this.gameObject.GetComponent<Collider2D>();
        else weaponTrigger = gameObject.AddComponent<PolygonCollider2D>();
    }

    public virtual void Attack()
    {
        Debug.Log("Attack called");
    }

}
