using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GG_GameManagerScript : MonoBehaviour
{
    

    public static GG_GameManagerScript Instance;
    [Header("GoalArea����")]
    public GG_GameOverZone goalArea;  // Inspector�ŃZ�b�g
    public TMPro.TextMeshProUGUI gameOverScoreText;  // UI�e�L�X�g
    public GameObject panel;  // �Q�[���I�[�o�[�p�l��
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

        // �Q�[���X�g�b�v
        Time.timeScale = 0f;
        ScoreUI.SetActive(false);
        panel.SetActive(true);
        gameOverScoreText.gameObject.SetActive(true);
        gameOverScoreText.text = $"�Q�[���I�[�o�[�I\n�X�R�A���v: {totalScore}\n�B�̓���: {animalsInGoal}�C (+{goalScore}�|�C���g)";

        Debug.Log($"�Q�[���I�[�o�[ �X�R�A���v: {totalScore}");

        // �K�v�Ȃ炱���ő��쐧���⃊�X�^�[�g�Ăяo��������
    }
}
