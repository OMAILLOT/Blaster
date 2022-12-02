
using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { START, PLAY, END }

public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;


    void Awake()
    {
        DontDestroyOnLoad(transform.parent.gameObject);

        gameState = GameState.START;

        UIManager.Instance.Init();

        PlayerController.Instance.Init();
    }

    public void LoadScene(string sceneName) => StartScene(sceneName);

    void StartScene(string sceneName)
    {
        gameState = GameState.PLAY;

        UIManager.Instance._loadingCanvas.DOFade(1, 0.2f).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
            UIManager.Instance.StartGame();
        });
    }

    public void MenuScene()
    {
        gameState = GameState.START;

        UIManager.Instance._loadingCanvas.DOFade(1, 0.2f).OnComplete(() =>
        {
            SceneManager.LoadScene("Menu");
            UIManager.Instance.StartMenu();
        });
    }

    public void EndGame()
    {
        gameState = GameState.END;

        UIManager.Instance.HandleEnd();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}