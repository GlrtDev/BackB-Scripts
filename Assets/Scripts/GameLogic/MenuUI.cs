using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class MenuUI : MonoBehaviour
{
    public GameObject levelSelector;
    public GameObject Menu;
    public void GoToLevelSelection()
    {
        Menu.SetActive(false);
        levelSelector.SetActive(true);
    }

    public void NewGame()
    {
        SaveData data = new SaveData();
        SaveGame.Save<SaveData>("data", data);
        PlayerData.starNumberPerLevel = data.starNumberPerLevel;
    }
}
