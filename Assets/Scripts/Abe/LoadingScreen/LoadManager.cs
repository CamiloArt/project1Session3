﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour {
    
    public GameEngine gameEngine;
    public TurnEnd turnEnd;
    public LoadingBar loadingBar; //access LoadingBar in LoadingScreen GameObject
    [Tooltip("LoadingScreen = [0], SelectionMenu = [1], StrategyMap = [2], BattleMap = [3]")]
    public GameObject[] Scenes;

    public bool loadToSelectionMenu = false;
    public bool loadToStrategyMap = false;
    public bool loadToBattleMap = false;

	void Awake() 
    {
		
	}

	void Update() 
    {
        if (loadToSelectionMenu)
        {
            Load_LoadingScreen();
            if (loadingBar.loaded)
            {
                SelectionMenu();
            }
        }
        if (loadToStrategyMap)
        {
            Load_LoadingScreen();
            if (loadingBar.loaded)
            {
                StrategyMap();
            }
        }
        if (loadToBattleMap)
        {
            Load_LoadingScreen();
            if(loadingBar.loaded)
            {
                BattleMap();
            }
        }
	}

    private void Load_LoadingScreen()
    {
        Scenes[0].SetActive(true);
        Scenes[1].SetActive(false);
        Scenes[2].SetActive(false);
    }

    private void SelectionMenu()
    {
        Scenes[0].SetActive(false);
        Scenes[1].SetActive(true);
        Scenes[2].SetActive(false);
        loadToSelectionMenu = false;
        //[3](false);
    }
    public void LoadTo_SelectionMenu()
    {
        loadToSelectionMenu = true;
    }

    private void StrategyMap()
    {
        Scenes[0].SetActive(false);
        Scenes[1].SetActive(false);
        Scenes[2].SetActive(true);
        loadToStrategyMap = false;
        //[3](false);
    }
    public void LoadTo_StrategyMap()
    {
        loadToStrategyMap = true;
    }

    private void BattleMap()
    {
        Scenes[0].SetActive(false);
        Scenes[1].SetActive(false);
        Scenes[2].SetActive(false);
        loadToBattleMap = false;
        //[3](true);
    }
    public void LoadTo_BattleMap()
    {
        loadToBattleMap = true;
    }
}
