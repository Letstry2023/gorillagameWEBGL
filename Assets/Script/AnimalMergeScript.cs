using UnityEngine;

public class AnimalMergeScript : MonoBehaviour
{
    [Header("この動物のレベル（段階）")]
    public int level = 1; // この動物のレベル（段階）
    public GameObject nextLevelPrefab; // 次のレベルのプレハブ（合体後に出すもの）
    public int maxLevel = 10;  // 最後のレベルを設定
    public AudioClip mergeSound; // 合体時のサウンド

    private bool isMerging = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMerging) return; // すでに合体中なら処理しない

        AnimalMergeScript other = collision.GetComponent<AnimalMergeScript>();
        if (other != null)
        {
            // 同じレベルで、両方合体中でなければ合体開始
            if (other.level == level && !other.isMerging && !other.isMerging
        && level < maxLevel)
            {
                StartCoroutine(Merge(other));
            }
        }
    }

    private System.Collections.IEnumerator Merge(AnimalMergeScript other)
    {
        isMerging = true;
        other.isMerging = true;
        // サウンド再生
        if (mergeSound != null)
        {
            // AudioSourceを一時的に生成して再生
            GameObject audioObj = new GameObject("MergeSound");
            AudioSource source = audioObj.AddComponent<AudioSource>();
            source.clip = mergeSound;
            source.Play();
            Destroy(audioObj, mergeSound.length);
        }
        // 合体成功時に合体後のレベルを渡す例
        int mergedLevel = level + 1; // 合体後のレベルを計算して渡す
        GGameScoreManagerScript.Instance.AddMergeScore(mergedLevel);
        // 合体アニメーションやエフェクト入れてもOK
        yield return new WaitForSeconds(0.2f);

        // 次のレベルがあれば生成
        if (nextLevelPrefab != null)
        {
            Instantiate(nextLevelPrefab, transform.position, Quaternion.identity);
        }

        // 合体前の動物を消す
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
