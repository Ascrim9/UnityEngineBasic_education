using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossExplosion : MonoBehaviour
{
    private void OnDestroy()
    {
        SceneManager.LoadScene("InputRank");
    }
}
