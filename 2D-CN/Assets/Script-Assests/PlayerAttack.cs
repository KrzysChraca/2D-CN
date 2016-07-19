using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	//----- Attack Variables-------
	
	//float range = 1.8F;
	float attackCooldown = 0.05F;
	private float attackTimer = 0F;

	private bool attacking = false;

	public Collider2D attackTrigger;

	private bool attacked;

	//private Animator anim;

	// Use this for initialization
	void Start () {

		//anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
		attacked = false;
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.Mouse0) && !attacking && Time.time > attackTimer) {
			attacking = true;
			Debug.Log ("Mouse pressed");
			attackTrigger.enabled = true;
			if (attacking) {
				Debug.Log ("Attacking = true");
				attackTimer = Time.time + attackCooldown;
				attacked = true;
			}
		}
		if(Time.time > attackTimer + attackCooldown && attacked == true) {
			Debug.Log ("Attacking = false");
			attacking = false;
			attackTrigger.enabled = false;
			attacked = false;
		}


	}

	/*void meleeAttack(){
		if (Time.time > nextAttak) {
			//only repeat attack after attackInterval
			nextAttak = Time.time + attackInterval;
			
			var colls : Collider[] = Physics.OverlapSphere(transform.ImagePosition, range);
			
			for (bool hit : Collider in colls){
				if(hit && hit.tag == "Enemy"){
					//if the object is an enemy check the actual distance to the melee center
					float dist = Vector2.Distance(hit.tansform.position - tansform.position);
					if(dist <= range){
						//if inside the range -- apply damage to the hit object
						hit.SendMessage("ApplyDamage", meleeDamage);
					}
				}
			}
		}
		
	}
	
	void ApplyDamage(float damage){
		if (health > 0){ // if enemy still alive (don't kick a dead dog!)
			health -= damage; // apply the damage...
			// <- enemy can emit some sound here with audio.Play();
			if (health <= 0){ // if health has gone...
				// enemy dead: destroy it, explode it etc.
			}
		}*/


}
