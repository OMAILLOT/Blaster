using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Armory : MonoBehaviour
{
    public List<WeaponButton> weapons;

    [SerializeField] InfoArmory infoArmory;

    WeaponButton actualWeapon;

    public void Init()
    {
        foreach (WeaponButton weapon in weapons)
        {
            weapon.weaponName.text = weapon.weaponData._name;

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
            actualWeapon.weaponName.DOColor(ColorManager.Instance.DarkGrey, .3f);
            actualWeapon.background.DOColor(ColorManager.Instance.LightGrey, .3f);
        }

        actualWeapon = newWeapon;

        PlayerData.Instance.PlayerWeapon = actualWeapon.weaponData;

        actualWeapon.weaponName.DOColor(Color.white, .3f);
        actualWeapon.background.DOColor(ColorManager.Instance.DarkGrey, .3f);

        infoArmory.Init(actualWeapon.weaponData);

        PlayerPrefs.SetString("WeaponName", actualWeapon.weaponData._name);
    }

}
