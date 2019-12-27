using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : Cube
{
    LevelGenerator levelGenerator;

    public override void OnEnable()
    {
        base.OnEnable();
        levelGenerator = GetComponentInParent<LevelGenerator>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            iTween.ScaleTo(this.gameObject, iTween.Hash(
                "scale", new Vector3(4,4,4),
                "time", 1.0f,
              //  "delay", 0.3f,
               // "looptype", iTween.LoopType.pingPong,
                "easetype", iTween.EaseType.spring,
                "oncomplete","RestartLevel",
                "oncompletetarget",levelGenerator.gameObject
                ));
        }
    }
}
