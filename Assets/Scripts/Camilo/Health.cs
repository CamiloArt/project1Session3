﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float maxHp;
	public float currentHp;
	public float initialHp;        
	public bool isDead;
	public Armor myArmor;
	// Use this for initialization
	void Start () {
		currentHp = initialHp;
		myArmor = gameObject.GetComponent<Armor> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHp <= 0 && !isDead)
		{
			Death ();
		}
	}
	void Death ()
	{
		isDead = true;
	}
	public void ReceiveDamage(float damageTaken){
		currentHp -= (damageTaken -(damageTaken* (myArmor.currentArmor/10)));
	}
}
