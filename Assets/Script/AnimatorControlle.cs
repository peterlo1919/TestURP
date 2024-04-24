using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorControlle : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject hurtVFX_gameobject;
    [SerializeField] private ParticleSystem hurtVFX;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("GetHurt", false);
    }

    public void PlayRun()
    {
        animator.SetBool("isRunning", true);
    }

    public void PlayHurt()
    {
        animator.Play("DAMAGED00");
        hurtVFX_gameobject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        hurtVFX.Play();
    }

    public void PlaySkill1()
    {
        animator.Play("blast");
    }
    public void PlaySkill2()
    {
        animator.Play("Kick");
    }
    public void PlaySkill3()
    {
        animator.Play("Skill");
    }

    public void Reset()
    {
        animator.SetBool("Hit1", false);
        animator.SetBool("Hit2", false);
        animator.SetBool("Hit3", false);
        animator.SetBool("Hit4", false);
        animator.SetBool("isRunning", false);
    }
}
