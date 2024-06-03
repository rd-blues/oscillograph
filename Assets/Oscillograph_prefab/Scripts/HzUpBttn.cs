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
        if (hz <= 1000 && hz >= 0)
        {
            if (step > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift) && hz <= 900) hz += step * 1000;
                else hz += step;
            }
            else 
            {
                if (hz > 0) 
                {
                    if (Input.GetKey(KeyCode.LeftShift) && hz >= 100 ) hz += step * 1000;
                    else hz += step;
                }
                
            }                
            if (hz % 0.01 != 0) hz = MathF.Round(hz, point);
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
