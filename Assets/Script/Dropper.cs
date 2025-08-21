using UnityEngine;

[System.Serializable]
public class AnimalProbability
{
    public GameObject animalPrefab;
    [Range(0f, 1f)] public float probability = 0.1f;
}

public class Dropper : MonoBehaviour
{
    [Header("動物設定")]
    public AnimalProbability[] animalProbabilities; // 確率&プレビュー倍率つき
    public Transform spawnPoint; // 落下開始位置
    public float moveSpeed = 5f;
    public float limitX = 2.5f;

    [Header("UI表示用")]
    public Transform nextAnimalDisplayPos; // 次の動物を見せる位置（UIや画面内の任意の場所）

    private GameObject currentAnimal;      // 落下準備中のAnimal
    private GameObject nextAnimalPrefab;   // 次に出す予定のAnimalプレハブ
    private GameObject nextAnimalDisplay;  // 次動物の表示用インスタンス
    private Vector3 originalScale;


    [System.Serializable]
    public class AnimalProbability
    {
        public GameObject animalPrefab;
        [Range(0f, 1f)] public float probability = 0.1f;
        [Header("プレビュー倍率")]
        public float previewScale = 1.0f; // 追加: プレビュー用のスケール
    }

    void Start()
    {
        // 最初の次Animalを決めておく
        PickNextAnimal();
        // 最初の落下用Animalを用意
        PrepareCurrentAnimal();
    }

    void Update()
    {
        // 横移動
        float h = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * h * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -limitX, limitX), transform.position.y, transform.position.z);

        // プレビュー動物の位置追従
        if (currentAnimal != null)
            currentAnimal.transform.position = spawnPoint.position;

        // 落下操作
        if (Input.GetMouseButtonDown(0))
            DropAnimal();
    }

    // 現在の落下用Animalを生成
    void PrepareCurrentAnimal()
    {
        // 次Animalを本体として生成
        currentAnimal = Instantiate(nextAnimalPrefab, spawnPoint.position, Quaternion.identity);

        // 元スケール保存
        originalScale = currentAnimal.transform.localScale;

        // プレビュー倍率反映
        var ap = GetProbabilityData(nextAnimalPrefab);
        if (ap != null)
            currentAnimal.transform.localScale = originalScale * ap.previewScale;

        // 重力OFF
        var rb = currentAnimal.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        // 次のAnimalを決めてUI表示用インスタンス更新
        PickNextAnimal();
    }

    // 次Animalをランダム選出してUI表示
    void PickNextAnimal()
    {
        int index = GetRandomAnimalIndex();
        var ap = animalProbabilities[index];
        nextAnimalPrefab = ap.animalPrefab;

        // すでに表示インスタンスがあれば破棄
        if (nextAnimalDisplay != null)
            Destroy(nextAnimalDisplay);

        // 表示用に生成
        nextAnimalDisplay = Instantiate(nextAnimalPrefab, nextAnimalDisplayPos.position, Quaternion.identity);
        nextAnimalDisplay.transform.localScale = ap.animalPrefab.transform.localScale * ap.previewScale;

        // 表示用は物理無効
        var rb = nextAnimalDisplay.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;
    }

    void DropAnimal()
    {
        if (currentAnimal != null)
        {
            // スケールを元に戻す
            currentAnimal.transform.localScale = originalScale;

            // 重力ON
            var rb = currentAnimal.GetComponent<Rigidbody2D>();
            if (rb != null) rb.simulated = true;

            currentAnimal = null;

            // 次Animalを落下用として用意
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

