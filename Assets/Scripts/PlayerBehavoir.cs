using System;
using System.Collections;
using System.Collections.Generic;
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

    public void Division()
    {
        if (BallsLeft() > 0) {
            GameObject DividedBall = pool.GetPooledObject(0);
            DividedBall.transform.position = gameObject.transform.position;
            Rigidbody rigidbody1 = DividedBall.GetComponent<Rigidbody>();
            DividedBall.SetActive(true);
            rigidbody1.velocity = Quaternion.Euler(0, 0, 90) * rb.velocity;
            rb.velocity = Quaternion.Euler(0, 0, -90) * rb.velocity;
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
    // void OnCollisionEnter(Collision collision)
    //{
    //Vector3 newSpeed = -rb.velocity;
    // rb.velocity = 2*(-rb.velocity);
    //  }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "block")
    //    rb.velocity = -rb.velocity;
    //}
}
