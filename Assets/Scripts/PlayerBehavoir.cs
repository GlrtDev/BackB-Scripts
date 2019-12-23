using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavoir : MonoBehaviour
{
    public Rigidbody rb;
    public ObjectPooler pool;
    private static float angle = 0.0f;
    public GameObject[] playerSpawns;
    public void Division()
    {
        GameObject DividedBall = pool.GetPooledObject(0);
        DividedBall.transform.position = gameObject.transform.position;
        Rigidbody rigidbody1 = DividedBall.GetComponent<Rigidbody>();
        DividedBall.SetActive(true);
        rigidbody1.velocity = Quaternion.Euler(0, 0, 90) * rb.velocity;
        rb.velocity = Quaternion.Euler(0, 0, -90) * rb.velocity;
        //rigidbody1.angularVelocity = Vector3.zero;
        //rigidbody1.velocity = Vector3.zero;
        //rigidbody1.velocity = Quaternion.AngleAxis(90.0f, Vector3.up) * rigidbody1.velocity;
        //DividedBall = pool.GetPooledObject(0);
        //DividedBall.transform.position = gameObject.transform.position;
        //rigidbody1 = DividedBall.GetComponentInChildren<Rigidbody>();
        //rigidbody1.velocity = Quaternion.AngleAxis(-90.0f, Vector3.up) * gameObject.GetComponent<Rigidbody>().velocity;
        // DividedBall.SetActive(true);


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

    void OnEnable()
    {
        pool = GetComponentInParent<ObjectPooler>();
        rb = gameObject.GetComponent<Rigidbody>();
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
        //animation of destrofying
    }
    // void OnCollisionEnter(Collision collision)
    //{
    //Vector3 newSpeed = -rb.velocity;
    // rb.velocity = 2*(-rb.velocity);
    //  }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "block")
        rb.velocity = -rb.velocity;
    }
}
