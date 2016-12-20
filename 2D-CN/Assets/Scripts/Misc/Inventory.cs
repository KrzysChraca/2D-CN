using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Inventory : MonoBehaviour 
{


	public List<Item> items; 

	void Start()
	{
		items = new List<Item> ();
		items.Sort ();
		foreach (Item atrib in items) {
			print (atrib.name + " - " + atrib.type);
		}
	}

	public Item AddNew(string Iname, Item.Type x){

		Item newItem = new Item (Iname, x);

		items.Add (newItem);

		return newItem;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("PickUp")) {

			Item temp = other.GetComponent<Item>();

			Item x = new Item(temp.name, temp.type);

			AddNew (x.name, x.type);

			Debug.Log (x.name + " - " + x.type);

			Destroy(other.gameObject);

			items.Sort ();
			foreach (Item atrib in items) {
				print (atrib.name + " - " + atrib.type);
			}

		}


	}
}
