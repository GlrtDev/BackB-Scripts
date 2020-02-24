using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class IngameUI : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public GameObject summaryUI;
    public TMP_Text ballLeftUI;
    public GameObject stars;
    public Image starsFill;
    public TMP_Text movesLeftUI;
    void Start()
    {
        UpdateBallLeftText();
        
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

    public void UpdateBallLeftText()
    {
        ballLeftUI.text = "x" + PlayerBehavoir.BallsLeft().ToString();
    }

    public void UpdateMoveLeftText(int movesLeft)
    {
        movesLeftUI.text = movesLeft.ToString();
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
