using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTitleScene : MonoBehaviour
{


    [SerializeField, HeaderAttribute("�����Ɏ��ɐi�ރV�[������")]
    public string NextScene = "���ɐi�ރV�[��"; // �J�ڐ�̃V�[�����������ɏ���

    public void OnStartButton()
    {
        // �V�[�����������Ŏ󂯎��
        string sceneName = NextScene; // �����͎��ۂ̃V�[�����ɒu�������Ă�������
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
