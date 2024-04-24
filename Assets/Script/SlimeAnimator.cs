using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimator : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject hurtVFX_gameobject;
    [SerializeField] private ParticleSystem hurtVFX;
    [SerializeField] private AudioSource hurtSE;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox" || other.tag == "Missile")
        {
            anim.SetBool("GetHurt", true);
            hurtVFX_gameobject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            hurtVFX.Play();
            hurtSE.Play();
        }
    }

    public void TurnToIdle()
    {
        anim.SetBool("GetHurt", false);
    }
}
