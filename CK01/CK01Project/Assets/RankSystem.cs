using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable]
public struct RankData
{
    public int score;
    public string name;
}

public class RankSystem : MonoBehaviour
{
    private InputRankName inputnameBTN;
    [SerializeField] public int maxRankCount = 5;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Transform panelRankInfo;

    private RankData[] rankDataArray;
    private int curIndex = 0;



    private void Awake()
    {
        rankDataArray = new RankData[maxRankCount];

        LoadRankData();
        CompareRank();
        SaveRankData();
        PrintRankData();
    }


    private void LoadRankData()
    {


        for (int i = 0; i < maxRankCount; ++i)
        {
            rankDataArray[i].score = PlayerPrefs.GetInt("Score" + i);
            rankDataArray[i].name = PlayerPrefs.GetString("Name" + i);
        }


    }

    private void CompareRank()
    {
        RankData curData = new RankData();

        curData.name = PlayerPrefs.GetString("Name");
        curData.score = PlayerPrefs.GetInt("Score");

        for (int i = 0; i < maxRankCount; ++i)
        {
            if (curData.score > rankDataArray[i].score)
            {
                curIndex = i;
                break;
            }
        }

        for (int i = maxRankCount - 1; i > 0; --i)
        {
            rankDataArray[i] = rankDataArray[i - 1];


            if (curIndex == i - 1)
            {
                break;
            }
        }

        rankDataArray[curIndex] = curData;
    }


    private void SpawnText(string print, Color color)
    {
        GameObject clone = Instantiate(textPrefab);

        TextMeshProUGUI text = clone.GetComponent<TextMeshProUGUI>();


        clone.transform.SetParent(panelRankInfo);

        clone.transform.localScale = Vector3.one;

        text.text = print;
        text.color = color;
    }

    public void PrintRankData()
    {


        Color color = Color.white;


        for (int i = 0; i < maxRankCount; ++i)
        {
            color = curIndex != i ? Color.white : Color.yellow;


            SpawnText((i + 1).ToString(), color);
            SpawnText(rankDataArray[i].name.ToString(), color);
            SpawnText(rankDataArray[i].score.ToString(), color);

        }
    }


    private void SaveRankData()
    {
        for (int i = 0; i < maxRankCount; ++i)
        {

            PlayerPrefs.SetString("Name" + i, rankDataArray[i].name);
            PlayerPrefs.SetInt("Score" + i, rankDataArray[i].score);
        }
    }


}
