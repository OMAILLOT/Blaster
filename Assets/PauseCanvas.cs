using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text _amountTarget, _hintDesc;

    public void Init()
    {
        _amountTarget.text = 20 + "/" + 20; //Mettre valeur 
        _hintDesc.text = "Behind the waterfall"; //Mettre indice target
    }
}
