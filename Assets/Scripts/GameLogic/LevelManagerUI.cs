﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManagerUI : MonoBehaviour
{
    public LevelManager levelManager;
    public Transform content;
    public LevelUI LevelUIPrefab;
    public bool UnlockAllLevels;
    // Start is called before the first frame update
    void Start()
    {
        if (levelManager)
            Display(levelManager);
    }
    public virtual void Display(LevelManager lm)
    {
        this.levelManager = lm;
        Refresh();
    }
    public virtual void Refresh()
    {
        foreach (Transform t in content)
        {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < PlayerData.starNumberPerLevel.Count; i++)
        {
            int playerLevelData = PlayerData.starNumberPerLevel[i];

                LevelUI ui = LevelUI.Instantiate(LevelUIPrefab, content);
                ui.onClicked.AddListener(UIClicked);
                //Debug.Log('v');
                Level level = ui.gameObject.GetComponent<Level>();
                level.index = i;
                level.stars = playerLevelData;
                ui.Display(level);
            if(!UnlockAllLevels)
                if (level.stars == 0)
                    break;
            
        }
    }

    public virtual void UIClicked(LevelUI lvlUI)
    { 
        levelManager.EnterTheLevel(lvlUI.level);
    }
}
