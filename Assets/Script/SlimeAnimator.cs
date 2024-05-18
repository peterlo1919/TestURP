using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimator : MonoBehaviour
{
    private Animator anim;
    private EnemyController enemyController;
    [SerializeField] public int damage = 10;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private VFXManager vFXManager;
    

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox" || other.tag == "Missile")
        {
            anim.SetBool("GetHurt", true);
            vFXManager.enemyHurtVFX.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            vFXManager.enemyHurtVFX.Play();
            soundManager.PlayHurtSE();
            enemyController.TakeDamage(damage);
        }
    }

    public void TurnToIdle()
    {
        anim.SetBool("GetHurt", false);
    }
}
