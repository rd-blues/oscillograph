using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
[RequireComponent(typeof(Outline))]

public class Switch : MonoBehaviour
{
    public string _name;
    private bool isMouseOver = false;
    private bool isMouseDrag = false;
        
    public bool switchState = false;


    private void Update()
    {

        if (isMouseDrag || isMouseOver)
            GetComponent<Outline>().enabled = true;
        else
            GetComponent<Outline>().enabled = false;

    }

    private void OnMouseDown()
    {
        if (switchState != true)
        {
            transform.localRotation = Quaternion.Euler(-35, 0, 0);
            switchState = true;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(35, 0, 0);
            switchState = false;
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
