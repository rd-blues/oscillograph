using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ExtensionMethods;
[RequireComponent(typeof(Outline))]

public class Socket : MonoBehaviour
{
    private bool isMouseOver = false;
    private bool isMouseDrag = false;
    public Transform handBox;
    public Cable_Script cable;
    public Transform socketPoint;
    public bool signalOutput = false;
    public bool sinusoidalType = false;
    public ValueGenerator frequency;
    public ValueGenerator voltage;
    public float hz;
    public float v;
    


    private void Update()
    {
        if (signalOutput == true)
        {
            OutputSocket();
        }
        else InputSocket();

        if (handBox.childCount != 0 && socketPoint.childCount == 0)
        { 
            GetComponent<Outline>().enabled = true;
            
            if (isMouseOver == false && Input.GetMouseButtonDown(0))
            {
                foreach (Transform child in handBox)
                {
                    cable = child.GetComponent<Cable_Script>();
                    child.transform.SetParent(cable.parent1);
                    child.transform.localPosition = new Vector3(0, 0, -0.2766f);
                    child.transform.localRotation = Quaternion.Euler(0, -90, 0);
                }          

            }
            
        }
        else
            GetComponent<Outline>().enabled = false;

    }

    
    private void OnMouseDown()
    {
        foreach (Transform child in handBox)
            {
                child.transform.SetParent(socketPoint);
                child.transform.localPosition = new Vector3(0, 0, 0);
                child.transform.localRotation = Quaternion.Euler(0, 0, 0);
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

    private void OutputSocket()
    {
        hz = frequency.value;
        v = voltage.value;
        if (socketPoint.childCount != 0)
        {
            Transform child = socketPoint.GetChild(0);
            Cable_Script cable_Script = child.GetComponent<Cable_Script>();
            cable_Script.signalOutput = true;
            cable_Script.sinusoidalSignal = sinusoidalType;
            cable_Script.hz = hz;
            cable_Script.v = v;
        }

        
    }

    private void InputSocket()
    {
        if (socketPoint.childCount != 0)
        {
            Transform child = socketPoint.GetChild(0);
            Cable_Script cable_Script = child.GetComponent<Cable_Script>();
            cable_Script.signalOutput = false;
            sinusoidalType = cable_Script.sinusoidalSignal;
            hz = cable_Script.hz;
            v = cable_Script.v;
        }
        else
        {
            hz = 0;
            v = 0;
        }
    }
}
