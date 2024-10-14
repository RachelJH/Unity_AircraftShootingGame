using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private GameObject textBossWarning;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int maxEnemyCount = 100;

    private void Awake()
    {
        textBossWarning.SetActive(false);
        
        boss.SetActive(false);

        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        int currentEnemyCount = 0;

        while (true)
        {
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);

            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);

            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);

            SpawnEnemyHPSlider(enemyClone);

            currentEnemyCount++;
            if(currentEnemyCount == maxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }

    private IEnumerator SpawnBoss()
    {
        textBossWarning.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        textBossWarning.SetActive(false);
        boss.SetActive(true);

        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);

        sliderClone.transform.SetParent(canvasTransform);

        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositionAutoSetter>().SetUp(enemy.transform);
    
        sliderClone.GetComponent<EnemyHPViewer>().SetUp(enemy.GetComponent<EnemyHP>());
    }
}
