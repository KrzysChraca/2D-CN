using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	bool isDead;
	bool damaged;

	// Use this for initialization
	void Start () {

		currentHealth = startingHealth;
		healthSlider.value = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage (int amount){

		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;

		if(currentHealth <= 0 && !isDead)
		{
			Death();
		}

	}

	public void Death()
	{
		isDead = true;

		//player can not attack


	}

}
