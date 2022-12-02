using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    [SerializeField] TMP_Text sliderValue;
    public void UpdateValue(float value)
    {
        sliderValue.text = value.ToString("");
    }
}
