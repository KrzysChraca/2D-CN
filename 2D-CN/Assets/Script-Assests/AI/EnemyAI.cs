using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public int health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (health <= 0) {
			Destroy (gameObject);
		}

	}

	public void Damage(int damage){
		health -= damage;
		Debug.Log ("Damage Taken");
		//animate damage is taken
	}

}
