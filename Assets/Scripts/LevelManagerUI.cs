using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerUI : MonoBehaviour
{
    public LevelManager levelManager;
    public Transform content;
    public LevelUI LevelUIPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (levelManager)
            Display(levelManager);
    }
    public virtual void Display(LevelManager lm)
    {
        this.levelManager = lm;
        Refresh();
    }
    public virtual void Refresh()
    {
        foreach (Transform t in content)
        {
            Destroy(t.gameObject);
        }
            foreach (Level lvl in levelManager.levels)
        {
            LevelUI ui = LevelUI.Instantiate(LevelUIPrefab, content);
            ui.Display(lvl);
        }
    }
}
