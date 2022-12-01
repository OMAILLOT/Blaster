using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text _finalTime, _amountTarget;

    public void Init()
    {
        _finalTime.text = UIManager.Instance.GameCanvas.MapTime.ToString("NN") + "s";
        _amountTarget.text = 20 + "/" + 20;
    }
}
