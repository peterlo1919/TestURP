using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    public int hp = 999;
    public int damage = 1;
    public float speed;
    public GameObject explosion_effect;
    public bool IsBoss;


    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.damage = stats.damage;
        this.speed = stats.speed;
        this.explosion_effect = stats.explosion_effect;
        this.IsBoss = stats.IsBoss;
    }
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float og_speed;
    [SerializeField] private Animator anim;
    [SerializeField] AttackRange attackRange;
    Transform targetDestination;
    [SerializeField] CharacterController targetCharacter;
    [SerializeField] HealthBarController healthBarController;
    [SerializeField] GameObject targetGameObject;
    public EnemyStats stats;
    public EnemyData enemyData;
    private void Awake()
    {
        SetStats(enemyData.stats);
        og_speed = stats.speed;
        anim = GetComponent<Animator>();
        attackRange = GetComponentInChildren<AttackRange>();
    }

    void Update()
    {
        /*float dist = Vector3.Distance(transform.position, player.position);
        if(dist)*/
        Vector3  point = player.position;
        point.y = 0.0f;
        transform.LookAt(point);
        transform.Translate(0, 0, stats.speed);
    }

    void StopAttack()
    {
        attackRange.InAttackRange = false;
        anim.SetBool("IsAttack", false);
        stats.speed = og_speed;
    }

    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }
    public void Attack()
    {
            targetCharacter = targetGameObject.GetComponent<CharacterController>();
            targetCharacter.currentHP -= stats.damage;
            targetCharacter.currentHP = Mathf.Clamp(targetCharacter.currentHP, 0f, targetCharacter.maxHP);
            healthBarController.UpdateHealthBar();
    }
}
