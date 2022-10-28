using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Armory : MonoBehaviour
{
    public List<WeaponButton> weapons;

    WeaponButton actualWeapon;

    public void Init()
    {
        foreach (WeaponButton weapon in weapons)
        {
            if (weapon.weaponData == PlayerData.Instance.PlayerWeapon)
            {
                SelectWeapon(weapon);
            }
        }
    }

    public void SelectWeapon(WeaponButton newWeapon)
    {
        if (actualWeapon != null)
        {
            actualWeapon.weaponName.DOColor(Color.black, .3f);
            actualWeapon.background.DOColor(Color.white, .3f);
        }

        actualWeapon = newWeapon;

        PlayerData.Instance.PlayerWeapon = actualWeapon.weaponData;

        actualWeapon.weaponName.DOColor(Color.white, .3f);
        actualWeapon.background.DOColor(Color.black, .3f);
    }
}
