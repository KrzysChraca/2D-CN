using UnityEngine;
using System.Collections;

public abstract class Melee : MonoBehaviour {

    public float attackDuration = 0.1F,
        attackCooldown = 1F,
        attackSpeed = 5f;
    public int attackDmg = 1;
    public bool attacking;
    public Collider2D weaponTrigger;

    public virtual void Awake()
    {
        attacking = false;
    }

    public virtual void Start()
    {
        if (gameObject.GetComponent<Collider2D>())
            weaponTrigger = this.gameObject.GetComponent<Collider2D>();
        else
        {
            Debug.Log(string.Format("Missing collider for: {0}", gameObject.name));
            weaponTrigger = gameObject.AddComponent<PolygonCollider2D>();
        }
    }

    public virtual void AttackStart(float rotation, Vector3 origin)
    {
        Debug.Log("Starting attack");
    }

    public virtual IEnumerator Attack()
    {
        //Debug.Log("Attacking in this direction " + atkDir);
        yield return new WaitForSeconds(attackDuration);
        attacking = false;
        weaponTrigger.enabled = false;
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
