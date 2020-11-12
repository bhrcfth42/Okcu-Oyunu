﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float minX,maxX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(Mathf.Clamp(playerTransform.position.x,minX,maxX),transform.position.y,transform.position.z);
    }
}
