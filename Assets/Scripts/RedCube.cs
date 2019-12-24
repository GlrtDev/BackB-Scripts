﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCube : Cube
{
    public override void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            iTween.ShakeScale(this.gameObject, iTween.Hash(
                "amount", HitScale,
                "time", 0.5f));
            collision.gameObject.SetActive(false);
        }
    }
}
