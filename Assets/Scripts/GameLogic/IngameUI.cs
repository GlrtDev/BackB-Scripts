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
        summaryUI.SetActive(true);
        summaryUI.transform.localScale = Vector3.zero;
        iTween.ScaleTo(summaryUI, iTween.Hash(
                "scale", Vector3.one,
                "time", 0.5f,
                "easetype",iTween.EaseType.easeInOutCubic));
        starsFill.fillAmount = 0.33f * LevelGenerator.StarsAcquired();
    }

    public void NextLevelClick()
    {
        HideSummaryUI();
        levelGenerator.NextMap();
    }

    private void HideSummaryUI() //TO DO MAKE ANIMATIONS FOR UI
    {
        iTween.ScaleTo(summaryUI, iTween.Hash(
                "scale", Vector3.zero,
                "time", 0.5f,
                "easetype", iTween.EaseType.easeInOutCubic));
        //summaryUI.SetActive(false);
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
