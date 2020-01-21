
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static List<int> starNumberPerLevel = new List<int>();
    public static int numberOfLevels;
    public static int currentLevel;
    public static List<Texture2D> levelPrototypes = new List<Texture2D>();
    public static List<int> unlockedShapes = new List<int>();
    public static List<int> unlockedTails = new List<int>();
    public static Mesh currentShape;
    public static Material currentTail;
}
