using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoArmory : MonoBehaviour
{
    public TMP_Text _title, _firerate, _ammo, _reloadTime;

    public void Init(WeaponData newWeapon)
    {
        _title.text = newWeapon._name;
        _firerate.text = "Firerate : " + newWeapon._fireRate + " bullet/s";
        _ammo.text = "Ammo : " + newWeapon._ammo + " bullet";
        _reloadTime.text = "Reload time : " + newWeapon._reloadTime + " s";
    }
}
