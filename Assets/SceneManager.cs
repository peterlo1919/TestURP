using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] bool menu;
    [SerializeField] bool ToBattl1;
    [SerializeField] bool ToBattl2;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (ToBattl1 == true)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Battle1");
            }
        }
    }

    public void BAttle2()
    {
        
            UnityEngine.SceneManagement.SceneManager.LoadScene("Battle2");
        
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
