using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    public float ProcentOpen; // 0 - 100 

    public bool IsReverse = false;
    //public GameObject Orientir;

    float baseAngle;
    float sensetivity = 1f;
    Vector3 oldEulerAngle;

    public float MaxAngle;
    public float MinAngle;
    public float currAngle;

    private void Update()
    {
        currAngle = transform.localEulerAngles.x;
    }


    private void OnMouseDrag()
    {
        oldEulerAngle = transform.localEulerAngles;

        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // use just transform.position
        pos = Input.mousePosition - pos;

        float ang = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - baseAngle;
        ang *= IsReverse ? -1 : 1;

        Vector3 rot = new Vector3( ang * sensetivity, transform.localEulerAngles.y, transform.localEulerAngles.z);
        rot.x = Mathf.Clamp(rot.x, MinAngle, MaxAngle);

        ProcentOpen = (rot.z / 90f) * 100f;
        transform.localEulerAngles = rot;

        //oldEulerAngle = transform.localEulerAngles;

        //Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        //print("pos = " + pos);
        //pos = Input.mousePosition - pos;


        //float ang = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - baseAngle;

        //if (IsReverse)
        //    ang *= -1f;

        ////print("pos = " + ang);
        ////print("quternion = " + this.transform.localEulerAngles.z);
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, ang * sensetivity);
    }

    private void OnMouseDown()
    {
        // Reset
        //Orientir.transform.position = transform.position;
        //oldEulerAngle = Vector3.zero;

        //Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position); // just Use transfrom.position
        //pos = Input.mousePosition - pos;

        //baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        //if (IsReverse)
        //    baseAngle += transform.localEulerAngles.z;
        //else
        //    baseAngle -= transform.localEulerAngles.z;
    }

    private void OnMouseUp()
    {
        oldEulerAngle = transform.localEulerAngles;
        //Orientir.transform.position = transform.position;
    }
}
