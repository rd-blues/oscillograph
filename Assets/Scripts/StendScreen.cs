using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StendScreen : MonoBehaviour
{
    public GameObject ScreenOff;
    public PowerControl Power;
    public PowerStendControl PowerStend;

    public List<GameObject> Screens;
    int indexScreen = 0;

    [Header("Screen value")]
    public TextMeshProUGUI Q1;
    public TextMeshProUGUI P1;
    public TextMeshProUGUI P2;
    public TextMeshProUGUI P4;
    public TextMeshProUGUI P5;

    public void NextScreen()
    {
        if (!IsActiveScreen())
            return;

        indexScreen++;

        if (indexScreen == Screens.Count)
            indexScreen = 0;

        for(int i = 0; i < Screens.Count; i++)
        {
            if (indexScreen == i)
                Screens[i].SetActive(true);
            else
                Screens[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (IsActiveScreen())
        {
            ScreenOff.SetActive(false);

        }
        else
        {
            ScreenOff.SetActive(true);
        }
    }

    public bool IsActiveScreen()
    {
        if (Power.isOn && PowerStend.isOn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void SetValue(float rotShutter)
    {
        if (IsActiveScreen() && rotShutter >= 0)
        {
            float Q = rotShutter / 10.0f;
            float p1 = 0.4139f * Mathf.Pow(Q, 3) - 6.847f * Mathf.Pow(Q, 2) + 29.211f * Q;// 0.2715f * Mathf.Pow(Q, 2) - 6.9485f * Q + 50.545f;
            float p2 = 0.3753f * Mathf.Pow(Q, 3) - 6.3629f * Mathf.Pow(Q, 2) + 27.867f *Q;
            float p4 = 0.4483f * Mathf.Pow(Q, 3) - 7.3972f * Mathf.Pow(Q, 2) + 31.599f * Q;
            float p5 = 0.407f * Mathf.Pow(Q, 3) - 6.543f * Mathf.Pow(Q, 2) + 26.809f * Q;

            Q1.text = System.Math.Round(Q,1).ToString().Replace(',','.');
            P1.text = Mathf.RoundToInt(p1).ToString();
            P2.text = Mathf.RoundToInt(p2).ToString();
            P4.text = Mathf.RoundToInt(p4).ToString();
            P5.text = Mathf.RoundToInt(p5).ToString();
        }
        else
        {
            Q1.text = "0.0";
            P1.text = "0";
            P2.text = "0";
            P4.text = "0";
            P5.text = "0";
        }

    }
}
