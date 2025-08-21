using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GGameScoreManagerScript : MonoBehaviour
{
    public static GGameScoreManagerScript Instance;
    public TMPro.TextMeshProUGUI scoreText; // �X�R�A�\���p�̃e�L�X�g
    private int score = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ���̎��ɌĂ�
    public void AddMergeScore(int level)
    {
        int points = level * 3; // ���x���ɉ����ăX�R�A���v�Z
        score += points;
        scoreText.text = $"{score}"; // �X�R�A���X�V
        //Debug.Log($"���̃X�R�A +{points} (���v: {score})");
    }

    // �X�e�[�W�I�����ɌĂ�
    public void AddGoalScore(int count, int pointsPerAnimal)
    {
        int add = count * pointsPerAnimal;
        score += add;
        Debug.Log($"�B�̓��� {count} �C�� +{add} �|�C���g (���v: {score})");
    }

    public int GetScore()
    {
        return score;
    }

}
