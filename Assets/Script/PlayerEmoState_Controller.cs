using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmoothShakeFree;

public class PlayerEmoState_Controller : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource hurt_SE;

    public SmoothShake shake;
    public SmoothShakeFreePreset preset;
    
    public void Shake()
    {
        shake.StartShake(preset);
        hurt_SE.Play();
    }
    public void StopShake()
    {
        shake.StopShake();
        anim.SetBool("GetHurt", false);
    }
}
