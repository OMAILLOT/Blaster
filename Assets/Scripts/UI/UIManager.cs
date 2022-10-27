using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    public MenuCanvas menuCanvas;
    public GameCanvas gameCanvas;
    public EndCanvas endCanvas;

    public CanvasGroup LoadingCanvas;

    [SerializeField] CanvasGroup _menuCanvasGroup, _gameCanvasGroup, _endCanvasGroup;

    CanvasGroup _actualCanvasGroup;

    public void Init()
    {
        menuCanvas.Init();

        _actualCanvasGroup = _menuCanvasGroup;
    }


    public void SwitchToCanvas(CanvasGroup toCanvas)
    {
        if (_actualCanvasGroup == toCanvas) return;

        if (LoadingCanvas.alpha != 0) LoadingCanvas.DOFade(0, 0.3f);

        if (toCanvas == _gameCanvasGroup) gameCanvas.Init();
        if (toCanvas == _menuCanvasGroup) menuCanvas.Init();
        if (toCanvas == _endCanvasGroup) endCanvas.Init();

        _actualCanvasGroup.interactable = false;
        _actualCanvasGroup.blocksRaycasts = false;
        _actualCanvasGroup.alpha = 0;

        _actualCanvasGroup = toCanvas;

        _actualCanvasGroup.interactable = true;
        _actualCanvasGroup.blocksRaycasts = true;
        _actualCanvasGroup.DOFade(1, 0.3f);

    }

    public void StartGame()
    {
        LoadingCanvas.DOFade(1, 0.2f).OnComplete(() =>
        {
            SwitchToCanvas(_gameCanvasGroup);
        });
    }
}
