using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ExtensionMethods;
[RequireComponent(typeof(Outline))]


public class HzUpBttn : MonoBehaviour
{
    private bool isMouseOver = false;
    private bool isMouseDrag = false;

    public TMP_Text hzText;
    [SerializeField] private float step = 0.1f;
    public int bigStep;
    public int max;
    public int point; 
    public ValueGenerator valueGeneratorComponent;
    private float hz;

    
    private void Update()
    {

        if (isMouseDrag || isMouseOver)
            GetComponent<Outline>().enabled = true;
        else
            GetComponent<Outline>().enabled = false;

    }

    private void OnMouseDown()
    {
        hz = valueGeneratorComponent.value;
        if (hz <= max && hz >= 0)
        {
            if (step > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift) && hz <= max - bigStep) hz += bigStep;
                else hz += step; 
            }
            else 
            {
                if (hz > 0) 
                {
                    if (Input.GetKey(KeyCode.LeftShift) && hz > 1) hz += bigStep;
                    else hz += step;
                }
                
            }                
            if (hz % 0.01 != 0) hz = MathF.Round(hz, point);
            if (hz < 0) hz = 0;
            valueGeneratorComponent.value = hz;
            string hz1 = Convert.ToString(hz);
            hzText.text = hz1;
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
