using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<int> starNumberPerLevel;
    public List<int> unlockedShapes;
    public List<int> unlockedTails;
    public SaveData()
    {
        starNumberPerLevel = new List<int>();
        unlockedShapes = new List<int>();
        unlockedTails = new List<int>();
        starNumberPerLevel.Add(0); // 1 for test
        unlockedShapes.Add(0);
        unlockedTails.Add(0);
    }

    public SaveData(List<int> stars, List<int> shapes, List<int> tails)
    {
        starNumberPerLevel = stars;
        unlockedShapes = shapes;
        unlockedTails = tails;
    }
}
