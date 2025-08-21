using UnityEngine;

[System.Serializable]
public class AnimalProbability
{
    public GameObject animalPrefab;
    [Range(0f, 1f)] public float probability = 0.1f;
}

public class Dropper : MonoBehaviour
{
    [Header("�����ݒ�")]
    public AnimalProbability[] animalProbabilities; // �m��&�v���r���[�{����
    public Transform spawnPoint; // �����J�n�ʒu
    public float moveSpeed = 5f;
    public float limitX = 2.5f;

    [Header("UI�\���p")]
    public Transform nextAnimalDisplayPos; // ���̓�����������ʒu�iUI���ʓ��̔C�ӂ̏ꏊ�j

    private GameObject currentAnimal;      // ������������Animal
    private GameObject nextAnimalPrefab;   // ���ɏo���\���Animal�v���n�u
    private GameObject nextAnimalDisplay;  // �������̕\���p�C���X�^���X
    private Vector3 originalScale;


    [System.Serializable]
    public class AnimalProbability
    {
        public GameObject animalPrefab;
        [Range(0f, 1f)] public float probability = 0.1f;
        [Header("�v���r���[�{��")]
        public float previewScale = 1.0f; // �ǉ�: �v���r���[�p�̃X�P�[��
    }

    void Start()
    {
        // �ŏ��̎�Animal�����߂Ă���
        PickNextAnimal();
        // �ŏ��̗����pAnimal��p��
        PrepareCurrentAnimal();
    }

    void Update()
    {
        // ���ړ�
        float h = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * h * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -limitX, limitX), transform.position.y, transform.position.z);

        // �v���r���[�����̈ʒu�Ǐ]
        if (currentAnimal != null)
            currentAnimal.transform.position = spawnPoint.position;

        // ��������
        if (Input.GetMouseButtonDown(0))
            DropAnimal();
    }

    // ���݂̗����pAnimal�𐶐�
    void PrepareCurrentAnimal()
    {
        // ��Animal��{�̂Ƃ��Đ���
        currentAnimal = Instantiate(nextAnimalPrefab, spawnPoint.position, Quaternion.identity);

        // ���X�P�[���ۑ�
        originalScale = currentAnimal.transform.localScale;

        // �v���r���[�{�����f
        var ap = GetProbabilityData(nextAnimalPrefab);
        if (ap != null)
            currentAnimal.transform.localScale = originalScale * ap.previewScale;

        // �d��OFF
        var rb = currentAnimal.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        // ����Animal�����߂�UI�\���p�C���X�^���X�X�V
        PickNextAnimal();
    }

    // ��Animal�������_���I�o����UI�\��
    void PickNextAnimal()
    {
        int index = GetRandomAnimalIndex();
        var ap = animalProbabilities[index];
        nextAnimalPrefab = ap.animalPrefab;

        // ���łɕ\���C���X�^���X������Δj��
        if (nextAnimalDisplay != null)
            Destroy(nextAnimalDisplay);

        // �\���p�ɐ���
        nextAnimalDisplay = Instantiate(nextAnimalPrefab, nextAnimalDisplayPos.position, Quaternion.identity);
        nextAnimalDisplay.transform.localScale = ap.animalPrefab.transform.localScale * ap.previewScale;

        // �\���p�͕�������
        var rb = nextAnimalDisplay.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;
    }

    void DropAnimal()
    {
        if (currentAnimal != null)
        {
            // �X�P�[�������ɖ߂�
            currentAnimal.transform.localScale = originalScale;

            // �d��ON
            var rb = currentAnimal.GetComponent<Rigidbody2D>();
            if (rb != null) rb.simulated = true;

            currentAnimal = null;

            // ��Animal�𗎉��p�Ƃ��ėp��
            Invoke(nameof(PrepareCurrentAnimal), 0.5f);
        }
    }

    int GetRandomAnimalIndex()
    {
        float total = 0f;
        foreach (var ap in animalProbabilities) total += ap.probability;

        float r = Random.Range(0f, total);
        float sum = 0f;
        for (int i = 0; i < animalProbabilities.Length; i++)
        {
            sum += animalProbabilities[i].probability;
            if (r <= sum) return i;
        }
        return animalProbabilities.Length - 1;
    }

    AnimalProbability GetProbabilityData(GameObject prefab)
    {
        foreach (var ap in animalProbabilities)
        {
            if (ap.animalPrefab == prefab)
                return ap;
        }
        return null;
    }
}

