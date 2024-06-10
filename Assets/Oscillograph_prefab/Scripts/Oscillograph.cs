using System;
using System.Collections.Generic;
using System.Net.Sockets;
using ExtensionMethods;
using ProtoTurtle.BitmapDrawing;
using UnityEngine;


public class Oscillograph : MonoBehaviour
{
    public static Oscillograph instance;
    [SerializeField] private List<Material> materials;
    private bool isOn = false;
    [Header("Buttons")]
    [SerializeField] private Switch powerButton;
    [SerializeField] private Rotated_Detail brightness;    
    [SerializeField] private Rotated_Detail VremyaDelLeft;
    [SerializeField] private Rotated_Detail VremyaDelRight;
    [SerializeField] private Rotated_Detail VoltageDelLeft;
    [SerializeField] private Rotated_Detail VoltageDelRight;
    [Header("Dislpays")]
    [SerializeField] private Color gridColor;
    [SerializeField] private Color frameColor;
    [SerializeField] private GameObject displayGrid;
    [SerializeField] private GameObject displayLine;
    [SerializeField] private GameObject powerLight;
    [Header("Inputs")]
    [SerializeField] private Socket inputSynchA;
    [SerializeField] private float inputSynchAVoltage;
    [SerializeField] private float inputSynchAFrequency;
    [Space]
    [SerializeField] private Socket inputSynchB;
    [SerializeField] private float inputSynchBVoltage;
    [SerializeField] private float inputSynchBFrequency;
    [Space]
    [SerializeField] private Socket plusInput;
    [SerializeField] private float plusInputFrequency;
    [SerializeField] private float plusInputVoltage;
    [Space]
    [SerializeField] private Socket minusInput;
    [SerializeField] private float minusInputFrequency;
    [SerializeField] private float minusInputVoltage;
    [Space]
    [SerializeField] private Socket xInput;
    [SerializeField] private float xInputFrequency;
    [SerializeField] private float xInputVoltage;
    [Header("X coord")]
    [SerializeField] private Color xColor;
    [SerializeField] private float voltageX;
    [SerializeField] private float frequencyX;
    [Header("Y coord")]
    [SerializeField] private Color yColor;
    [SerializeField] private float voltageY;
    [SerializeField] private float frequencyY;
    [Header("Lissajau")]
    [Range(0.1f, 1.0f)]
    [SerializeField] private float pointAccuracy = 0.2f;

    private Texture2D textureGrid;
    private Texture2D textureLine;    
    private float inputSynchAVoltageOld;
    private float inputSynchBVoltageOld;
    private float inputSynchAFrequencyOld;
    private float inputSynchBFrequencyOld;
    private float plusInputVoltageOld;
    private float plusInputFrequencyOld;
    private float minusInputVoltageOld;
    private float minusInputFrequencyOld;
    private float xInputFrequencyOld;
    private float xInputVoltageOld;
    private int textureWidth = 300 / 2;
    private int textureHeight = 300 / 2;

    /*
    private void Awake()
    {
        instance = this;
        //сетка
        textureGrid = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        textureGrid.wrapMode = TextureWrapMode.Clamp;
        textureGrid.filterMode = FilterMode.Point;
        displayGrid.GetComponent<Renderer>().material.mainTexture = textureGrid;
        //графики
        textureLine = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        textureLine.wrapMode = TextureWrapMode.Clamp;
        textureLine.filterMode = FilterMode.Point;
        displayLine.GetComponent<Renderer>().material.mainTexture = textureLine;

        textureLine.DrawFilledRectangle(new Rect(0, 0, textureWidth, textureHeight), Color.clear);
        textureLine.Apply();

        DrawGrid();
        displayLine.SetActive(false);

    }

    private void Update()
    {
        SwitchLedColor();

        if (isOn)
        {
            displayLine.SetActive(true);
            ReadWireData();
            CheckDataUpdate();
            // DrawXLine();
            // DrawYLine();
            // DrawLissajau();
            // DrawAllLines();

        }
        else
        {
            displayLine.SetActive(false);
        }
        textureLine.DrawFilledRectangle(new Rect(0, 0, textureWidth, textureHeight), Color.clear);
        textureLine.Apply();
    }
    private void ReadWireData()
    {
        voltageX = 0;
        voltageY = 0;
        frequencyX = 0;
        frequencyY = 0;
        if (inputSynchA != null)
            if (inputSynchA.insertedObject != null)
            {
                inputSynchAVoltage = inputSynchA.insertedObject.transform.parent.GetComponent<WireRender>().GetWireVoltage();
                inputSynchAFrequency = inputSynchA.insertedObject.transform.parent.GetComponent<WireRender>().GetWireFrequency();

                if (xInput.insertedObject != null)
                {
                    voltageY = inputSynchAVoltage;
                    frequencyY = inputSynchAFrequency;
                }
                else
                {
                    voltageX = inputSynchAVoltage;
                    frequencyX = inputSynchAFrequency;
                }
            }

        if (inputSynchB != null)
            if (inputSynchB.insertedObject != null)
            {
                inputSynchBVoltage = inputSynchB.insertedObject.transform.parent.GetComponent<WireRender>().GetWireVoltage();
                inputSynchBFrequency = inputSynchB.insertedObject.transform.parent.GetComponent<WireRender>().GetWireFrequency();
                voltageY = inputSynchBVoltage;
                frequencyY = inputSynchBFrequency;
            }

        if (plusInput != null)
            if (plusInput.insertedObject != null)
            {
                plusInputFrequency = plusInput.insertedObject.transform.parent.GetComponent<WireRender>().GetWireFrequency();
                plusInputVoltage = plusInput.insertedObject.transform.parent.GetComponent<WireRender>().GetWireVoltage();
                voltageX = plusInputVoltage;
                frequencyX = plusInputFrequency;
            }

        if (minusInput != null)
            if (minusInput.insertedObject != null)
            {
                minusInputFrequency = minusInput.insertedObject.transform.parent.GetComponent<WireRender>().GetWireFrequency();
                minusInputVoltage = minusInput.insertedObject.transform.parent.GetComponent<WireRender>().GetWireVoltage();
                voltageY = minusInputVoltage;
                frequencyY = minusInputFrequency;
            }
        if (xInput != null)
            if (xInput.insertedObject != null)
            {
                xInputFrequency = xInput.insertedObject.transform.parent.GetComponent<WireRender>().GetWireFrequency();
                xInputVoltage = xInput.insertedObject.transform.parent.GetComponent<WireRender>().GetWireVoltage();
                voltageX = xInputVoltage;
                frequencyX = xInputFrequency;
            }
    }
    private void SwitchLedColor()
    {
        if (powerButton.buttonState == ButtonState.pressed)
        {
            powerLight.GetComponent<MeshRenderer>().material = materials[1];
            isOn = true;
        }
        else
        {
            powerLight.GetComponent<MeshRenderer>().material = materials[0];
            isOn = false;
        }
    }
    private void GetdisplayGridBrightness()
    {
        Color color = new Color(255, 255, 255, brightness.dataOut);
        displayGrid.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }
    private void CheckDataUpdate()
    {
        if (inputSynchAVoltage != inputSynchAVoltageOld)
        {
            DrawXLine();
            DrawYLine();
            inputSynchAVoltageOld = inputSynchAVoltage;
        }
        if (inputSynchBVoltage != inputSynchBVoltageOld)
        {
            DrawXLine();
            DrawYLine();
            inputSynchBVoltageOld = inputSynchBVoltage;
        }
        if (inputSynchAFrequency != inputSynchAFrequencyOld)
        {
            DrawXLine();
            DrawYLine();
            inputSynchAFrequencyOld = inputSynchAFrequency;
        }
        if (inputSynchBFrequency != inputSynchBFrequencyOld)
        {
            DrawXLine();
            DrawYLine();
            inputSynchBFrequencyOld = inputSynchBFrequency;
        }

        if (minusInputVoltage != minusInputVoltageOld)
        {
            DrawAllLines();
            minusInputVoltageOld = minusInputVoltage;
        }
        if (minusInputFrequency != minusInputFrequencyOld)
        {
            DrawAllLines();
            minusInputFrequencyOld = minusInputFrequency;
        }
        if (plusInputVoltage != plusInputVoltageOld)
        {
            DrawAllLines();
            plusInputVoltageOld = plusInputVoltage;
        }
        if (plusInputFrequency != plusInputFrequencyOld)
        {
            DrawAllLines();
            plusInputFrequencyOld = plusInputFrequency;
        }
        if (xInputFrequency != xInputFrequencyOld)
        {
            DrawAllLines();
            xInputFrequencyOld = xInputFrequency;
        }
        if (xInputVoltage != xInputVoltageOld)
        {
            DrawAllLines();
            xInputVoltageOld = xInputVoltage;
        }
    }
    private void DrawGrid()
    {
        //clear screen
        textureGrid.DrawFilledRectangle(new Rect(0, 0, textureWidth, textureHeight), Color.clear);

        for (int i = 0; i <= textureWidth; i += textureWidth / 10)
        {
            textureGrid.DrawLine(i, 0, i, textureHeight, gridColor);
        }
        for (int i = 0; i <= textureHeight; i += textureHeight / 10)
        {
            textureGrid.DrawLine(0, i, textureWidth, i, gridColor);
        }

        textureGrid.DrawLine(0, textureHeight / 2, textureWidth, textureHeight / 2, frameColor);
        textureGrid.DrawLine(textureWidth / 2, 0, textureWidth / 2, textureHeight, frameColor);
        textureGrid.DrawRectangle(new Rect(0, 0, textureWidth, textureHeight), frameColor);

        // textureGrid.DrawPixel(0, textureHeight / 2, Color.blue);

        textureGrid.Apply();

    }
    private void DrawXLine()
    {
        // textureLine.DrawFilledRectangle(new Rect(0, 0, textureWidth, textureHeight), Color.clear);

        if (inputSynchA.sygnalType == Socket.SygnalType.sinusoidal)
        {
            int lastX = 0;
            int lastY = (textureHeight / 2);

            for (int i = 0; i < textureWidth; i++)
            {
                float y = Mathf.Sin(2 * Mathf.PI * -frequencyX * (i * VremyaDelLeft.dataOut * 10f) / textureWidth) * (voltageX * VoltageDelLeft.dataOut * 15f);

                // textureLine.DrawPixel(i, (int)y + (textureHeight / 2), Color.blue);
                textureLine.DrawLine(lastX, lastY, i, (int)y + (textureHeight / 2), xColor);

                lastX = i;
                lastY = (int)y + (textureHeight / 2);
            }
            textureLine.Apply();
        }
        else
        {
            int lastX = 0;
            int lastY = (textureHeight / 2);

            for (int i = 0; i < textureWidth; i++)
            {
                float y = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * -frequencyX * (i * VremyaDelLeft.dataOut * 10f) / textureWidth)) * (voltageX * VoltageDelLeft.dataOut * 15f);

                // textureLine.DrawPixel(i, (int)y + (textureHeight / 2), Color.blue);
                textureLine.DrawLine(lastX, lastY, i, (int)y + (textureHeight / 2), xColor);

                lastX = i;
                lastY = (int)y + (textureHeight / 2);
            }
            textureLine.Apply();
        }
    }
    public void DrawAllLines()
    {

        if (xInput.insertedObject != null && inputSynchB.insertedObject != null)
        {
            if (xInput.sygnalType == Socket.SygnalType.sinusoidal && inputSynchB.sygnalType == Socket.SygnalType.sinusoidal)
            {
                DrawLissajau();
            }
        }
        else if (xInput.insertedObject != null && inputSynchA.insertedObject != null)
        {
            if (xInput.sygnalType == Socket.SygnalType.sinusoidal && inputSynchA.sygnalType == Socket.SygnalType.sinusoidal)
            {
                DrawLissajau();
            }
        }
        else
        {
            if (inputSynchA.insertedObject != null) DrawXLine();
            if (inputSynchB.insertedObject != null) DrawYLine();
        };
    }
    private void DrawYLine()
    {
        // textureLine.DrawFilledRectangle(new Rect(0, 0, textureWidth, textureHeight), Color.clear);

        // int lastX = (textureWidth / 2);
        // int lastY = 0;

        // for (int i = 0; i < textureHeight; i++)
        // {
        //     float x = Mathf.Cos(2 * Mathf.PI * -frequencyY * i / textureHeight) * voltageY;

        //     // textureLine.DrawPixel((int)x + (textureWidth / 2), i, Color.blue);
        //     textureLine.DrawLine(lastX, lastY, (int)x + (textureWidth / 2), i, Color.blue);

        //     lastX = (int)x + (textureWidth / 2);
        //     lastY = i;
        // }
        if (inputSynchB.sygnalType == Socket.SygnalType.sinusoidal)
        {
            int lastX = 0;
            int lastY = (textureHeight / 2);

            for (int i = 0; i < textureWidth; i++)
            {
                float y = Mathf.Sin(2 * Mathf.PI * -frequencyY * (i * VremyaDelRight.dataOut * 10f) / textureWidth) * (voltageY * VoltageDelRight.dataOut * 15f);

                // textureLine.DrawPixel(i, (int)y + (textureHeight / 2), Color.blue);
                textureLine.DrawLine(lastX, lastY, i, (int)y + (textureHeight / 2), yColor);

                lastX = i;
                lastY = (int)y + (textureHeight / 2);
            }

            textureLine.Apply();
        }
        else
        {
            int lastX = 0;
            int lastY = (textureHeight / 2);

            for (int i = 0; i < textureWidth; i++)
            {
                float y = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * -frequencyY * (i * VremyaDelRight.dataOut * 10f) / textureWidth)) * (voltageY * VoltageDelRight.dataOut * 15f);

                // textureLine.DrawPixel(i, (int)y + (textureHeight / 2), Color.blue);
                textureLine.DrawLine(lastX, lastY, i, (int)y + (textureHeight / 2), yColor);

                lastX = i;
                lastY = (int)y + (textureHeight / 2);
            }

            textureLine.Apply();
        }
    }
    private void DrawLissajau()
    {
        // int lastX = (textureWidth / 2);
        // int lastY = (textureHeight / 2);

        // int lastX;
        // int lastY;

        for (float i = 0; i < 280; i += pointAccuracy)
        {
            float y = Mathf.Cos(2 * Mathf.PI * frequencyY * i / textureHeight) * (voltageY * 15f);
            float x = Mathf.Sin(2 * Mathf.PI * frequencyX * i / textureWidth) * (voltageX * 15f);


            textureLine.DrawPixel((int)x + (textureWidth / 2), (int)y + (textureHeight / 2), xColor);
            // textureLine.DrawLine(lastX, lastY, (int)x + (textureWidth / 2), (int)y + (textureHeight / 2), xColor);

            // lastX = (int)x + (textureWidth / 2);
            // lastY = (int)y + (textureHeight / 2);

        }
        textureLine.Apply();
    }
*/
}
