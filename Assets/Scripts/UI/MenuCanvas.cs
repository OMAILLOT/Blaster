using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum MenuState { WAIT, MENU, EQUIPEMENT, SETTINGS }

public class MenuCanvas : MonoBehaviour
{

    public MenuState state;

    [SerializeField] RectTransform menu, equipement, settings;

    [SerializeField] EquipementScreen equipementScreen;
    [SerializeField] SettingsScreen settingScreen;

    public void Init()
    {
        state = MenuState.MENU;

        settingScreen.Init();

        equipementScreen.Armory.Init();
    }

    public void OpenEquipement()
    {
        if (state == MenuState.WAIT) return;

        state = MenuState.WAIT;

        menu.DOAnchorMin(new Vector2(0, 1), .3f);
        menu.DOAnchorMax(new Vector2(1, 2), .3f).OnComplete(() => state = MenuState.EQUIPEMENT);

        equipement.DOAnchorMin(new Vector2(0, 0), .3f);
        equipement.DOAnchorMax(new Vector2(1, 1), .3f).OnComplete(() => state = MenuState.EQUIPEMENT);
    }

    public void CloseEquipement()
    {
        if (state == MenuState.WAIT) return;

        state = MenuState.WAIT;

        menu.DOAnchorMin(new Vector2(0, 0), .3f);
        menu.DOAnchorMax(new Vector2(1, 1), .3f).OnComplete(() => state = MenuState.MENU);

        equipement.DOAnchorMin(new Vector2(0, -1), .3f);
        equipement.DOAnchorMax(new Vector2(1, 0), .3f).OnComplete(() => state = MenuState.MENU);
    }

    public void OpenSettings()
    {
        if (state == MenuState.WAIT) return;

        state = MenuState.WAIT;

        menu.DOAnchorMin(new Vector2(0, -1), .3f);
        menu.DOAnchorMax(new Vector2(1, 0), .3f).OnComplete(() => state = MenuState.SETTINGS);

        settings.DOAnchorMin(new Vector2(0, 0), .3f);
        settings.DOAnchorMax(new Vector2(1, 1), .3f).OnComplete(() => state = MenuState.SETTINGS);
    }

    public void CloseSettings()
    {
        if (state == MenuState.WAIT) return;

        state = MenuState.WAIT;

        menu.DOAnchorMin(new Vector2(0, 0), .3f);
        menu.DOAnchorMax(new Vector2(1, 1), .3f).OnComplete(() => state = MenuState.MENU);

        settings.DOAnchorMin(new Vector2(0, 1), .3f);
        settings.DOAnchorMax(new Vector2(1, 2), .3f).OnComplete(() => state = MenuState.MENU);
    }
}
