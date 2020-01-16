using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


[System.Serializable]
public class LevelUiEvent : UnityEvent<LevelUI> {}

public class LevelUI : MonoBehaviour
{
    public Level level;
    public int index;
    public int stars;
    public Image starsImg;
    public TMP_Text buttonText;
    public LevelUiEvent onClicked;
    public Image levelImg;
    [SerializeField]
    private Color colorMask;
    void Start()
    {
        if (level)
            Display(level);
        buttonText = GetComponentInChildren<TMP_Text>();
        levelImg = GetComponent<Image>();
        buttonText.text = index.ToString();
        starsImg.fillAmount = 0.33f * stars;
        levelImg.color = Random.ColorHSV(0f,1f, 0.4f,0.7f,0.9f, 1.0f);
        levelImg.color *= colorMask;
    }

    public virtual void Display(Level lvl)
    {
        this.level = lvl;
        this.index = lvl.index;
        this.stars = lvl.stars;
    }
    public virtual void Click()
    {
        onClicked.Invoke(this);
    }
}
