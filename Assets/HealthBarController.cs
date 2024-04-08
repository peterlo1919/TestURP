using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [HeaderAttribute("UI Setting")]
    [SerializeField] public Slider hp_slider;
    [SerializeField] private float fillSpeed;
    //[SerializeField] private Color less_50percent;
    //[SerializeField] private Color less_30percent;
    [SerializeField] private Text hp_Text;
    [SerializeField] private Image hp_Fill;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private CharacterController characterController;

    private void Awake()
    {
        hp_Text.text = characterController.maxHP.ToString();
    }
    public void UpdateHealthBar()
    {
        float targetFillAmount = characterController.currentHP / characterController.maxHP;
        hp_Fill.DOFillAmount(targetFillAmount, fillSpeed);
        hp_Fill.DOColor(colorGradient.Evaluate(targetFillAmount), fillSpeed);
        hp_Text.text = characterController.currentHP.ToString();
    }
}
