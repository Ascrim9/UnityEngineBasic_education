using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputRankName : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    private string playerName = null;
    [SerializeField] private string RankSceen;

    private void Awake()
    {
        playerName = playerNameInput.GetComponent<TMP_InputField>().text;
    }

    public void InputName()
    {
        playerName = playerNameInput.text;
        PlayerPrefs.SetString("Name", playerName);
        SceneManager.LoadScene(RankSceen);
    }
}
