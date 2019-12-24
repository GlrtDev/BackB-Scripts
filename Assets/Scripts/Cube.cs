using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 HitScale = new Vector3(0.25f,0.25f,0.25f);
    public Vector3 zeroScale = Vector3.zero;

    public virtual void OnCollisionEnter(Collision collision)
    {
       
    }
    //public virtual void OnEnable()
    //{
    //    iTween.ScaleTo(this.gameObject, iTween.Hash(
    //        "scale", startScale,
    //        "time", 1.5f));
    //}
    //public virtual void OnDisable()
    //{
    //    iTween.ScaleTo(this.gameObject, iTween.Hash(
    //        "scale", zeroScale,
    //        "time", 0.5f));
    //}
}
