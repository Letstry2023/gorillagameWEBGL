using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{

    [SerializeField, HeaderAttribute("ここに次に進むシーン名を")]
    public string NextScene = "次に進むシーン"; // 遷移先のシーン名をここに書く

    public void OnNextSceneButton()
    {

        // シーン名を引数で受け取る
        string sceneName = NextScene; // ここは実際のシーン名に置き換えてください
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
