using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text _amountTarget, _hintDesc;

    public void Init()
    {
        _amountTarget.text = TargetManager.Instance.nuberOfTargetHit + "/" + TargetManager.Instance.numberOfTarget; 
        _hintDesc.text = "Behind the waterfall";
    }
}
