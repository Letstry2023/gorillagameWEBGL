using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{

    [SerializeField, HeaderAttribute("�����Ɏ��ɐi�ރV�[������")]
    public string NextScene = "���ɐi�ރV�[��"; // �J�ڐ�̃V�[�����������ɏ���

    public void OnNextSceneButton()
    {

        // �V�[�����������Ŏ󂯎��
        string sceneName = NextScene; // �����͎��ۂ̃V�[�����ɒu�������Ă�������
        SceneManager.LoadScene(sceneName);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
