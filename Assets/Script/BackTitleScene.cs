using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTitleScene : MonoBehaviour
{


    [SerializeField, HeaderAttribute("ここに次に進むシーン名を")]
    public string NextScene = "次に進むシーン"; // 遷移先のシーン名をここに書く

    public void OnStartButton()
    {
        // シーン名を引数で受け取る
        string sceneName = NextScene; // ここは実際のシーン名に置き換えてください
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
