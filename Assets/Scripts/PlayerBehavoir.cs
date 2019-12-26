using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerUIEvent : UnityEvent { }

public class PlayerBehavoir : MonoBehaviour
{
    public Rigidbody rb;
    public ObjectPooler pool;
    public GameObject[] playerSpawns;
    public static int SpawnCount;
    public IngameUI gameUI;
    private Vector3 oneUnitVector = new Vector3(0.5f, 0.5f, 0.5f);
    public void Division()
    {
        if (BallsLeft() > 0) {
            GameObject DividedBall = pool.GetPooledObject(0);
            DividedBall.transform.position = gameObject.transform.position;
            Rigidbody rigidbody1 = DividedBall.GetComponent<Rigidbody>();
            DividedBall.SetActive(true);
            rigidbody1.velocity = Quaternion.Euler(0, 0, 90) * rb.velocity;
            rb.velocity = Quaternion.Euler(0, 0, -90) * rb.velocity;
            iTween.ScaleTo(this.gameObject, iTween.Hash(
                "scale", oneUnitVector,
                "time", 0.3f));
            iTween.ScaleTo(DividedBall, iTween.Hash(
                "scale", oneUnitVector,
                "time", 0.3f));
        }
    }

    public static int BallsLeft()
    {
        return LevelGenerator.ballsToUse - SpawnCount;
    }

    public void MoveBalls(int direction)
    {
        //pool = GetComponentInParent<ObjectPooler>();
        // rb = GetComponent<Rigidbody>();
        playerSpawns = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerSpawn in playerSpawns)
        {
            rb = playerSpawn.GetComponent<Rigidbody>();
            switch (direction) {
                case 0: rb.AddForce(new Vector3(3, 0, 0), ForceMode.Impulse); break;
                case 1: rb.AddForce(new Vector3(-3, 0, 0), ForceMode.Impulse); break;
                case 2: rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse); break;
                case 3: rb.AddForce(new Vector3(0, -3, 0), ForceMode.Impulse); break;
            }
            //rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
        }

        //Debug.Log("PrintOnEnable: script was start");
    }

    private void Awake()
    {
        GameObject UI =  GameObject.FindGameObjectWithTag("IngameUI");
        gameUI = UI.GetComponent<IngameUI>();
    }

    void OnEnable()
    {
        pool = GetComponentInParent<ObjectPooler>();
        rb = gameObject.GetComponent<Rigidbody>();
        ++SpawnCount;
        Debug.Log(" LOG: " + SpawnCount + " level shit: " + LevelGenerator.ballsToUse);
        gameUI.UpdateText();
        //if (angle < 360.0f)
        //    angle += 45.0f;
        // else
        //     angle = 0.0f;
        //rb.AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
        // rb.velocity = Quaternion.AngleAxis(angle, Vector3.right) * new Vector3(1, -1, 0);
        //rb.velocity = Vector3.zero;
        // Debug.Log("PrintOnEnable: script was enabled");
    }

    private void OnDisable()
    {
        --SpawnCount;
        //IngameUI.playerUIEvent.Invoke();
        gameUI.UpdateText();
        //animation of destrofying
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector3 camPosition = Input.touches[i].position;
                camPosition.z = 63.9f; // distance from player to camera on Z axis
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(camPosition);
                Vector3 playerPos = transform.position;
                touchPos.z = 0; playerPos.z = 0;
                if ((touchPos - playerPos).magnitude < 0.5f)
                    if(Input.touches[i].phase == TouchPhase.Began)
                    Division(); 
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 playerPos = transform.position;
            Vector3 camPosition = Input.mousePosition;
            camPosition.z = 63.9f;
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(camPosition);
            touchPos.z = 0; playerPos.z = 0;
            if ((touchPos - playerPos).magnitude < 0.5f)
                Division();
            Debug.Log("touch: " + touchPos + " player: " + playerPos);
        }
    }
}
