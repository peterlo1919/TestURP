using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [HeaderAttribute("--------AudioSource SE--------")]
    [SerializeField] private AudioSource punch_SE;
    [SerializeField] private AudioSource hurt_SE;
    [SerializeField] private AudioSource skillHints_SE;

    public void PlayPunchSE()
    {
        punch_SE.Play();
    }

    public void PlayHurtSE()
    {
        hurt_SE.Play();
    }

    public void PlaySkillHintsSE()
    {
        skillHints_SE.Play();
    }
}
