using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
[RequireComponent(typeof(Outline))]

public class Rotated_Detail : MonoBehaviour
{
    public string _name;    
    [SerializeField] private float minRotationAngle = 0.0f;
    [SerializeField] private float maxRotationAngle = 360.0f;
    [SerializeField] private int parts;
    [SerializeField] private int stepDegree;
    private Quaternion originalRotation;
    private bool isMouseOver = false;
    private bool isMouseDrag = false;
    [SerializeField] private bool reverseRotation = false;
    public float valveAngle = 0.0f;
    public float inpactOnPressure = 0.0f;
    public float[] floats;
    public float dataOut;
    private int min;
   



    private void Start()
    {
        float m = Mathf.Round(minRotationAngle);
        min = Convert.ToInt32(m);
        originalRotation = transform.localRotation;
        valveAngle = originalRotation.z;
    }

    private void Update()
    {

        if (isMouseDrag || isMouseOver)
            GetComponent<Outline>().enabled = true;
        else
            GetComponent<Outline>().enabled = false;

        if (isMouseDrag)
        {
            if (!reverseRotation)
                valveAngle += Input.GetAxis("Mouse X") * 10;
            else
                valveAngle -= Input.GetAxis("Mouse X") * 10;

            valveAngle = Mathf.Clamp(valveAngle, minRotationAngle, maxRotationAngle);
            int valveAngleX = Convert.ToInt32(valveAngle);


            transform.localRotation = Quaternion.Euler(0, 0, valveAngleX.MapInt(0, 360, 0, parts) * stepDegree);
            
        }

        int normalizedAngle = Mathf.RoundToInt(transform.localRotation.eulerAngles.z) - min;
        if (stepDegree != 0)
        {
            int index = Mathf.FloorToInt((float)normalizedAngle / stepDegree);
            if (index >= 0 && index < floats.Length)
            {
                dataOut = floats[index];
            }
            else
            {
                dataOut = 1;
            }
        }

        /*for (int i = 0; i < floats.Length; i++)
        {
            int targetAngle = min + (i * stepDegree);
            float a = transform.localRotation.eulerAngles.z;
            a = Mathf.Round(a);
            int angle = Convert.ToInt32(a);
            if (angle == targetAngle)
            {
                dataOut = floats[i];
            }
        }*/
    }
    private void OnMouseDrag()
    {
        isMouseDrag = true;
        PlayerController.instance.state = PlayerController.State.Stop;
    }
    private void OnMouseUp()
    {
        isMouseDrag = false;
        PlayerController.instance.state = PlayerController.State.Move;
    }
    private void OnMouseEnter()
    {
        isMouseOver = true;
    }
    private void OnMouseExit()
    {
        isMouseOver = false;
    }
    
} 
