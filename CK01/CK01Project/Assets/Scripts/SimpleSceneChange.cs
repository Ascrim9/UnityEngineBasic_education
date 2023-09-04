using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneChange : MonoBehaviour
{

    public void SimpleEndingSceneChange()
    {
        SceneManager.LoadScene("EndingScene");
    }
    public void SimpleGameSceneChange()
    {
        Debug.Log("LoadScene");
        SceneManager.LoadScene("GameScene");
    }
    public void SimpleMain02Change()
    {
        SceneManager.LoadScene("Main02");
    }
}
