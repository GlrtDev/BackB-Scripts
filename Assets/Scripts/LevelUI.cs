using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class LevelUiEvent : UnityEvent<LevelUI> {}

public class LevelUI : MonoBehaviour
{
    public Level level;
    public int index;
    public TMP_Text title;

    public LevelUiEvent e;
    void Start()
    {
        if (level)
            Display(level);
    }

    public virtual void Display(Level lvl)
    {
        this.level = lvl;
        this.index = lvl.index;
        this.title.text = lvl.title;
    }
    public virtual void Click()
    {

    }
}
