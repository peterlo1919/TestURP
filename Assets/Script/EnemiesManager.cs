using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] Vector3 spwanArea;
    [SerializeField] float spwanTimer;
    [SerializeField] GameObject player;
    [SerializeField] GameObject expManager;
    [SerializeField] GameObject effectManager;

    public void SpwanEnemy(EnemyData enemyToSpwan)
    {
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;

        GameObject newEnemy = Instantiate(enemyToSpwan.animatedPrefab);
        newEnemy.transform.position = position;
        EnemyController newEnemyComponent = newEnemy.GetComponent<EnemyController>();
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.SetStats(enemyToSpwan.stats);
        newEnemy.transform.parent = transform;
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spwanArea.x, spwanArea.x);
            position.z = spwanArea.z * f;
        }
        else
        {
            position.z = UnityEngine.Random.Range(-spwanArea.z, spwanArea.z);
            position.x = spwanArea.x * f;
        }


        return position;
    }
}
