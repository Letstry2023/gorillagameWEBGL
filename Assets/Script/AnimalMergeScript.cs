using UnityEngine;

public class AnimalMergeScript : MonoBehaviour
{
    [Header("���̓����̃��x���i�i�K�j")]
    public int level = 1; // ���̓����̃��x���i�i�K�j
    public GameObject nextLevelPrefab; // ���̃��x���̃v���n�u�i���̌�ɏo�����́j
    public int maxLevel = 10;  // �Ō�̃��x����ݒ�
    public AudioClip mergeSound; // ���̎��̃T�E���h

    private bool isMerging = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMerging) return; // ���łɍ��̒��Ȃ珈�����Ȃ�

        AnimalMergeScript other = collision.GetComponent<AnimalMergeScript>();
        if (other != null)
        {
            // �������x���ŁA�������̒��łȂ���΍��̊J�n
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
        // �T�E���h�Đ�
        if (mergeSound != null)
        {
            // AudioSource���ꎞ�I�ɐ������čĐ�
            GameObject audioObj = new GameObject("MergeSound");
            AudioSource source = audioObj.AddComponent<AudioSource>();
            source.clip = mergeSound;
            source.Play();
            Destroy(audioObj, mergeSound.length);
        }
        // ���̐������ɍ��̌�̃��x����n����
        int mergedLevel = level + 1; // ���̌�̃��x�����v�Z���ēn��
        GGameScoreManagerScript.Instance.AddMergeScore(mergedLevel);
        // ���̃A�j���[�V������G�t�F�N�g����Ă�OK
        yield return new WaitForSeconds(0.2f);

        // ���̃��x��������ΐ���
        if (nextLevelPrefab != null)
        {
            Instantiate(nextLevelPrefab, transform.position, Quaternion.identity);
        }

        // ���̑O�̓���������
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
