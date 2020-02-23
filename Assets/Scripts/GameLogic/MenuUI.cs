using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class MenuUI : MonoBehaviour
{
    public GameObject levelSelector;
    public GameObject menu;
    public GameObject playerSettings;
    public GameObject settings;
    public GameObject credits;
    public GameObject menuButton;
    public GameObject menuButtonBG;

    public void GoToLevelSelection()
    {
        DisableAll();
        levelSelector.SetActive(true); MenuButtonSetActive(true);
    }

    public void GoToPlayerSettings()
    {
        DisableAll();
        playerSettings.SetActive(true); MenuButtonSetActive(true);
    }

    public void GoToSettings()
    {
        DisableAll();
        settings.SetActive(true); MenuButtonSetActive(true);
    }

    public void GoToCredits()
    {
        DisableAll();
        credits.SetActive(true); MenuButtonSetActive(true);
    }

    public void GoToMenu()
    {
        // make it another script and async function
        SaveData menuAutoSave = SaveGame.Load<SaveData>("data");
        menuAutoSave.unlockedShapes = PlayerData.unlockedShapes;
        menuAutoSave.unlockedTails = PlayerData.unlockedTails;
        SaveGame.Save<SaveData>("data", menuAutoSave);

        DisableAll();
        menu.SetActive(true); MenuButtonSetActive(false);
    }

    public void DisableAll()
    {
        settings.SetActive(false);
        menu.SetActive(false);
        credits.SetActive(false);
        levelSelector.SetActive(false);
        playerSettings.SetActive(false);
    }

    private void MenuButtonSetActive(bool action)
    {
        menuButton.SetActive(action);
        menuButtonBG.SetActive(action);
    }
    public void NewGame()
    {
        SaveData data = new SaveData();
        SaveGame.Save<SaveData>("data", data);
        PlayerData.starNumberPerLevel = data.starNumberPerLevel;
        PlayerData.unlockedShapes = data.unlockedShapes;
        PlayerData.unlockedTails = data.unlockedTails;
        Debug.Log("newgame");
    }
}
