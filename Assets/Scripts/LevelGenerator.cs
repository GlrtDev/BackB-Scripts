using System;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    private Texture2D map;
    public bool testGo;
    public ObjectPooler Pool;
    public ColorToPrefabNumber[] colorMappings;
    public GameObject[] exits;
    public IngameUI ingameUI;
    public static int ballsToUse;
    // Use this for initialization

       
    void Start () {
        if(!testGo)
        GetMap(PlayerData.currentLevel); //Uncomment on Relese
        PlayerBehavoir.SpawnCount = 0;
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
        GameLogicInit();

        GameObject playerSpawn = GameObject.FindGameObjectWithTag("Player"); //  Initial Update 
        playerSpawn.GetComponent<PlayerBehavoir>().gameUI.UpdateText(); //       On gui
    }

    private void GameLogicInit()
    {
        exits = GameObject.FindGameObjectsWithTag("Exit");
        ballsToUse = 0; 
        foreach (GameObject exit in exits)
        {
            Exit exit_ = exit.GetComponent<Exit>();
            ballsToUse += exit_.Capacity();
            exit_.onColl.AddListener(CollisionHandle);
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
                GO.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GO.SetActive(true);
                iTween.ScaleFrom(GO, iTween.Hash(
                "scale", Vector3.zero,
                "time", 1.5f));
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
            //Debug.Log(numberOfFullExits);
        }
    }

    public void RestartLevel()
    {
        GenerateLevel();
    }

    public int BallsToUse()
    {
        return ballsToUse;
    }
}
