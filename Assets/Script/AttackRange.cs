using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Animator anim;
    [SerializeField] private float enemySpeed;
    [SerializeField] public bool InAttackRange = false;
    private void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
        enemySpeed = enemyController.stats.speed;
    }
    void Update()
    {
        if(InAttackRange == true)
        {
            Attack_State();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            InAttackRange = true;
        }
    }

    void Attack_State()
    {
        enemyController.stats.speed = 0;
        anim.SetBool("IsAttack", true);
    }

}
