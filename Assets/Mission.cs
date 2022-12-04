using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission : MonoBehaviour
{
    [SerializeField] TMP_Text _bestTime;

    public void Init(int i)
    {
        if (PlayerPrefs.GetFloat("Mission" + i, 999) == 999)
        {
            _bestTime.text = "Best\n" + "NA";
        }
        else
        {
            _bestTime.text = "Best\n" + PlayerPrefs.GetFloat("Mission" + i).ToString("N2") + "s";
        }
    }
}
