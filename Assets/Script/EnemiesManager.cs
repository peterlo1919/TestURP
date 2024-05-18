using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private bool notEmpty = true;
    [SerializeField] GameObject[] enemies;
    [SerializeField] overClock_button overClock_Button;
    [SerializeField] EnemyController enemyController;
    [SerializeField] Dialog dialog;

    private void Update()
    {
        if(notEmpty == true)
        {
            enemyController.enabled = false;
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] == null)
                {
                    enemies[i] = null;
                }
            }

            if((enemies[0] == null) && (enemies[1] == null) && (enemies[2] == null) && (enemies[3] == null) && (enemies[4] == null) && (enemies[5] == null) && (enemies[6] == null) && (enemies[7] == null) && (enemies[8] == null))
            {
                notEmpty = false;
            }
        }
        else
        {
            if (enemyController == null)
            {
                enemyController = null;
                dialog.winning();
            }
            else
            {
                enemyController.enabled = true;
                overClock_Button.IncreaseValue();
            }
        }
    }

}
