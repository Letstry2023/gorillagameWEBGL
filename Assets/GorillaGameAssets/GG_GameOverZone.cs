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
            Debug.Log("ゲームオーバー！");
            // ゲームオーバー処理を呼ぶ（例：シーン遷移、UI表示など）
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
    // ステージ終了時に呼ぶ
    public void OnStageClear()
    {
        int animalCount = CountAnimalsInGoal();
        GGameScoreManagerScript.Instance.AddGoalScore(animalCount, pointsPerAnimal);
        Debug.Log($"ステージクリア！スコア合計: {GGameScoreManagerScript.Instance.GetScore()}");
    }
}
