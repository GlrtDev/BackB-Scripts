using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
[System.Serializable]
public class ExitBlockEvent : UnityEvent { }

public class Exit : Cube
{
    public ExitBlockEvent ballAndExitCollision;

    int amountIn;
    public TMP_Text text;

    public virtual void OnEnable()
    {
        Init();
        UpdateText();
    }

    public override void Awake()
    {
        ballsNeededToAction = 1;
        base.Awake();
        Debug.Log("exit init");
        UpdateText();
    }
    public virtual void Init() {
       amountIn = 0;
       //ballsNeededToAction = 1;
    }
    public void OnCollisionEnter(Collision collision)
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
            ballAndExitCollision.Invoke();
        }
    }

    public void BallIn()
    {
        ++amountIn;
        UpdateText();
    }
    public virtual bool isFull()
    {
        if (amountIn >= ballsNeededToAction)
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
        text.text = (ballsNeededToAction - amountIn).ToString();
    }

    public virtual int Capacity()
    {
        return ballsNeededToAction;
    }
}
