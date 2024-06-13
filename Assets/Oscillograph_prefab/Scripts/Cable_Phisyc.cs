using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable_Phisyc : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform cable1;
    public Transform cable2;
    private Vector3[] vector3 = new Vector3[2];

    void Update()
    {
        vector3[0] = cable1.position;
        vector3[1] = cable2.position;
        lineRenderer.SetPositions(vector3);

    }
}
