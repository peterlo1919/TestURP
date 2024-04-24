using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    [SerializeField] private GameObject[] haveKnockBack;
    [SerializeField] CharacterController targetCharacter;
    [SerializeField] MPBarController _MPBarController;
    [SerializeField] GameObject targetGameObject;
    [SerializeField] float restore_MP;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("HIT");
            Attack();
            for (int i = 0; i < haveKnockBack.Length; i++)
            {
                if(haveKnockBack[i].active == true)
                {
                    KnockBack(other.gameObject);
                }
            }
        }
    }

    void KnockBack(GameObject enemy_object)
    {
        Rigidbody enemy_rb = enemy_object.GetComponent<Rigidbody>();
        enemy_rb.AddForce(transform.up * 200.0f);
        enemy_rb.AddForce(transform.forward * 200.0f);
    }

    public void Attack()
    {
        targetCharacter = targetGameObject.GetComponent<CharacterController>();
        targetCharacter.currentMP += restore_MP;
        targetCharacter.currentMP = Mathf.Clamp(targetCharacter.currentMP, 0f, targetCharacter.maxMP);
        _MPBarController.UpdateMPBar();
    }
}
