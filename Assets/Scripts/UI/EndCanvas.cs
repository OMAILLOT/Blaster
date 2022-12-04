using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text _finalTime, _amountTarget;

    public void Init()
    {
        PlayerController.Instance.DesactivePlayer();

        Cursor.lockState = CursorLockMode.None;

        _finalTime.text = UIManager.Instance.GameCanvas.MapTime.ToString("N2") + "s";
        _amountTarget.text = TargetManager.Instance.nuberOfTargetHit + "/" + TargetManager.Instance.numberOfTarget;
    }
}
