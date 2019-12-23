using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ExitBlockEvent : UnityEvent<Exit> { }

public class Exit : MonoBehaviour
{
    public ExitBlockEvent onColl;
    int ballsNeededToFill;
    int amountIn = 0;
    public virtual void OnEnable()
    {
        amountIn = 0;
        ballsNeededToFill = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Player")
        {
            if (!isFull())
            {
                collision.collider.gameObject.SetActive(false);
                BallIn();
            }
            onColl.Invoke(this);
        }
    }

    public void BallIn()
    {
        ++amountIn;
    }
    public virtual bool isFull()
    {
        if (amountIn >= ballsNeededToFill)
        {
            Close();
            return true;
        }
        return false;
    }

    public virtual void Close()
    {

    }
}
