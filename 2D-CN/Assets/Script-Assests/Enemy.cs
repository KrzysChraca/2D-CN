using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
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

	public void recieveDamage(float damage){
		health = health - damage;

		Debug.Log (health);

	}

	public void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("Attack")) {

			AttackTrigger dam = other.GetComponent<AttackTrigger>();

			recieveDamage(dam.dmg);

		}

	}
}
