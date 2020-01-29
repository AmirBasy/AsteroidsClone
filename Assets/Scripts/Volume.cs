using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider VolumeSlider; //slider per il volume
    public Text Value; //valore dello slider

    public void ValueSlider()
    {
        Value.text = VolumeSlider.value.ToString(); //cambia valore al testo dello slide
    }

    void Update()
    {
        ValueSlider();
    }
}
