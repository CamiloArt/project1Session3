﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlatform : MonoBehaviour {
    //--PUBLIC VARIABLES--//
    public SelectVehicle selectVehicle;

    public Vector3 PRotationAmount;
    public Vector3 currentRotation;

    public GameObject parentP; //parent platform

    public int PRotationAngle = 120;
    //--PRIVATE VARIABLES--//
    private bool rotate;
    private bool axisInUse = false;

    private float parentSpeed = 5f;
    private float direction;

    void Start()
    {
        PRotationAmount = new Vector3(0, 0, 0);
        currentRotation = new Vector3(0, 0, 0);
    }

    void Update()
    {
        OnInputPress();
    }

    void OnInputPress()
    {
        //Left
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !selectVehicle.selected)
        {
            rotate = true;
            PRotationAmount.y -= PRotationAngle;
            direction = -1;
        }
        if (Input.GetAxisRaw("X360_HorizontalDPad") == -1f && !selectVehicle.selected)
        {
            if (!axisInUse)
            {
                Debug.Log("run");

                axisInUse = true;

                rotate = true;
                PRotationAmount.y -= PRotationAngle;
                direction = -1;
            }
        }
        //Right
        if (Input.GetKeyDown(KeyCode.RightArrow) && !selectVehicle.selected)
        {
            rotate = true;
            PRotationAmount.y += PRotationAngle;
            direction = 1;
        }
        if (Input.GetAxisRaw("X360_HorizontalDPad") == 1f && !selectVehicle.selected)
        {
            if (!axisInUse)
            {
                axisInUse = true;

                rotate = true;
                PRotationAmount.y += PRotationAngle;
                direction = 1;
            }
        }

        if (!rotate)
        {
            axisInUse = false;
        }

        if (rotate)
        {
            RotateParent();
        }
    }

    void RotateParent()
    {
        if (!selectVehicle.selected)
        {
            currentRotation = Vector3.Lerp(currentRotation, PRotationAmount, Time.deltaTime * parentSpeed);
            transform.eulerAngles = currentRotation;
            if (direction == 1)
            {
                if ((currentRotation.y - PRotationAmount.y) > -0.5f)
                {
                    transform.eulerAngles = PRotationAmount;
                    if (PRotationAmount.y == 360)
                    {
                        transform.eulerAngles = Vector3.zero;
                        PRotationAmount.y = 0;
                        currentRotation.y = 0;
                    }
                    rotate = false;
                } 
            }
            else
            {
                if ((currentRotation.y - PRotationAmount.y) < 0.5f)
                {
                    transform.eulerAngles = PRotationAmount;
                    if (PRotationAmount.y == -360)
                    {
                        transform.eulerAngles = Vector3.zero;
                        PRotationAmount.y = 0;
                        currentRotation.y = 0;
                    }
                    rotate = false;
                } 
            }
        }
    }
}