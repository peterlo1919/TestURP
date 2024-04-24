using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPBarController : MonoBehaviour
{
    [HeaderAttribute("UI Setting")]
    [SerializeField] public Slider mp_slider;
    [SerializeField] private float fillSpeed;
    [SerializeField] private Image mp_Fill;
    [SerializeField] private CharacterController characterController;

    public void UpdateMPBar()
    {
        float targetFillAmount = characterController.currentMP / characterController.maxMP;
        mp_Fill.DOFillAmount(targetFillAmount, fillSpeed);
    }
}
