using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
public class loadScript : MonoBehaviour
{
    void Awake()
    {
        SaveData data = SaveGame.Load<SaveData>("data", new SaveData());
        PlayerData.starNumberPerLevel = data.starNumberPerLevel;
        PlayerData.unlockedShapes = data.unlockedShapes;
        PlayerData.unlockedTails = data.unlockedTails;
    }
    
}
