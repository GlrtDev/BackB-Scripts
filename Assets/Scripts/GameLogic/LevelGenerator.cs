using UnityEngine;
using TMPro;
using BayatGames.SaveGameFree;

public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    private Texture2D map;

    public bool testGo;
    public ObjectPooler Pool;
    public ColorToPrefabNumber[] colorMappings;
    public GameObject[] exits;
    public event System.Action NextLevelStarted;
    public HintsBase hints;
    private bool firstRun;

    public IngameUI ingameUI;
    public static int ballsToUse;
    public TMP_Text floorNumber;
    public GameObject floor;

    private Vector3 oneUnitVector = new Vector3(0.5f,0.5f,0.5f);
    public Camera mainCamera;

    public static int movesFor3Stars;
    // Use this for initialization
    public Material backgroundMat;
    public Material cubeMat;
    public Material floorMat;
    private int backgroundCount;
    // public Light subLight;

    void Start () {
        int i = 0;
        while (Resources.Load<Texture2D>("Backgrounds/Background" + i) != null)
            i++;
        backgroundCount = i;

        firstRun = true;
        if (!testGo)
        GetMap(PlayerData.currentLevel); //Uncomment on Relese
        NextLevelStarted += hints.ShowHint;
        PlayerBehavoir.spawnCount = 0;
        GenerateLevel();
    }

    void GetMap(int currentLevel)
    {
        map = PlayerData.levelPrototypes[currentLevel];
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
                "time", 0.3f));
    }
     
	void GenerateLevel ()
	{
        if(firstRun)
        if (NextLevelStarted != null)
            NextLevelStarted();

        floorNumber.text = PlayerData.currentLevel.ToString();
        iTween.ScaleTo(floorNumber.gameObject, iTween.Hash(
                "scale", Vector3.one,
                "easetype", iTween.EaseType.easeInOutQuint,
                "time", 1.0f));

        //floor.gameObject.transform.localScale = new Vector3(11, 16, 1);
        iTween.ScaleTo(floor.gameObject, iTween.Hash(
                "scale", new Vector3(10, 15, 1),
                "easetype", iTween.EaseType.easeInOutQuint,
                "time", 0.3f));

        int nr = PlayerData.currentLevel % backgroundCount;
        Texture2D texNow = Resources.Load<Texture2D>("Backgrounds/Background" + nr);

        backgroundMat.mainTexture = texNow;
        Vector2 randOffset = new Vector2(Random.value, Random.value);
        backgroundMat.SetTextureOffset("_FlowMap", randOffset);
        backgroundMat.SetTextureOffset("_MainTex", randOffset);

        cubeMat.mainTexture = texNow;
        floorMat.SetTexture("_MainTex", floorMat.GetTexture("_SecondaryTex"));
        floorMat.SetFloat("_Blend", 0.0f);
        floorMat.SetTexture("_SecondaryTex", texNow); //new texture
        
        iTween.ValueTo(floor.gameObject, iTween.Hash( //lerpingMat
                "from", 0.0f,
                "to", 1.0f,
                "easetype", iTween.EaseType.linear,
                "time", 0.6f,
                "onupdate" , "LerpBeetwenMat",
                "onupdatetarget", this.gameObject));

        // subLight.color = texNow.GetPixel(3, 3);
        for (int i = 0; i < gameObject.transform.childCount; i++) // delete previous
            transform.GetChild(i).gameObject.SetActive(false);

        for (int x = 0; x < map.width; x++)
		{
			for (int y = 1; y < map.height; y++)
			{
				GenerateTile(x, y);
			}
		} //generete current

        //Count the pixels on the bottom; for game logic
        movesFor3Stars = 0; PlayerBehavoir.numberOfMoves = 0;
        for (int x = 0, y = 0; x < map.width; x++)
        {   
                CheckForPixel(x,y);
        }
        
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
    } // Bottom pixels for game logic

    private void GameLogicInit()
    {
        exits = GameObject.FindGameObjectsWithTag("Exit");
        ballsToUse = 0; 
        foreach (GameObject exit in exits)
        {
            Exit exit_ = exit.GetComponent<Exit>();
            ballsToUse += exit_.Capacity();
            exit_.ballAndExitCollision.AddListener(CollisionHandle);
            Debug.Log("level gen init");
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
                float zOffset = colorMapping.orderInObjectPool / 1000f; // walkaround to clipping textures-models
                Vector3 position = new Vector3(x, y, 100 + zOffset);
                GameObject GO = Pool.GetPooledObject(colorMapping.orderInObjectPool);
                GO.transform.localPosition = position;
                GO.transform.rotation = Quaternion.identity;
                GO.transform.localScale = Vector3.zero;
                GO.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
        firstRun = true;
    }

    private void CollisionHandle() //check if all exits are full
    {
        int numberOfFullExits = 0;
        foreach (GameObject go in exits)
        {
            Exit ex = go.GetComponent<Exit>();
            if (ex.isFull())
                ++numberOfFullExits;
            if (numberOfFullExits == exits.GetLength(0))
            {
                ingameUI.ShowSummaryUI();
                SaveThisGo();
            }
            //Debug.Log(numberOfFullExits);
        }
    }

    private void SaveThisGo()
    {
        if (StarsAcquired() > PlayerData.starNumberPerLevel[PlayerData.currentLevel])
        {
            PlayerData.starNumberPerLevel[PlayerData.currentLevel] = StarsAcquired();
        }
        SaveData newData = new SaveData(
            PlayerData.starNumberPerLevel,
            PlayerData.unlockedShapes,
            PlayerData.unlockedTails
            );

        SaveGame.Save<SaveData>("data", newData);
    }

    public void RestartLevel()
    {
        iTween.PunchPosition(mainCamera.gameObject, 10 * Vector3.one, 0.5f);
        firstRun = false;
        GenerateLevel();

    }

    public int BallsToUse()
    {
        return ballsToUse;
    }

    public void RotateFloor()
    {
        iTween.ScaleTo(floor.gameObject, iTween.Hash(
            "scale", new Vector3(22,32, 1),
            "oncomplete", "GenerateLevel",
            "oncompletetarget", this.gameObject,
           "easetype", iTween.EaseType.easeOutCubic,
            "time", 0.7f));
        
    }

    public static int StarsAcquired()
    {
        int unneseseryMoves = PlayerBehavoir.numberOfMoves - movesFor3Stars; // how many moves more than it should be 
        if (unneseseryMoves <= 0)  
            return 3;

        else if (unneseseryMoves == 1)
            return 2;

        else if (unneseseryMoves <= 3)
            return 1;

        else 
            return 1;
    }

    private void LerpBeetwenMat(float param)
    {
        floorMat.SetFloat("_Blend", param);
    }
}
