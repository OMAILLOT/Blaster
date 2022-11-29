using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIGameManager : MonoSingleton<UIGameManager>
{
    public GameCanvas GameCanvas;
    public PauseCanvas PauseCanvas;
    public EndCanvas EndCanvas;

    [SerializeField] CanvasGroup _loadingCanvas, _gameCanvasGroup, _pauseCanvasGroup, _endCanvasGroup;

    CanvasGroup _actualCanvasGroup;

    PlayerInput playerInput;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.Menu.Pause.started += TogglePause;

        _loadingCanvas.DOFade(0, .2f);

        GameCanvas.Init();

        StartCoroutine(GameCanvas.StartCountdown());


        _actualCanvasGroup = _gameCanvasGroup;
    }

    public void SwitchToCanvas(CanvasGroup toCanvas, bool instant = false, bool hidePreviousCG = true)
    {
        if (_actualCanvasGroup == toCanvas) return;

        if (toCanvas == _gameCanvasGroup) GameCanvas.Init();
        if (toCanvas == _pauseCanvasGroup) PauseCanvas.Init();
        if (toCanvas == _endCanvasGroup) EndCanvas.Init();

        _actualCanvasGroup.interactable = false;
        _actualCanvasGroup.blocksRaycasts = false;

        if (hidePreviousCG)
        {
            _actualCanvasGroup.alpha = 0;
        }

        _actualCanvasGroup = toCanvas;

        _actualCanvasGroup.interactable = true;
        _actualCanvasGroup.blocksRaycasts = true;
        if (instant) _actualCanvasGroup.alpha = 1;
        else _actualCanvasGroup.DOFade(1, 0.3f);

    }

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (_actualCanvasGroup != _pauseCanvasGroup) HandleEnterPause();
        else if (_actualCanvasGroup == _pauseCanvasGroup) HandleExitPause();
    }

    public void HandleEnd() => SwitchToCanvas(_endCanvasGroup);

    public void HandleEnterPause()
    {
        GameCanvas.IsRunning = false;

        Cursor.lockState = CursorLockMode.None;

        SwitchToCanvas(_pauseCanvasGroup, true, false);
    }
    public void HandleExitPause()
    {
        GameCanvas.IsRunning = true;

        Cursor.lockState = CursorLockMode.Locked;

        SwitchToCanvas(_gameCanvasGroup);
    }

}
