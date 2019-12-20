using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public IDictionary<int, int> starNumberPerLevel;
    List<Texture2D> levelPrototypes = new List<Texture2D>();

    public SaveData()
    {
        LoadLevelsPrototypes(true);
    }

    public List<Texture2D> LoadLevelsPrototypes(bool initialLoad) //TODO
    {
        int i = 1;
        starNumberPerLevel = new Dictionary<int, int>();
        
        while (Resources.Load<Texture2D>("Levels/" + i) != null)
        {
            levelPrototypes.Add( Resources.Load<Texture2D>("Levels/" + i));
            if(initialLoad)
            starNumberPerLevel[i] = 1; //change to 0
            ++i;
        }

        foreach (Object o in levelPrototypes)
        {
            Debug.Log(o);
        }
        return levelPrototypes;
    }
}
