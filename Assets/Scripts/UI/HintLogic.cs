using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintLogic : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        iTween.ScaleTo(this.gameObject, iTween.Hash(
            "scale", Vector3.one,
            "time", 0.5f,
            "delay", 0.3f,
            "easeType", iTween.EaseType.easeInOutExpo));
    }

    public void CloseThis()
    {
        iTween.ScaleTo(this.gameObject, iTween.Hash(
            "scale", Vector3.zero,
            "time", 0.5f,
            "easeType", iTween.EaseType.easeInOutExpo,
            "oncomplete", "Disable",
            "oncompletetarget", this.gameObject));
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
