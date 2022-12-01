using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    public MenuCanvas menuCanvas;
    public GameCanvas GameCanvas;
    public PauseCanvas PauseCanvas;
    public EndCanvas EndCanvas;
    public CanvasGroup _loadingCanvas;

    [SerializeField] CanvasGroup _menuCanvasGroup, _gameCanvasGroup, _pauseCanvasGroup, _endCanvasGroup;

    CanvasGroup _actualCanvasGroup;

    PlayerInput playerInput;

    public void Init()
    {
        menuCanvas.Init();

        _actualCanvasGroup = _menuCanvasGroup;

        //playerInput = new PlayerInput();

        //playerInput.Menu.Pause.started += TogglePause;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        //playerInput.Enable();

        _loadingCanvas.DOFade(1, 0.2f);

        _loadingCanvas.DOFade(0, .2f);

        SwitchToCanvas(_gameCanvasGroup);

        StartCoroutine(GameCanvas.StartCountdown());
    }

    public void StartMenu()
    {
        //playerInput.Disable();
        Destroy(GameManager.Instance.transform.parent.gameObject);

        _loadingCanvas.DOFade(1, 0.2f);

        _loadingCanvas.DOFade(0, .2f);

        SwitchToCanvas(_menuCanvasGroup);
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

    public void TogglePause()
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
