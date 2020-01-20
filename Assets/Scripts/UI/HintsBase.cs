using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelToHintWidgetPrefab
{
    public int level;
    public GameObject prefab;
}

public class HintsBase : MonoBehaviour
{
    public LevelToHintWidgetPrefab[] levelToHintWidgetPrefabs;

    public void ShowHint()
    {
        Debug.Log("Next Level");
        foreach(LevelToHintWidgetPrefab occurrence in levelToHintWidgetPrefabs)
            if(occurrence.level == PlayerData.currentLevel)
                Instantiate(occurrence.prefab, transform);
    }
}
