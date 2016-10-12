using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {

    public Vector3 moveDir;
    public int speed,
            damage;
    public float deathTimer = 5,
        attackDelay = 0,
        fireRate = 0.5f;

    public virtual void Start()
    {
        //Physics2D.IgnoreLayerCollision(8, 9);
    }

    public virtual IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") || col.name == "Enemy")
        {
            Debug.Log("Hit an enemy " + col.name);
            col.GetComponent<IDamagable>().RecieveDamage(damage);
        }
    }
}
