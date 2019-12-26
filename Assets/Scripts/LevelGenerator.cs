using UnityEngine;
using TMPro;
public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    private Texture2D map;
    public bool testGo;
    public ObjectPooler Pool;
    public ColorToPrefabNumber[] colorMappings;
    public GameObject[] exits;
    public IngameUI ingameUI;
    public static int ballsToUse;
    public TMP_Text floorNumber;
    public GameObject floor;
    private Vector3 oneUnitVector = new Vector3(0.5f,0.5f,0.5f);
    public Camera mainCamera;
    public int movesFor3Stars;
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

    void CleanPreviousLevel()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            iTween.ScaleTo(transform.GetChild(i).gameObject, iTween.Hash(
                "scale", Vector3.zero,
                "easetype", iTween.EaseType.easeInOutQuint,
                "time", 0.5f));

        iTween.ScaleTo(floorNumber.gameObject, iTween.Hash(
                "scale", Vector3.zero,
                "oncomplete", "RotateFloor",
                "oncompletetarget", this.gameObject,
                "easetype", iTween.EaseType.easeInOutQuint,
                "time", 1.0f));
    }

	void GenerateLevel ()
	{
        floorNumber.text = PlayerData.currentLevel.ToString();
        iTween.ScaleTo(floorNumber.gameObject, iTween.Hash(
                "scale", Vector3.one,
                "easetype", iTween.EaseType.easeInOutQuint,
                "time", 1.0f));

        for (int i = 0; i < gameObject.transform.childCount; i++) // delete previous
            transform.GetChild(i).gameObject.SetActive(false);

        for (int x = 0; x < map.width; x++)
		{
			for (int y = 1; y < map.height; y++)
			{
				GenerateTile(x, y);
			}
		}

        //Count the pixels on the bottom, there for game logic
        movesFor3Stars = 0;
        for (int x = 0, y = 0; x < map.width; x++)
        {
            
                CheckForPixel(x,y);
            
        }
        Debug.Log(movesFor3Stars);
        GameLogicInit();

        GameObject playerSpawn = GameObject.FindGameObjectWithTag("Player"); //  Initial Update 
        playerSpawn.GetComponent<PlayerBehavoir>().gameUI.UpdateText(); //       On gui
    }

    private void CheckForPixel(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            return;
        }
        else if (pixelColor.a < 1f)
        {
            movesFor3Stars += 1;
            return;
        }
        else if (pixelColor.a == 1)
        {
            movesFor3Stars += 2;
            return;
        }
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
                GameObject GO = Pool.GetPooledObject(colorMapping.orderInObjectPool);
                GO.transform.localPosition = position;
                GO.transform.rotation = Quaternion.identity;
                GO.transform.localScale = Vector3.zero;
                GO.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GO.SetActive(true);
                iTween.ScaleTo(GO, iTween.Hash(
                "scale", oneUnitVector,
                "time", 0.5f,
                "easeType", iTween.EaseType.easeInOutQuint));
            }
        }
	}

    public void NextMap()
    {
        int currentMap = ++PlayerData.currentLevel;
        GetMap(currentMap);
        CleanPreviousLevel();
        
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
        iTween.PunchPosition(mainCamera.gameObject, 10 * Vector3.one, 0.5f);
        GenerateLevel();
    }

    public int BallsToUse()
    {
        return ballsToUse;
    }

    public void RotateFloor()
    {
        iTween.RotateFrom(floor.gameObject, iTween.Hash(
            "rotation", new Vector3(180, 0, 0),
            "oncomplete", "GenerateLevel",
            "oncompletetarget", this.gameObject,
            "easetype", iTween.EaseType.easeOutCubic,
            "time", 2.0f));
    }
}
