using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Playables;

public class CombatSystem : MonoBehaviour
{
    public bool attack;
    private int flag_combo;
    public int count_combo;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnAttack(bool isPress)
    {
        if(isPress == true)
        {
            if(!attack)
            {
                attack = true;

                switch(count_combo)
                {
                    case 0:
                        anim.SetBool("Hit1", true);        
                        break;
                }
            }
        }
    }

    public void ComboEnable()
    {
        if(flag_combo == 0)
        {
            flag_combo = 1;
        }
    }

    public void OnCombo(bool isPress)
    {
        if(isPress = true)
        {
            if(attack && flag_combo == 1)
            {
                flag_combo = 2;
            }
        }
    }

    public void ComboCheck()
    {
        if(flag_combo == 2)
        {
            switch (count_combo)
            {
                case 0:
                   // anim.SetBool("Hit1", false);
                    anim.SetBool("Hit2", true);
                    count_combo = 1;
                    break;
                case 1:
                    //anim.SetBool("Hit2", false);
                    anim.SetBool("Hit3", true);
                    count_combo = 2;
                    break;
                case 2:
                    //anim.SetBool("Hit3", false);
                    anim.SetBool("Hit4", true);
                    count_combo = 3;
                    break;
            }
            flag_combo = 0;
        }
    }

    public void AttackStop()
    {
        if(flag_combo == 1 || flag_combo == 2)
        {
            flag_combo = 0;
        }
        attack = false;
        count_combo = 0;
        anim.SetBool("Hit1", false);
        anim.SetBool("Hit2", false);
        anim.SetBool("Hit3", false);
        anim.SetBool("Hit4", false);
    }
}
