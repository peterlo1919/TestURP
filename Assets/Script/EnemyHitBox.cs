using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyController.Attack();
        }
    }
}
