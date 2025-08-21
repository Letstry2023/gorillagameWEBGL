using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GG_GameManagerScript : MonoBehaviour
{
    

    public static GG_GameManagerScript Instance;
    [Header("GoalArea判定")]
    public GG_GameOverZone goalArea;  // Inspectorでセット
    public TMPro.TextMeshProUGUI gameOverScoreText;  // UIテキスト
    public GameObject panel;  // ゲームオーバーパネル
    public GameObject ScoreUI;

    private bool isGameOver = false;

    private void Awake()
    {
        panel.SetActive(false);
        gameOverScoreText.gameObject.SetActive(false);
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        int animalsInGoal = goalArea.CountAnimalsInGoal();
        int goalScore = animalsInGoal * goalArea.pointsPerAnimal;
        int totalScore = GGameScoreManagerScript.Instance.GetScore() + goalScore;

        // ゲームストップ
        Time.timeScale = 0f;
        ScoreUI.SetActive(false);
        panel.SetActive(true);
        gameOverScoreText.gameObject.SetActive(true);
        gameOverScoreText.text = $"ゲームオーバー！\nスコア合計: {totalScore}\n檻の動物: {animalsInGoal}匹 (+{goalScore}ポイント)";

        Debug.Log($"ゲームオーバー スコア合計: {totalScore}");

        // 必要ならここで操作制限やリスタート呼び出しを入れる
    }
}
