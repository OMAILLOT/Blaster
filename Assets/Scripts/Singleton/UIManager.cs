using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Space(10)]
    [Header("TextGameView")]

    CanvasGroup _actualCanvasGroup;

    public PlayerInput playerInput;

    public void Init()
    {
        menuCanvas.Init();

        _actualCanvasGroup = _menuCanvasGroup;

        playerInput = new PlayerInput();
        playerInput.Enable();


        playerInput.Menu.Pause.started += TogglePause;
    }

    private void OnDestroy()
    {
        playerInput.Menu.Pause.started -= TogglePause;
    }

    public void StartGame()
    {
        _menuCanvasGroup.gameObject.SetActive(false);

        _loadingCanvas.DOFade(1, 0.2f);

        _loadingCanvas.DOFade(0, .2f);

        SwitchToCanvas(_gameCanvasGroup);

        StartCoroutine(GameCanvas.StartCountdown());

    }

    public void StartMenu()
    {
        _menuCanvasGroup.gameObject.SetActive(true);

        playerInput.Disable();
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

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.gameState == GameState.START) return;
        if (_actualCanvasGroup != _pauseCanvasGroup) HandleEnterPause();
        else if (_actualCanvasGroup == _pauseCanvasGroup) HandleExitPause();
    }

    public void HandleEnd() => SwitchToCanvas(_endCanvasGroup);

    public void HandleEnterPause()
    {
        PlayerController.Instance.DesactivePlayer();

        GameCanvas.canRunning = false;

        Cursor.lockState = CursorLockMode.None;

        SwitchToCanvas(_pauseCanvasGroup, true, false);
    }
    public void HandleExitPause()
    {
        PlayerController.Instance.ActivatePlayer();

        GameCanvas.canRunning = true;

        Cursor.lockState = CursorLockMode.Locked;

        SwitchToCanvas(_gameCanvasGroup);
    }

}
