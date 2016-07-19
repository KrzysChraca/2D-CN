using UnityEngine;
using System.Collections;

public class ObjectScript : MonoBehaviour {

	public int team = 0;
	public float attackDistance = 2;

	public float myRadius = 0.5f;
	public float attackTimer = 0;
	public float charAttackCD = 1.5f;

	[HideInInspector]
	public Quaternion rotationCalc;
	[HideInInspector]
	public NavMeshAgent navAgentRef;

	public int level = 1;
	public int maxLeve = 100;

	public float experience = 0;


	//Base Value,Stat Value,Growth per Level, every x level.
	[Header ("Object Stats")]
	public ObjectStat statPower;
	public ObjectStat statMagic;
	public ObjectStat statSpeed;
	public ObjectStat statKarma;

	//Base Values (derived from stats), Max Value, Value, mod value
	[Header("Object Variables")]
	public ObjectVariable health;
	public ObjectVariable energy;
	public ObjectVariable energyRegen;
	public ObjectVariable attack;
	public ObjectVariable attackBonus;
	public ObjectVariable attackSpeed;
	public ObjectVariable moveSpeed;
	public ObjectVariable dashRate;

	//ActorFlag is a custom class, see definition at bottom
	[Header("Object Flags")]
	public ObjectFlag flagInvincibility; //Cannot be hurt
	public ObjectFlag flagStunned; //Stunned, cannot take action
	public ObjectFlag flagPhantom; //Dodging flag, makes everything miss (basically invincibility)
	public ObjectFlag flagConfused; //All Actors are considered enemies
	public ObjectFlag flagMindControlled;//Enemies are allies and allies are enemies

	/*protected HeroController heroController;
	protected EnemyController enemyController;*/
	
	//Add list for collectables (keys, skill points, ....)
		//list for skill tree/upgrades
	[HideInInspector]	
	public float currentHP;
	public float changeSpeed;


	public float dealDamage(ObjectScript other)
	{
		float temp = attack.value;
		temp = temp + attackBonus.value;

		return temp;
	}

	public void heal(float healAmount)
	{
		health.value = health.value + healAmount;
		if (health.value > health.maxValue)
			health.value = health.maxValue;
	}

	public void receiveDamage(float damage)
	{
		if (!flagInvincibility.value) {
			if (Random.Range (0f, 1f) < dashRate.value || flagPhantom.value) {
				//missed
				damage = 0;
			} else {
				health.value = health.value - damage;
			}
		} else {
			//invicible
			damage = 0;
		}
	}

	public void levelUp()
	{
		level++;
		health.value = health.maxValue;
	}





	//Use this for initialization
	void Start () {
		if (tag == "hero")
			team = 0;
		else if (tag == "enemy")
			team = 1;

		//heroController = GameObject.FindGameObjectWithTag("HeroController").GetComponent<HeroController>();
		//enemyController = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();


		currentHP = health.value;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


//----------------------------------------------------------------------------------------------------------------------



	[System.Serializable]
	public class ObjectStat
	{
		public float baseValue = 0;
		public float actualValue = 0;
		public float growthPerLevel = 0;
		public float updateEveryXLevel = 0;
		
		public ObjectStat(float basevalue, float actualvalue, float growthperlevel, float updateeveryxlevel)
		{
			baseValue = basevalue;
			actualValue = actualvalue;
			growthPerLevel = growthperlevel;
			updateEveryXLevel = updateeveryxlevel;
		}
	}
	
	[System.Serializable]
	public class ObjectFlag
	{
		public bool value = false;
		public bool canBeChanged = true;
		
		public ObjectFlag(bool val, bool canbechanged)
		{
			value = val;
			canBeChanged = canbechanged;
		}
		
		public void setFlagValue(bool val)
		{
			if(canBeChanged)
			{ 
				value = val; 
			}
		}
	}
	[System.Serializable]
	public class ObjectVariable
	{
		public float baseValue = 0;
		public float maxValue = 0;
		public float value = 0;
		public float modValue = 0;
		
		public ObjectVariable(float baseVal, float maxVal, float val)
		{
			baseValue = baseVal;
			maxValue = maxVal;
			value = val;
		}
	}

}
