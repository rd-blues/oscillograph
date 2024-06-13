using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
[RequireComponent(typeof(Outline))]

public class Rotate_Detail : MonoBehaviour
{
    public string _name;
    
    [SerializeField] private float minRotationAngle = 0.0f;
    [SerializeField] private float maxRotationAngle = 90.0f;
    private Quaternion originalRotation;
    private bool isMouseOver = false;
    private bool isMouseDrag = false;
    [SerializeField] private bool reverseRotation = false;
    public float valveAngle = 0.0f;
    public float inpactOnPressure = 0.0f;

    



    private void Start()
    {
        
        originalRotation = transform.localRotation;
        valveAngle = originalRotation.y;
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

            
            transform.localRotation = Quaternion.Euler(-90, 0, valveAngleX.MapInt(0, 90, 0, 2) * 45);
            



            
        }
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
