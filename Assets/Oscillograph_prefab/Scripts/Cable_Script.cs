using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ExtensionMethods;
using System.Linq;
[RequireComponent(typeof(Outline))]
public class Cable_Script : MonoBehaviour
{
    private bool isMouseOver = false;
    private bool isMouseDrag = false;

    public Transform parent1;
    public Transform parent2;
    public GameObject child;
    

    
    private void Update()
    {

        if (isMouseDrag || isMouseOver)
            GetComponent<Outline>().enabled = true;
        else
            GetComponent<Outline>().enabled = false;
        /*if (parent2 == child.transform.parent) 
        {
            if (isMouseOver == false && Input.GetMouseButtonUp(0))
            {
                child.transform.SetParent(parent1);
                child.transform.localPosition = new Vector3(0, 0, -0.2766f);
                child.transform.localRotation = Quaternion.Euler(0, -90, 0);
            }
        }*/
    }

    private void OnMouseDown()
    {
        
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

        if (parent2 != child.transform.parent && parent2.childCount == 0)
        {
            child.transform.SetParent(parent2);

            child.transform.localRotation = Quaternion.Euler(0, 0, 0);
            child.transform.localPosition = Vector3.zero;
        }
        
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
