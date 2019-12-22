using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //public List<Level> levels;

    public void Awake()
    {
        bool newGame = true;
        //SaveData data = new SaveData();
        if (SaveGame.Exists("data"))
            newGame = !newGame;
        SaveData data = SaveGame.Load<SaveData>("data", new SaveData());
        PlayerData.starNumberPerLevel = data.starNumberPerLevel;
        PlayerData.numberOfLevels = data.starNumberPerLevel.Count;
        if (!newGame)
            PlayerData.levelPrototypes = data.LoadLevelsPrototypes(false);
        else
            PlayerData.levelPrototypes = data.levelPrototypes;
        //LoadLevelsFromPlayerData();
    }

    public void EnterTheLevel(Level level)
    {
        PlayerData.currentLevel = level.index;
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game Scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    //private void LoadLevelsFromPlayerData()
    //{
    //    int levelIndex = 1;
    //    do
    //    {
    //        //levels.Add(LevelPrefab);
    //        //levels[levelIndex - 1].stars = PlayerData.starNumberPerLevel[levelIndex];
    //        //levels[levelIndex - 1].index = levelIndex;
    //         ++levelIndex;
    //        Debug.Log("cos");
    //    } while (levelIndex != PlayerData.numberOfLevels+1);
    //}
}
