using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    private bool touchBegan = false;
    void OnEnable()
    {
        var recognizer = new TKSwipeRecognizer();
        recognizer.gestureRecognizedEvent += PlayerBehavoir.MoveBalls;
        TouchKit.addGestureRecognizer(recognizer); //TO DO ADD SIMPLE TOUCH RECIGNIZER
    }

    void OnDisable()
    {
        TouchKit.removeAllGestureRecognizers();
    }

    private void Update() // only for taps
    {
        if (Input.touchCount > 0)
        {
            GameObject[] playerSpawns = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < Input.touchCount; i++)
            {
                foreach (GameObject playerSpawn in playerSpawns)
                {
                    Vector3 camRelativePosition = Input.touches[i].position;               
                    Vector3 playerPos = playerSpawn.transform.position;
                    camRelativePosition.z = playerPos.z - Camera.main.transform.position.z; // distance from player to camera on Z axis
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(camRelativePosition);
                    bool touchEnded = false;
                    touchPos.z = 0; playerPos.z = 0;

                    if ((touchPos - playerPos).magnitude < 0.5f)
                    {
                        switch (Input.touches[i].phase)
                        {
                            case TouchPhase.Ended:
                                if (touchBegan)
                                    playerSpawn.GetComponent<PlayerBehavoir>().Division();
                                    touchEnded = true;
                                break;
                            case TouchPhase.Began:
                                touchBegan = true;
                                break;
                            case TouchPhase.Moved:
                                touchBegan = false;
                                break;

                        }
                    }
                    if(touchEnded == true)
                        break;
                }
            }
        }
    }
}
