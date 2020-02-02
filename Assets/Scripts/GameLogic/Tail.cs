using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnlockType
{
    ad,
    stars,
    mixed,
};

public class Tail : MonoBehaviour
{
    public event System.Action ItemEquipedEvent;

    public Material trailMat;
    public int index;
    public GameObject equipButton;
    public TMPro.TMP_Text equipButtonText;
    public GameObject unlockButton;
    public UnlockType unlockType;

    public int starsToUnlock;
    public bool unlocked;

    public void Equip()
    {
        PlayerData.currentTail = trailMat;
        Debug.Log(PlayerData.currentTail);
        ItemEquipedEvent();
    }

    private void OnEnable()
    {
        if (PlayerData.unlockedTails.Contains(index))
        {
            unlocked = true;
            equipButton.SetActive(true);
            unlockButton.SetActive(false);
        }
        else
        {
            equipButton.SetActive(false);
            unlockButton.SetActive(true);
        }
        GetComponentInChildren<TrailRenderer>().material = trailMat;
        Refresh();
    }

    public void Refresh()
    {
        if (PlayerData.currentTail == trailMat)
            equipButtonText.text = "Equipped";
        else
            equipButtonText.text = "Equip";
    }
}
