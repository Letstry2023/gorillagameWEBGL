using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float limitX = 2.5f;
    public GameObject[] animalPrefabs;
    public Transform spawnPoint;

    private GameObject currentAnimal;

    void Start()
    {
        PrepareNextAnimal();
    }

    void Update()
    {
        // �ړ����́i��� or A/D�j
        float h = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * h * moveSpeed * Time.deltaTime;

        // �͈͐���
        float clampedX = Mathf.Clamp(transform.position.x, -limitX, limitX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // �}�E�X���N���b�N�ŗ���
        if (Input.GetMouseButtonDown(0))
        {
            DropAnimal();
        }
    }

    void PrepareNextAnimal()
    {
        int index = Random.Range(0, animalPrefabs.Length);
        currentAnimal = Instantiate(animalPrefabs[index], spawnPoint.position, Quaternion.identity);
        currentAnimal.GetComponent<Rigidbody2D>().simulated = false;
    }

    void DropAnimal()
    {
        if (currentAnimal != null)
        {
            currentAnimal.transform.position = spawnPoint.position;
            currentAnimal.GetComponent<Rigidbody2D>().simulated = true;
            currentAnimal = null;
            Invoke(nameof(PrepareNextAnimal), 0.5f);
        }
    }
}
