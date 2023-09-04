using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class StageEnemyData
{
    public int enemyCount; // 에너미 양
}

[System.Serializable]
public class EnemyPrefabData
{
    public GameObject enemyPrefab; // 에너미 프리팹
    public int spawnWeight; // 스폰 가중치
}

[System.Serializable]
public class BossData
{
    public GameObject bossPrefab; // 보스 프리팹
    public int requiredEnemyCount; // 보스 등장까지 필요한 에너미 수
}

public class EnemySpawner : Singleton<EnemySpawner>
{
    public GameObject stageName;

    [SerializeField] private StageEnemyData[] stageEnemyData; // 스테이지별 에너미 양 데이터
    [SerializeField] private EnemyPrefabData[] enemyPrefabsData;
    [SerializeField] private StageData stageData;
    [SerializeField] private BossData[] bossData; // 스테이지별 보스 데이터

    [HideInInspector] public bool isBossSpawned = false; // 보스가 스폰되었는지 여부
    private int curEnemycnt;
    [HideInInspector] public int curStageIndex = 0; // 현재 스테이지
    private float spawnRate;

    [SerializeField] private float baseSpawnRate = 1.0f; // 기본 스폰률
    [SerializeField] private float spawnRateIncrease = 0.1f; // 스폰률 증가량


    public GameObject[] bossWarning;

    private void Start()
    {
        spawnRate = baseSpawnRate;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        
        while (true)
        {

            if(!isBossSpawned)
            {
                float posY = Random.Range(stageData.LimitMin.y + 0.5f, stageData.LimitMax.y + -0.5f);
                int enemyPrefabIndex = GetRandomEnemyPrefabIndex();
                GameObject enemyClone = Instantiate(enemyPrefabsData[enemyPrefabIndex].enemyPrefab, new Vector3(stageData.LimitMax.x + 0.5f, posY), Quaternion.identity);
                curEnemycnt++;

                // 스테이지 증가 체크
                if (curEnemycnt >= stageEnemyData[curStageIndex].enemyCount)
                {
                    if (!isBossSpawned && curStageIndex < bossData.Length && curEnemycnt >= bossData[curStageIndex].requiredEnemyCount)
                    {

                        SpawnBoss();
                    }
                    else
                    {
                        curStageIndex++;

                        if (curStageIndex >= stageEnemyData.Length)
                        {
                            Debug.Log("All stages cleared!");
                            break;
                        }
                        curEnemycnt = 0; 
                        spawnRate = baseSpawnRate - (spawnRateIncrease * curStageIndex); 

                        isBossSpawned = false;

                        yield return new WaitForSeconds(3.0f);
                    }
                }
                int minLevel = curStageIndex * 5 + 1; // 스테이지에 따른 최소 레벨
                int maxLevel = (curStageIndex + 1) * 5; // 스테이지에 따른 최대 레벨
                int enemyLevel = Random.Range(minLevel, maxLevel + 1);
                enemyClone.GetComponent<EnemyStats>().Enemy_level = enemyLevel;

                yield return new WaitForSeconds(spawnRate);
            }
            else
            {
                yield return null;
            }
           
        }
    }

    private int GetRandomEnemyPrefabIndex()
    {
        int totalWeight = 0;
        foreach (var prefabData in enemyPrefabsData)
        {
            totalWeight += prefabData.spawnWeight;
        }

        int randomValue = Random.Range(0, totalWeight);
        int currentIndex = 0;
        for (int i = 0; i < enemyPrefabsData.Length; i++)
        {
            currentIndex += enemyPrefabsData[i].spawnWeight;
            if (randomValue < currentIndex)
            {
                return i;
            }
        }

        return 0;
    }

    private void SpawnBoss()
    {

        if (!isBossSpawned && curStageIndex < bossData.Length)
        {

            BossData currentBossData = bossData[curStageIndex];


            if (currentBossData.bossPrefab != null)
            {
              
                Instantiate(currentBossData.bossPrefab, new Vector3(stageData.LimitMax.x + 0.5f, stageData.LimitMax.y * 0.5f), Quaternion.identity);
                isBossSpawned = true;
            }
            else
            {
                
            }
        }
    }
}
