using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public TMP_Text starsAcquiredText;
    public GameObject[] itemPrefabs;

    private void OnEnable()
    {
        int starsSum = 0; 
        foreach (int stars in PlayerData.starNumberPerLevel)
            starsSum += stars;
        starsAcquiredText.text = "Stars: " + starsSum.ToString();

        int index = 0;
        foreach (GameObject itemPrefab in itemPrefabs)
        {
            Tail item = itemPrefab.GetComponent<Tail>();
            switch (item.unlockType)
            {
                case UnlockType.stars:
                    if (item.starsToUnlock <= starsSum && !(item.unlocked))
                    {
                        item.unlocked = true;
                        PlayerData.unlockedTails.Add(index);
                    }
                    break;
                
            }
            index++;
        }
    }
 
}
