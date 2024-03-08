using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Horizontal"))
        {
            Debug.Log("Down");
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            Debug.Log("Up");
        }
    }
}
