using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    public StendScreen Screen;

    private void OnMouseDown()
    {
        Screen.NextScreen();

        // Animation
        Vector3 direction = transform.forward * -0.0015f;
        transform.position += direction;
    }

    private void OnMouseUp()
    {


        // Animation
        Vector3 direction = transform.forward * -0.0015f;
        transform.position -= direction;
    }
}
