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

    public override void OnEnable()
    {
        amountIn = 0;
        ballsNeededToAction = 1;
        base.OnEnable();
        UpdateText();
    }

    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.tag == "Player")
        {
            if (!isFull())
            {
                FindObjectOfType<AudioManager>().Play("ExitCollision");
                transform.localScale = HitScale;
                iTween.ScaleTo(this.gameObject, iTween.Hash(
                "scale", startScale,
                "time", 0.4f,
                "easeType",iTween.EaseType.easeOutExpo));
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
