using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSizeMixin : CubeMixin
{
    public Vector3 scaleVector = Vector3.zero;
    public float speed = 20.0f;
    public float delay = 1.5f;
    public bool CheckForUpdate = false;
    public override void LoadMixin()
    {
        iTween.ScaleTo(cube.gameObject, iTween.Hash(
            "scale", scaleVector,
            "speed", speed,
            "delay", delay,
            "easetype", iTween.EaseType.easeInOutExpo,
            "looptype", iTween.LoopType.pingPong
            ));
    }
    //TO DO , THIS IS TOO SLOW 
    public void Update()
    {
        if (CheckForUpdate)
        {
            if (cube.gameObject.transform.localScale.x < 0.1f)
                cube.gameObject.GetComponent<BoxCollider>().enabled = false;
            else
                cube.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
