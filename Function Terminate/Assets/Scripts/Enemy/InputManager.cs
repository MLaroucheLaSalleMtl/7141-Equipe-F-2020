using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager 
{ 
    public static float MH()
    {
        float r = 0.0f;
        r+=Input.GetAxis("JMH");
        r+=Input.GetAxis("KMH");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float MV()
    {
        float r = 0.0f;
        r += Input.GetAxis("JMV");
        r += Input.GetAxis("KMV");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 MainJoystick()
    {
        return new Vector3(MH(),0, MV());
    }

    public static bool Square() => Input.GetButtonDown("Square");
    public static bool Cross() => Input.GetButtonDown("Cross");
    public static bool Triangle() => Input.GetButtonDown("Triangle");
    public static bool Circle() => Input.GetButtonDown("Circle");
    public static bool R1() => Input.GetButtonDown("R1");
    public static bool R2() => Input.GetButtonDown("R2");
    public static bool L1() => Input.GetButtonDown("L1");
    public static bool L2() => Input.GetButtonDown("L2");
    public static bool Start() => Input.GetButtonDown("Start");

}
