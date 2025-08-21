using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GG_GameOverZone : MonoBehaviour
{

    public int pointsPerAnimal = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Animal"))
        {
            Debug.Log("�Q�[���I�[�o�[�I");
            // �Q�[���I�[�o�[�������Ăԁi��F�V�[���J�ځAUI�\���Ȃǁj
            GG_GameManagerScript.Instance.GameOver();
        }
    }

    public int CountAnimalsInGoal()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        int count = 0;
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Animal"))
            {
                count++;
            }
        }
        return count;
    }
    // �X�e�[�W�I�����ɌĂ�
    public void OnStageClear()
    {
        int animalCount = CountAnimalsInGoal();
        GGameScoreManagerScript.Instance.AddGoalScore(animalCount, pointsPerAnimal);
        Debug.Log($"�X�e�[�W�N���A�I�X�R�A���v: {GGameScoreManagerScript.Instance.GetScore()}");
    }
}
