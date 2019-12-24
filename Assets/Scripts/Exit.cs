using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
[System.Serializable]
public class ExitBlockEvent : UnityEvent<Exit> { }

public class Exit : Cube
{
    public ExitBlockEvent onColl;
    int ballsNeededToFill;
    int amountIn = 0;
    public TMP_Text text;

    public virtual void OnEnable()
    {
        // base.OnEnable();
        Init();
        UpdateText();
    }

    public virtual void Init() {
        amountIn = 0;
        ballsNeededToFill = 1;
    }
    public override void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.tag == "Player")
        {
            if (!isFull())
            {
                iTween.ScaleFrom(this.gameObject, iTween.Hash(
                "scale", HitScale,
                "time", 0.5f));
                collision.collider.gameObject.SetActive(false);
                BallIn();
            }
            else
                iTween.ColorFrom(this.gameObject, Color.black, 0.5f);
            onColl.Invoke(this);
        }
    }

    public void BallIn()
    {
        ++amountIn;
        UpdateText();
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

    public virtual void UpdateText()
    {
        text.text = (ballsNeededToFill - amountIn).ToString();
    }

    public virtual int Capacity()
    {
        return ballsNeededToFill;
    }
}
