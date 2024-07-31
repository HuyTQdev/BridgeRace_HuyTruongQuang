using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField]private Vector3 offset;
    [SerializeField]private float speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * speed);
    }

    private void OnEnable()
    {
        EventManager.Instance.StartListening("EndGame", SetTarget);
    }


    private void OnDisable()
    {
        if(!EventManager.CheckNull())
            EventManager.Instance.StopListening("EndGame", SetTarget);
    }
    private void SetTarget(object[] parameters)
    {
        if(parameters.Length > 0 && parameters[0] is Character)
        {
            this.target = ((Character)parameters[0]).transform;
        }
    }
}
