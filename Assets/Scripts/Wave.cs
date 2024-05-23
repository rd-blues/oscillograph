using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Wave : MonoBehaviour
{
    LineRenderer lineRenderer;
    public float amplitude;
    public float wavelength;
    public int positionCount;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawSineWave(this.transform.position, amplitude, wavelength);
    }

    void DrawSineWave(Vector3 startPoint, float amplitude, float wavelength)
    {
        float x = 0f;
        float y;
        float k = 2 * Mathf.PI / wavelength;
        lineRenderer.positionCount = positionCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            x += i * 0.001f;
            y = amplitude * Mathf.Sin(k * x);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) + startPoint);
        }
    }
}
