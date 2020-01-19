using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject strikingGO;

    public void SoundButtonClick()
    {
        if(AudioListener.volume == 1f)
        {
            strikingGO.SetActive(true); //striking visible
            AudioListener.volume = 0f;
        }
        else
        {
            strikingGO.SetActive(false);
            AudioListener.volume = 1f;
        }

    }
    private void OnEnable()
    {
        if (AudioListener.volume == 0f)
        {
            strikingGO.SetActive(true);
        }
    }

}
