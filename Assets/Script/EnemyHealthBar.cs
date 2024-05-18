using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Camera _cam;

    void Update()
    {
        transform.rotation = _cam.transform.rotation;
        transform.position = target.position + offset;
    }
}
