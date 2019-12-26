using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public IDictionary<int, int> starNumberPerLevel;

    public SaveData()
    {
        starNumberPerLevel.Add(1, 0);
    }
}
