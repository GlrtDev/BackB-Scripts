using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    private Texture2D map;

	public ColorToPrefab[] colorMappings;

	// Use this for initialization
	void Start () {
       // GetMap(PlayerData.currentLevel); //Uncomment on Relese
		GenerateLevel();
	}

    void GetMap(int currentMap)
    {
        map = PlayerData.levelPrototypes[currentMap-1];
    }

	void GenerateLevel ()
	{
		for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
				GenerateTile(x, y);
			}
		}
	}

	void GenerateTile (int x, int y)
	{
		Color pixelColor = map.GetPixel(x, y);

		if (pixelColor.a == 0)
		{
			// The pixel is transparrent. Let's ignore it!
			return;
		}

		foreach (ColorToPrefab colorMapping in colorMappings)
		{
			if (colorMapping.color.Equals(pixelColor))
			{
				Vector3 position = new Vector3(x, y, 100);
				Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
			}
		}
	}
	
}
