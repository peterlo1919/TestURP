using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class overClock_button : MonoBehaviour
{
    [SerializeField] private GameObject defaulBGM;
    [SerializeField] private GameObject awakeBGM;
    [SerializeField] private Button button;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private SlimeAnimator slimeAnimator;
    [SerializeField] private GameObject awakeVFX;
    [SerializeField] private GameObject VFX;
    [SerializeField] private Slider slider;
    [SerializeField] private bool awake = false;

    void Update()
    {
        if(slider.value ==  slider.maxValue)
        {
            VFX.SetActive(true);
            button.enabled = true;
        }
        if(awake == true)
        {
            DecreaseValue();
            awakeBGM.SetActive(true);
            defaulBGM.SetActive(false);
            button.enabled = false;
        }
        if(awake == true && slider.value == 0)
        {
            Reformation();
        }
    }

    public void IncreaseValue()
    {
        if (awake == false)
        {
            slider.value += 1 * Time.deltaTime;
        }
    }

    public void DecreaseValue()
    {
        slider.value -= 0.7f * Time.deltaTime;
    }

    public void Awaking()
    {
        awake = true;
        awakeVFX.SetActive(true);
        characterController.moveSpeed = characterController.moveSpeed * 3;
        slimeAnimator.damage = slimeAnimator.damage * 3;
    }

    public void Reformation()
    {
        awake = false;
        awakeVFX.SetActive(false);
        characterController.moveSpeed = characterController.moveSpeed/3;
        slimeAnimator.damage = slimeAnimator.damage/3;
    }
}
