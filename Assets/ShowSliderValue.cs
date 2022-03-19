using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderValue : MonoBehaviour
{

    public Slider sliderUI;
    public void Show()
    {
        GetComponent<Text>().text = sliderUI.value.ToString();
    }
    
}
