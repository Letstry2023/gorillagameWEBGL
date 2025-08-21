using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GGameScoreManagerScript : MonoBehaviour
{
    public static GGameScoreManagerScript Instance;
    public TMPro.TextMeshProUGUI scoreText; // スコア表示用のテキスト
    private int score = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 合体時に呼ぶ
    public void AddMergeScore(int level)
    {
        int points = level * 3; // レベルに応じてスコアを計算
        score += points;
        scoreText.text = $"{score}"; // スコアを更新
        //Debug.Log($"合体スコア +{points} (合計: {score})");
    }

    // ステージ終了時に呼ぶ
    public void AddGoalScore(int count, int pointsPerAnimal)
    {
        int add = count * pointsPerAnimal;
        score += add;
        Debug.Log($"檻の動物 {count} 匹で +{add} ポイント (合計: {score})");
    }

    public int GetScore()
    {
        return score;
    }

}
