﻿using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private float yVelocity = 0.5F;
    void Update()
    {
        float newPosition = Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime);
        transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
    }
}