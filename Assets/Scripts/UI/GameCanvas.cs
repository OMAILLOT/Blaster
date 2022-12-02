using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text _startCountdown, _timer, _amountTarget, _ammo;
    public float MapTime;
    public bool IsRunning;

    public void Init()
    {
        _amountTarget.text = 20 + "/" + 20;
        RefreshAmmo();
    }

    public void RefreshAmmo()
    {
        _ammo.text = 20 + "/" + 20;
    }

    public IEnumerator StartCountdown()
    {
        for (int i = 5; i > 0; i--)
        {
            _startCountdown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        _startCountdown.text = "";
        _timer.gameObject.GetComponent<CanvasGroup>().DOFade(1, .2f);

        IsRunning = true;
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
