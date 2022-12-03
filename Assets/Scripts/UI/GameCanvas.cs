using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text _startCountdown, _timer, _amountTarget, numberOfTargetLeft, numberOfTargetMax, numberOfBulletLeft, numberOfBulletMax;
    public float MapTime;
    public bool canRunning;

    public void Init()
    {
        RefreshAmmo((int)PlayerData.Instance.PlayerWeapon._ammo);
        numberOfBulletMax.text = PlayerData.Instance.PlayerWeapon._ammo.ToString();
    }

    public void RefreshAmmo(int currentAmmo)
    {
        numberOfBulletLeft.text = currentAmmo.ToString();
    }

    public void FirstRefeshTargetCounter()
    {
        RefreshTargetCounter(TargetManager.Instance.numberOfTarget);
        numberOfTargetMax.text = TargetManager.Instance.numberOfTarget.ToString();
    }

    public void RefreshTargetCounter(int numberOfTargetHit)
    {
        numberOfTargetLeft.text = numberOfTargetHit.ToString();
    }

    public IEnumerator StartCountdown()
    {
        canRunning = false;
        PlayerController.Instance.canShoot = false;
        for (int i = 5; i > 0; i--)
        {
            _startCountdown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        _startCountdown.text = "";
        _timer.gameObject.GetComponent<CanvasGroup>().DOFade(1, .2f);

        canRunning = true;
        PlayerController.Instance.canShoot = true;
    }

    private void Update()
    {
        if (canRunning)
        {
            MapTime += Time.deltaTime;
            _timer.text = MapTime.ToString("N2") + "s";
        }

    }

    public void EndGame() => canRunning= false;


}
