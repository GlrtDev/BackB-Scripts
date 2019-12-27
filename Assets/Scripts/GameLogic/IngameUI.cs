using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class IngameUI : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public GameObject summaryUI;
    public GameObject controlButtons;
    public TMP_Text ballLeftUI;
    public GameObject stars;
    public Image starsFill;
    //public static PlayerUIEvent playerUIEvent;
    // Start is called before the first frame update

    void Start()
    {
        UpdateText();
        
    }

    public void ShowSummaryUI()
    {
        starsFill.fillAmount = 0.33f * LevelGenerator.StarsAcquired();
        summaryUI.SetActive(true);
    }

    public void NextLevelClick()
    {
        HideSummaryUI();
        levelGenerator.NextMap();
    }

    private void HideSummaryUI() //TO DO MAKE ANIMATIONS FOR UI
    {
        summaryUI.SetActive(false);
    }

    public void RestartClick()
    {
        HideSummaryUI();
        levelGenerator.RestartLevel();
    }

    public void UpdateText()
    {
        ballLeftUI.text = "Balls Left : " + PlayerBehavoir.BallsLeft().ToString();
       // Debug.Log("apdejt");
    }

    public void GoToMenu()
    {
        StartCoroutine(LoadMenuScene());
    }

    IEnumerator LoadMenuScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu Scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
