﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour {
	//if selected = true, enable selection for weapon and consumables
	//--PUBLIC VARIABLES--//
	public SelectVehicle selectVehicle;
	public ApplyValues applyValues;

	[Tooltip("Minigun:[0], RocketLauncher:[1], Flamethrower[2]")]
	public GameObject[] muscleCar_Weapons;
	[Tooltip("Minigun:[0], RocketLauncher:[1], Flamethrower[2]")]
	public GameObject[] buggy_Weapons;
	[Tooltip("Minigun:[0], RocketLauncher:[1], Flamethrower[2]")]
	public GameObject[] monsterTruck_Weapons;
    [Tooltip("Minigun:[0], RocketLauncher:[1], Flamethrower[2]")]
    public bool[] currentValueW;

    public GameObject currentSelectedWeapon;

    public Text dpsWText;

    public float dps;

	//Minigun
	public void Option0()
	{
		if (applyValues.muscleCar_LookAt)
		{
            currentValueW[0] = true;
            currentValueW[1] = false;
            currentValueW[2] = false;

            currentSelectedWeapon = muscleCar_Weapons[0];

			muscleCar_Weapons[0].SetActive(true);
			muscleCar_Weapons[1].SetActive(false);
            muscleCar_Weapons[2].SetActive(false);

            dps = 0.2f;
            dpsWText.text = "Mingun Damage: " + dps.ToString();
		}
		if (applyValues.buggy_LookAt)
		{
            currentValueW[0] = true;
            currentValueW[1] = false;
            currentValueW[2] = false;

            currentSelectedWeapon = buggy_Weapons[0];

			buggy_Weapons[0].SetActive(true);
			buggy_Weapons[1].SetActive(false);
			buggy_Weapons[2].SetActive(false);

            dps = 0.2f;
            dpsWText.text = "Minigun Damage: " + dps.ToString();
		}
		if (applyValues.monsterTruck_LookAt)
		{
            currentValueW[0] = true;
            currentValueW[1] = false;
            currentValueW[2] = false;

            currentSelectedWeapon = monsterTruck_Weapons[0];

			monsterTruck_Weapons[0].SetActive(true);
			monsterTruck_Weapons[1].SetActive(false);
			monsterTruck_Weapons[2].SetActive(false);

            dps = 0.2f;
            dpsWText.text = "Minigun Damage: " + dps.ToString();
	    }
	}
	//Rocket Launcher
	public void Option1()
	{
		if (applyValues.muscleCar_LookAt)
		{
            currentValueW[0] = false;
            currentValueW[1] = true;
            currentValueW[2] = false;

            currentSelectedWeapon = muscleCar_Weapons[1];

			muscleCar_Weapons[0].SetActive(false);
			muscleCar_Weapons[1].SetActive(true);
			muscleCar_Weapons[2].SetActive(false);

            dps = 0.5f;
            dpsWText.text = "Rocket Launcher Damage: " + dps.ToString();
		}
		if (applyValues.buggy_LookAt)
		{
            currentValueW[0] = false;
            currentValueW[1] = true;
            currentValueW[2] = false;

            currentSelectedWeapon = buggy_Weapons[1];

			buggy_Weapons[0].SetActive(false);
			buggy_Weapons[1].SetActive(true);
			buggy_Weapons[2].SetActive(false);

            dps = 0.5f;
            dpsWText.text = "Rocket Launcher Damage: " + dps.ToString();
		}
		if (applyValues.monsterTruck_LookAt)
		{
            currentValueW[0] = false;
            currentValueW[1] = true;
            currentValueW[2] = false;

            currentSelectedWeapon = monsterTruck_Weapons[1];

			monsterTruck_Weapons[0].SetActive(false);
			monsterTruck_Weapons[1].SetActive(true);
			monsterTruck_Weapons[2].SetActive(false);

            dps = 0.5f;
            dpsWText.text = "Rocket Launcher Damage: " + dps.ToString();
		}
	}
	//Flamethrower
	public void Option2()
	{
		if (applyValues.muscleCar_LookAt)
		{
            currentValueW[0] = false;
            currentValueW[1] = false;
            currentValueW[2] = true;

            currentSelectedWeapon = muscleCar_Weapons[2];

			muscleCar_Weapons[0].SetActive(false);
			muscleCar_Weapons[1].SetActive(false);
			muscleCar_Weapons[2].SetActive(true);

            dps = 0.7f;
            dpsWText.text = "Flamethrower Damage: " + dps.ToString();
		}
		if (applyValues.buggy_LookAt)
		{
            currentValueW[0] = false;
            currentValueW[1] = false;
            currentValueW[2] = true;

            currentSelectedWeapon = buggy_Weapons[2];

			buggy_Weapons[0].SetActive(false);
			buggy_Weapons[1].SetActive(false);
			buggy_Weapons[2].SetActive(true);

            dps = 0.7f;
            dpsWText.text = "Flamethrower Damage: " + dps.ToString();
		}
		if (applyValues.monsterTruck_LookAt)
		{
            currentValueW[0] = false;
            currentValueW[1] = false;
            currentValueW[2] = true;

            currentSelectedWeapon = monsterTruck_Weapons[2];

			monsterTruck_Weapons[0].SetActive(false);
			monsterTruck_Weapons[1].SetActive(false);
			monsterTruck_Weapons[2].SetActive(true);

            dps = 0.7f;
            dpsWText.text = "Flamethrower Damage: " + dps.ToString();
		}
	}

    public void ResetWeaponSelection()
    {
        muscleCar_Weapons[0].SetActive(false);
        muscleCar_Weapons[1].SetActive(false);
        muscleCar_Weapons[2].SetActive(false);

        buggy_Weapons[0].SetActive(false);
        buggy_Weapons[1].SetActive(false);
        buggy_Weapons[2].SetActive(false);

        monsterTruck_Weapons[0].SetActive(false);
        monsterTruck_Weapons[1].SetActive(false);
        monsterTruck_Weapons[2].SetActive(false);
    }
}
