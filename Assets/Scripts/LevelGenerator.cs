using System;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    private Texture2D map;
    public ObjectPooler Pool;
    public ColorToPrefabNumber[] colorMappings;
    public GameObject[] exits;
    public IngameUI ingameUI;
    // Use this for initialization

       
    void Start () {
        GetMap(PlayerData.currentLevel); //Uncomment on Relese
		GenerateLevel();
	}

    void GetMap(int currentLevel)
    {
        map = PlayerData.levelPrototypes[currentLevel-1];
    }

	void GenerateLevel ()
	{
        for (int i = 0; i < gameObject.transform.childCount; i++) // delete previous
            transform.GetChild(i).gameObject.SetActive(false);
        
        for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
				GenerateTile(x, y);
			}
		}

        exits = GameObject.FindGameObjectsWithTag("Exit");
        foreach(GameObject exit in exits)
        {
            Debug.Log(exit);
            exit.GetComponent<Exit>().onColl.AddListener(CollisionHandle);
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

		foreach (ColorToPrefabNumber colorMapping in colorMappings)
		{
			if (colorMapping.color.Equals(pixelColor))
			{
				Vector3 position = new Vector3(x, y, 100);
                //Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                GameObject GO = Pool.GetPooledObject(colorMapping.orderInObjectPool);
                GO.transform.position = position;
                GO.transform.rotation = Quaternion.identity;
                GO.SetActive(true);
            }
		}
	}

    public void NextMap()
    {
        int currentMap = ++PlayerData.currentLevel;
        GetMap(currentMap);
        GenerateLevel();
    }
    private void CollisionHandle(Exit exit) //check if all exits are full
    {
        int numberOfFullExits = 0;
        foreach (GameObject go in exits)
        {
            Exit ex = go.GetComponent<Exit>();
            if (ex.isFull())
                ++numberOfFullExits;
            if (numberOfFullExits == exits.GetLength(0))
                ingameUI.ShowSummaryUI();
            Debug.Log(numberOfFullExits);
        }
    }

}
