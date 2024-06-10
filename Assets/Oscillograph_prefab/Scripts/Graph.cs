using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{
    [SerializeField] GameObject pointPref;
    [SerializeField] Transform parent;
    public float a;
    private float oldA;

    private void Awake()
    {
        oldA = a;
    }
    private void Update()
    {
        
        
        if (a != oldA) 
        {
            
            oldA = a;
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
            var position = Vector3.zero;
            float x, y;
            x = -25;
            for (int i = -100; i <= 100; i++)
            {
                GameObject point = Instantiate(pointPref, parent);                
                y = Mathf.Sin(x);
                x += 0.25f;
                position.x = x;
                position.y = y * a;
                point.transform.localPosition = position;
            }
        }
        
    }

}
