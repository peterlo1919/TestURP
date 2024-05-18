using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject winning_panel;
    [SerializeField] private bool end = false;
    [SerializeField] private bool win = false;
    [SerializeField] private Slider slider;
    [SerializeField] private EnemyController boss;
    [SerializeField] private GameObject[] dialog;
    [SerializeField] private EnemyController[] enemyController;
    [SerializeField] private GameObject panel;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private SceneManager sceneManager;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        FreezeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(win == true && Input.GetMouseButtonDown(0))
        {
            sceneManager.Menu();
        }
        TutorialDialog();
        awakeTuTorial();
    }

    private void TutorialDialog()
    {
        if (Input.GetMouseButtonDown(0) && end == false)
        {
            panel.SetActive(false);
            dialog[0].SetActive(false);
            UnfreezeGame();
            enemyController = null;
            end = true;
        }
    }
    private void FreezeGame()
    {
            Time.timeScale = 0;
            characterController.enabled = false;
        if(end == false)
        {
            for(int i = 0; i < enemyController.Length; i++)
            {
                enemyController[i].enabled = false;
            }
        }
        boss.enabled = false;
        }
    private void UnfreezeGame()
        {
            Time.timeScale = 1;
            characterController.enabled = true;
            boss.enabled = true;
        if(end == false)
        {
            for (int i = 0; i < enemyController.Length; i++)
            {
                enemyController[i].enabled = true;
            }
        }
    }

    private void awakeTuTorial()
    {
        if (slider.value == slider.maxValue)
        {
            FreezeGame();
            if(i == 0)
            {
                dialog[1].SetActive(true);
            }
            if (Input.GetMouseButtonDown(0))
            {
                dialog[1].SetActive(false);
                UnfreezeGame();
                i++;
            }

            if (Input.GetMouseButtonDown(0) && i == 1)
            {
                dialog[2].SetActive(false);
                i++;
            }
        }
    }

    public void winning()
    {
        
            boss = null;
            panel.SetActive(true);
            winning_panel.SetActive(true);
            win = true;
        
    }
}
