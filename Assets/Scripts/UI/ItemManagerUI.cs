using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerUI : MonoBehaviour
{
    public ItemManager itemManager;
    public Transform content;
    void Start()
    {
        if (itemManager)
            Display(itemManager);
    }
    public virtual void Display(ItemManager tm)
    {
        this.itemManager = tm;
        Refresh();
    }

    public virtual void Refresh()
    {
        foreach (Transform t in content)
        {
            Destroy(t.gameObject);
        }

        foreach(GameObject itemPrefab in itemManager.itemPrefabs)
        {
            GameObject Item = Instantiate(itemPrefab, content);
            Item.GetComponent<Tail>().ItemEquipedEvent += Refresh;
        }
    }
}
