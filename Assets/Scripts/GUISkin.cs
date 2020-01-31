using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISkin : MonoBehaviour
{
    public GUISkin MenuSkin;
    bool toggleTxt;
    int toolbarInt = 0;
    public string[] toolbarStrings = new string[] { "Toolbar1", "Toolbar2", "Toolbar3" };
    int selGridInt = 0;
    public string[] selStrings = new string[] { "Grid 1", "Grid 2", "Grid 3", "Grid 4" };
    float hSliderValue = 0;
    float hSbarValue;
 
    private void OnGUI()
    {

        GUI.Box(Rect(Screen.width / 2 - 140, Screen.height / 2 - 140, 300, 300), "This is the title of a box");
        GUI.BeginGroup(new Rect(Screen.width / 2 - 140, Screen.height / 2 - 140, 300, 300));
        GUI.Button(Rect(0, 25, 100, 20), "I am a button");
        GUI.Label(Rect(0, 50, 100, 20), "I'm a Label!");
        toggleTxt = GUI.Toggle(Rect(0, 75, 200, 30), toggleTxt, "I am a Toggle button");
        toolbarInt = GUI.Toolbar(Rect(0, 110, 250, 25), toolbarInt, toolbarStrings);
        selGridInt = GUI.SelectionGrid(Rect(0, 170, 200, 40), selGridInt, selStrings, 2);
        hSliderValue = GUI.HorizontalSlider(Rect(0, 210, 100, 30), hSliderValue, 0.0, 1.0);
        hSbarValue = GUI.HorizontalScrollbar(Rect(0, 230, 100, 30), hSbarValue, 1.0, 0.0, 10.0);
        GUI.EndGroup();
    }

    private Rect Rect(int v1, int v2, int v3, int v4)
    {
        throw new NotImplementedException();
    }
}
