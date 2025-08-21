using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindowScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;                     // �t���X�N���[��OFF
        Screen.SetResolution(800, 480, false);         // ��800, ����480 �̃E�B���h�E
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        float targetAspect = 800f / 480f;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;   // �㉺�ɍ��т�ǉ�
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;   // ���E�ɍ��т�ǉ�
        }
    }


}
