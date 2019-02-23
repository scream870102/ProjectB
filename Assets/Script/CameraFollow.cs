﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minX,maxX,minY,maxY;
    public Transform target;
    // Start is called before the first frame update
    private Vector3 offset;
    private Vector3 cameraPosition;
    // Use this for initialization
    void Start()
    {
        offset = target.position - this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = cameraPosition;
        cameraPosition=target.position - offset;
        if(cameraPosition.x<minX)
            cameraPosition.x = minX;
        if(cameraPosition.x>maxX)
            cameraPosition.x = maxX;
        if(cameraPosition.y<minY)
            cameraPosition.y = maxY;
        if(cameraPosition.y>maxY)
            cameraPosition.y = maxY;
    }
}
