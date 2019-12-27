using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<int> starNumberPerLevel;

    public SaveData()
    {
        starNumberPerLevel = new List<int>();
        starNumberPerLevel.Add(0); // 1 for test
    }

    public SaveData(List<int> stars)
    {
        starNumberPerLevel = stars;
    }
}
