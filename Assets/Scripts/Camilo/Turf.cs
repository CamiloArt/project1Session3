﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turf : MonoBehaviour {

	public float minDistance;
	public GameEngine gameEngine;
	public GameObject flag;
	private Renderer flagRender;
	private bool blueLeader;
	private int blueGuardsCounter;
	private bool redLeader;
	private int redGuardsCounter;
	public int turnsCounter;
	[Header("Negative for Red, Positive for blue")]
	public int teamCounter;
	public enum OwnerNames
	{
		Neutral,
		RedTeam,
		BlueTeam
	}
	public OwnerNames owner;
	// Use this for initialization
	void Awake () {
		gameEngine = GameObject.FindGameObjectWithTag ("GameEngine").GetComponent<GameEngine> ();
		flagRender = flag.GetComponent<Renderer> ();
	}
	void Start(){
		owner = OwnerNames.Neutral;
		turnsCounter = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (turnsCounter != gameEngine.turnCounter) {
			CheckPlayers ();
			turnsCounter = gameEngine.turnCounter;
		}
	}
	void CheckPlayers(){
		float distance;
		blueLeader = false;
		redLeader = false;
		blueGuardsCounter = 0;
		redGuardsCounter = 0;
		for (int i = 0; i < gameEngine.players.Length; i++) {
			distance = Vector3.Distance (gameEngine.players[i].gameObject.transform.position, gameObject.transform.position);
			if (distance < minDistance) {
				if (gameEngine.players [i].typeOfPlayer.ToString () == "Leader") {
					if (gameEngine.players [i].playerTeam.teamColor.ToString() == "Blue") {
						blueLeader = true;
						Debug.Log ("blueleader on turf");
					}
					if (gameEngine.players [i].playerTeam.teamColor.ToString() == "Red") {
						redLeader = true;
						Debug.Log ("Redleader on turf");
					}
				}
				if (gameEngine.players [i].typeOfPlayer.ToString () == "Guard") {
					if (gameEngine.players [i].playerTeam.teamColor.ToString() == "Blue") {
						blueGuardsCounter++;
						Debug.Log ("blueguard on turf");
					}
					if (gameEngine.players [i].playerTeam.teamColor.ToString() == "Red") {
						redGuardsCounter++;
						Debug.Log ("blueguard on turf");
					}
				}
			}
		}
		SetTurf();
	}
	void SetTurf(){
		switch (owner) {
		case OwnerNames.BlueTeam:
			if(redLeader && !blueLeader){
				teamCounter--;				
			}else if(redGuardsCounter > 0 && blueGuardsCounter == 0){
				teamCounter--;
			}
			break;
		case OwnerNames.Neutral:
			if(redLeader && !blueLeader){
				teamCounter--;				
			}else if(!redLeader && blueLeader){
				teamCounter++;
			}
			break;
		case OwnerNames.RedTeam:
			if(!redLeader && blueLeader){
				teamCounter++;				
			}else if(redGuardsCounter == 0 && blueGuardsCounter > 0){
				teamCounter++;
			}
			break;
		
		}
		if (teamCounter == gameEngine.turnsToCapture) {
			owner = OwnerNames.BlueTeam;
		}else if(teamCounter == 0){
			owner = OwnerNames.Neutral;
			Vector3 newpos = new Vector3 (1, -100, 0);
			flag.transform.localPosition = newpos;
		}else if(teamCounter == -gameEngine.turnsToCapture){
			owner = OwnerNames.RedTeam;
		}

		if(teamCounter > 0 || teamCounter < 0 ){
			Vector3 newpos = new Vector3 (1, Mathf.Abs(teamCounter) * 2, 0);
			flag.transform.localPosition = newpos;
			if (teamCounter > 0) {
				flagRender.material.SetColor ("_Color", Color.blue);				
			} else {
				flagRender.material.SetColor ("_Color", Color.red);
			}
		}

		SetColor ();
	}
	void SetColor(){
		switch (owner) {
		case OwnerNames.BlueTeam:
			flagRender.material.shader = Shader.Find("Specular");
			//set Blue color;
			break;
		case OwnerNames.Neutral:
			flagRender.material.shader = Shader.Find("Specular");
			//set Blue color;
			break;
		case OwnerNames.RedTeam:
			flagRender.material.shader = Shader.Find("Specular");
			//set Blue color;
			break;

		}
	}
}