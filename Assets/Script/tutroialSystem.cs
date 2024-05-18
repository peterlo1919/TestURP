using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutroialSystem : MonoBehaviour
{
    private bool skillHints = false;
    private bool cutScence = false;
    [SerializeField] private GameObject default_BGM;
    [SerializeField] private GameObject win_BGM;
    [SerializeField] private GameObject victory_title;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private Transform winPosition;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject _camera;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private GameObject[] dialog;
    [SerializeField] private GameObject panel;
    private int i = 0;
    void Start()
    {
        FreezeGame();
    }

    private void Update()
    {
        if (enemyController == null && cutScence == false)
        {
            StartCoroutine(winCutScence(_camera, cutScence, winPosition, playerAnimator, victoryScreen, victory_title, win_BGM, default_BGM));
            cutScence = true;
        }
        else if(cutScence == true)
        {
            characterController.enabled = false;
        }
        TutorialDialog();
    }

    private void FreezeGame()
    {
        Time.timeScale = 0;
        characterController.enabled = false;
        enemyController.enabled = false;
    }
    private void UnfreezeGame()
    {
        Time.timeScale = 1;
        characterController.enabled = true;
        enemyController.enabled = true;
    }

    private void TutorialDialog()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (i == 3)
            {
                dialog[3].SetActive(false);
                panel.SetActive(false);
                UnfreezeGame();
            }
            else if (i < 3)
            {
                dialog[i].SetActive(false);
                dialog[i + 1].SetActive(true);
                i++;
            }

            if (skillHints == true && i == 4)
            {
                UnfreezeGame();

                dialog[4].SetActive(false);
            }
        }

        if (characterController.currentMP >= 10 && skillHints == false)
        {
            FreezeGame();
            dialog[4].SetActive(true);
            skillHints = true;
            i++;
        }
    }

    IEnumerator winCutScence(GameObject _camera,bool play,Transform winPosition,Animator animator,GameObject victoryScreen,GameObject victoryTitle,GameObject winBGM, GameObject defaultBGM)
    {
        play = true;
        victoryScreen.SetActive(true);
        characterController.enabled = false;
        yield return new WaitForSeconds(3);
        victoryScreen.SetActive(false);
        characterController.gameObject.transform.position = winPosition.position;
        characterController.gameObject.transform.rotation = winPosition.rotation;
        animator.Play("WIN00");
        victory_title.SetActive(true);
        default_BGM.SetActive(false);
        win_BGM.SetActive(true);
        _camera.SetActive(true);
    }
}
