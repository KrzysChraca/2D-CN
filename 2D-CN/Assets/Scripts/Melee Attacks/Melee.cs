using UnityEngine;
using System.Collections;

public abstract class Melee : MonoBehaviour {

    public float attackDuration = 0.1F,
        attackCooldown = 1F,
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

    public virtual IEnumerator Attack()
    {
        Debug.Log("Attack called");
        yield return new WaitForSeconds(attackDuration);
        attacking = false;
        weaponTrigger.enabled = false;
        PlayerAttack.meleeAttacking = false;
    }

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") || col.name == "Enemy")
        {
            Debug.Log("Hit an enemy " + col.name);
            col.GetComponent<IDamagable>().RecieveDamage(attackDmg);
        }
    }
}
