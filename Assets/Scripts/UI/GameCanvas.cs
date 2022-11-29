using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text _startCountdown, _timer, _amountTarget, _ammo;
    public float MapTime;
    public bool IsRunning;

    float _tempSpeed,_tempSidedSpeed;

    public void Init()
    {
        _amountTarget.text = 20 + "/" + 20; //Mettre valeur 
        RefreshAmmo();
    }

    public void RefreshAmmo()
    {
        _ammo.text = 20 + "/" + 20;
    }

    public IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(2);

        _tempSpeed = PlayerController.Instance.Speed;
        _tempSidedSpeed = PlayerController.Instance.SidedSpeed;


        PlayerController.Instance.Speed = 0;
        PlayerController.Instance.SidedSpeed = 0;

        for (int i = 3; i > 0; i--)
        {
            _startCountdown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        _startCountdown.text = "";
        _timer.gameObject.GetComponent<CanvasGroup>().DOFade(1, .2f);
        IsRunning = true;

        PlayerController.Instance.Speed = _tempSpeed;
        PlayerController.Instance.SidedSpeed = _tempSidedSpeed;
    }

    private void Update()
    {
        if (IsRunning)
        {
            MapTime += Time.deltaTime;
            _timer.text = MapTime.ToString("N2") + "s";
        }

    }

    public void EndGame() => IsRunning= false;


}
