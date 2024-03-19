using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private RaycastHit hit;
    private Vector3 _currentVelocity = Vector3.zero;

    private Transform m_transsform;
    public float m_distanceAway = 4.5f;


    private void Awake()
    {
        _offset = transform.position - target.position;
        m_transsform = this.transform;
    }

    private void Update()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        RaycastHit hit;
        if (Physics.Linecast(target.position + Vector3.up, m_transsform.position, out hit))
        {
            string name = hit.collider.gameObject.tag;
            if (name != "MainCamera")
            {
                float currentDistance = Vector3.Distance(hit.point, target.position);
                if (currentDistance < m_distanceAway)
                {
                    m_transsform.position = hit.point;
                }
            }
        }
    }
}
