using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour {

	public float dmg = 10;

	/*void OnTriggerEnter2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag ("Enemy")) {
			col.SendMessageUpwards("Damage", dmg);
		}
	}*/
}
