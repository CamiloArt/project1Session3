﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Player myPlayer;
	private GameEngine gameEngine;
	private PlayerInputs myInput;
	public ParticleSystem myDust; 

	public CharacterController playerCc;
	public Vector3 mapDirection;
	public float magnitude;
	float terrainminDistance;
	public bool pressingTime;

	//combat variables
	public float gravity;
	public float turningSpeed;

	public Vector3 direction;
	public Vector3 lastDirection;

	public Vector3 tdirection;
	public bool pressing;
	public bool shooting;
	public Vector3 shootingVector;

	public float turboTime;
	private bool usedTurbo;
	private float maxSpeed;
	private Quaternion ankle;
	public bool switchWeapon;

	private bool consBool;
	private bool consBool2;

	// Use this for initialization

	void Awake () {
		gameEngine = GameObject.FindGameObjectWithTag ("GameEngine").GetComponent<GameEngine> ();
		playerCc = gameObject.GetComponent<CharacterController> ();
		myPlayer = gameObject.GetComponent<Player> ();
		myInput = gameObject.GetComponent<PlayerInputs> ();
		terrainminDistance = 3f;
		turboTime = 3f;
		usedTurbo = false;
		switchWeapon = false;
		consBool = false;
		myDust.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		shooting = false;
		if (gameEngine.gameState == "strategyMap" && gameEngine.startTurn) {
			if (gameEngine.currentPlayer.playerTurn == myPlayer.playerTurn) {
				pressingTime = false;
				myPlayer = gameEngine.currentPlayer;
				playerCc = myPlayer.gameObject.GetComponent<CharacterController> ();
				MovePlayerMainMap ();
			}
		}
		else if(gameEngine.gameState == "battlemap" && gameEngine.inBattle && myPlayer.inBattle){
			SetDirection ();
			getShootingAxis ();
			getSwitchWeapon ();
		}
		else{
			myDust.Stop ();
		}
	}

	void MovePlayerMainMap(){
		this.mapDirection = Vector3.zero;
		if (Mathf.Abs (Input.GetAxis (myInput.hAxisName)) > 0.2 || Mathf.Abs (Input.GetAxis (myInput.vAxisName)) > 0.2) {
			mapDirection += new Vector3 (Input.GetAxis (myInput.hAxisName), 0f, Input.GetAxis (myInput.vAxisName));
			ApplyMovement ();
			myDust.Play ();
		} else {
			myDust.Stop ();
		}
		if (Input.GetButton (myInput.Abutton)) {
			pressingTime = true;
		}
	}

	void ApplyMovement(){
		float x = Input.GetAxis (myInput.hAxisName);
		float y = Input.GetAxis (myInput.vAxisName);
		float angle = Mathf.Atan2 (x, y) * Mathf.Rad2Deg;
		myPlayer.gameObject.transform.rotation = Quaternion.Euler(0, angle, 0);
		magnitude =  Vector3.Magnitude (mapDirection);
		CheckTerrains ();
		playerCc.Move (mapDirection * (myPlayer.playerUnit.speed.mapSpeed - myPlayer.playerUnit.fuel.terrainValue) * Time.deltaTime);
		myPlayer.playerUnit.fuel.currentFuel -= (myPlayer.playerUnit.fuel.fuelConsumption) * Time.deltaTime * magnitude;
	}
	void SetDirection(){
		direction = Vector3.zero;
		pressing = false;
		if (Mathf.Abs (Input.GetAxis (myInput.hAxisName)) > 0.2 || Mathf.Abs (Input.GetAxis (myInput.vAxisName)) > 0.2) {
			pressing = true;
			myDust.Play ();
		} else {
			myDust.Stop ();
		}
		if (Input.GetAxisRaw (myInput.trigger) > 0.2 && turboTime > 0) {
			if (!usedTurbo) {
				myPlayer.playerUnit.speed.currentSpeed = myPlayer.playerUnit.speed.turboSpeed;
				usedTurbo = true;
				maxSpeed = myPlayer.playerUnit.speed.turboSpeed;
			}
		}
		if(usedTurbo) {
			if (maxSpeed > myPlayer.playerUnit.speed.maxSpeed) {
				maxSpeed -= myPlayer.playerUnit.speed.speedDampener*4;
			}
				turboTime -= Time.deltaTime;
			if (turboTime < 0) {
				turboTime = 3f;
				usedTurbo = false;
			}
		}
		else {
			if (myPlayer.playerUnit.inCaltrops) {
				maxSpeed = myPlayer.playerUnit.speed.maxSpeed - myPlayer.playerUnit.speed.caltropsReducer;
			} else {
				maxSpeed = myPlayer.playerUnit.speed.maxSpeed;
			}
		}
		if (pressing) {
			myPlayer.playerUnit.speed.currentSpeed += myPlayer.playerUnit.speed.speedMultiplier;
		} else {
			myPlayer.playerUnit.speed.currentSpeed -= myPlayer.playerUnit.speed.speedDampener;
		}
		tdirection = tdirection.normalized;
		direction = (transform.rotation * tdirection) * myPlayer.playerUnit.speed.currentSpeed * Time.deltaTime;
		if (playerCc.isGrounded == true) {
			//This determined the direction at which you turn based on the left joystick of an Xbox controller
			Vector3 NextDir = new Vector3 (Input.GetAxisRaw (myInput.hAxisName.ToString ()), 0, Input.GetAxisRaw (myInput.vAxisName.ToString ()));
			//if you are moving, your next direction set by the joystick angle you are pressing
			if (NextDir != Vector3.zero) {
				ankle = Quaternion.LookRotation (NextDir);
			}
		} else {
			direction.y -= gravity * Time.deltaTime;
			tdirection = Vector3.forward;
		}
		float step = myPlayer.playerUnit.speed.currentSpeed * Time.deltaTime * turningSpeed;
		gameObject.transform.rotation = Quaternion.RotateTowards (transform.rotation, ankle, step);
		myPlayer.playerUnit.speed.currentSpeed = Mathf.Clamp (myPlayer.playerUnit.speed.currentSpeed, myPlayer.playerUnit.speed.minSpeed, maxSpeed);
		playerCc.Move (direction);
	}
	void getShootingAxis(){
		if (Mathf.Abs (Input.GetAxis (myInput.hAxisName2)) > 0.2 || Mathf.Abs (Input.GetAxis (myInput.vAxisName2)) > 0.2) {
			shooting = true;
			shootingVector = new Vector3 (Input.GetAxisRaw (myInput.hAxisName2), 0, Input.GetAxisRaw (myInput.vAxisName2));
		}
	}
	void getSwitchWeapon(){
		if (Input.GetButtonDown (myInput.RBbutton)) {
			if (switchWeapon) {
				myPlayer.playerUnit.weapon2.SetActive (false);
				myPlayer.playerUnit.weapon1.SetActive (true);
				switchWeapon = false;
			} else {
				myPlayer.playerUnit.weapon1.SetActive (false);
				myPlayer.playerUnit.weapon2.SetActive (true);
				switchWeapon = true;
			}
		}
		if (Mathf.Abs (Input.GetAxisRaw (myInput.trigger2)) > 0.2 && !consBool) {
			myPlayer.playerUnit.UseConsumable (direction);
			consBool = true;
		}
		if (Mathf.Abs (Input.GetAxisRaw (myInput.trigger2)) < 0.2 && consBool)
		{
			consBool = false;
		}
	}
	void CheckTerrains(){
		GameObject[] terrains = GameObject.FindGameObjectsWithTag ("Terrain");
		float distance;
		for (int i = 0; i < terrains.Length; i++) {
			distance = Vector3.Distance (gameObject.transform.position, terrains [i].gameObject.transform.position);
			if (distance < terrainminDistance) {
				myPlayer.playerUnit.fuel.terrainValue = terrains [i].gameObject.GetComponent<mapTerrains> ().terrainValue;
			}
		}
	}
}
