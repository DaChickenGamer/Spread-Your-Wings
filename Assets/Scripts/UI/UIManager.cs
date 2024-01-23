using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public Slider slider;

    public void SetMaxProgress(int progress)
    {
        slider.maxValue = progress;
        slider.value = progress;
    }
    
    public void SetProgress(int progress)
    {
        slider.value = progress;
    }
}
