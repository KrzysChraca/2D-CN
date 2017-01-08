using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour, IDamagable {
	public float health = 100;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Death ();

	}

	public void Death(){
		if (health <= 0) {
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter2D(Collider2D other){
        //Debug.Log("Collided with " + other.name);

		//if (other.gameObject.CompareTag ("Attack")) {

		//	RecieveDamage(other.GetComponent<AttackTrigger>().dmg);
		//}

        if(other.gameObject.CompareTag("Projectile"))
        {
            RecieveDamage(other.GetComponent<Projectile>().damage);
        }
	}

    public void RecieveDamage(int dmg)
    {
        health = health - dmg;

        //Debug.Log(health);
    }
}
