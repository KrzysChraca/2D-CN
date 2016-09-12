using UnityEngine;
using System.Collections;
using System;

public class Item : MonoBehaviour {
	[SerializeField]
	public string name;


	[SerializeField]
	public enum Type
	{
		key,
		healthPot,
		upgrade
	}


	public Type type;


	/*
		Key = 0,
		HealthPot = 1,
		Upgrade = 2
	*/



	public Item(string xName, Type xType)
	{
		name = xName;
		type = xType;
	}


}
