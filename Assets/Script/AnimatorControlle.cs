using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorControlle : MonoBehaviour
{
    private Animator animator;
    [SerializeField] MPBarController _MPBarController;
    [SerializeField] private VFXManager _VFXManager;
    [SerializeField] private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
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
        _VFXManager.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
        _VFXManager.playerHurtVFX.Play();
    }

    public void PlaySkill1()
    {
        characterController.currentMP -= 10;
        characterController.currentMP = Mathf.Clamp(characterController.currentMP, 0f, characterController.maxMP);
        _MPBarController.UpdateMPBar();
        animator.Play("blast");
    }
    public void PlaySkill2()
    {
        characterController.currentMP -= 30;
        characterController.currentMP = Mathf.Clamp(characterController.currentMP, 0f, characterController.maxMP);
        _MPBarController.UpdateMPBar();
        animator.Play("Kick");
    }
    public void PlaySkill3()
    {
        characterController.currentMP -= 100;
        characterController.currentMP = Mathf.Clamp(characterController.currentMP, 0f, characterController.maxMP);
        _MPBarController.UpdateMPBar();
        animator.Play("Skill");
    }

    public void PlayRoll()
    {
        animator.Play("Roll");
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
