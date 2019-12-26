using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //public List<Level> levels;
    public List<Texture2D> levelPrototypes = new List<Texture2D>();
    public void Awake()
    {
        SaveData data = SaveGame.Load<SaveData>("data", new SaveData());
        PlayerData.starNumberPerLevel = data.starNumberPerLevel;
        
        PlayerData.levelPrototypes = LoadLevelsPrototypes();
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

    public List<Texture2D> LoadLevelsPrototypes() //TODO
    {
        int i = 1;
        while (Resources.Load<Texture2D>("Levels/" + i) != null)
        {
            levelPrototypes.Add(Resources.Load<Texture2D>("Levels/" + i));
            ++i;
        }
        while(levelPrototypes.Count >= PlayerData.starNumberPerLevel.Count)
        {
            PlayerData.starNumberPerLevel.Add(PlayerData.starNumberPerLevel.Count + 1, 0);
        }

        foreach (Object o in levelPrototypes)
        {
            //Debug.Log(o);
        }
        return levelPrototypes;
    }

}
