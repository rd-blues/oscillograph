using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUsable : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray mouse_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(mouse_ray, out hit, 100))
            {
                if (hit.collider.GetComponent<IUsable>() != null)
                    hit.collider.GetComponent<IUsable>().Use();
            }
        }
    }
}
