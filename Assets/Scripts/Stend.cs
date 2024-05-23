using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stend : MonoBehaviour
{
    public PumpControl Pump;
    public List<ShutterUse> Shutters;

    public Shutter Shutter;
    [HideInInspector] public StendScreen screen;
    public static Stend Instance;

    // Start is called before the first frame update
    void Start()
    {
        screen = this.GetComponent<StendScreen>();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Pump.isOn && Shutters.FindAll(x => !x.isOn).Count == 0)
            screen.SetValue(Shutter.currAngle);
        else
            screen.SetValue(-1f);
    }
}
