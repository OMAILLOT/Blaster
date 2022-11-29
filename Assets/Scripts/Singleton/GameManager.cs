
using BaseTemplate.Behaviours;
using DG.Tweening;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public enum GameState { START, PLAY, END }

public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        gameState = GameState.START;

        UIManager.Instance.Init();
    }

    public void StartScene(string sceneName)
    {
        gameState = GameState.PLAY;

        SceneManager.LoadScene(sceneName);

        UIManager.Instance.StartGame();
    }

    public void EndGame()
    {
        gameState = GameState.END;
    }

    public void QuitApplication()
    {
        Application.Quit();

        UIGameManager.Instance.HandleEnd();

    }

    public void ReloadScene()
    {
        UIManager.Instance.LoadingCanvas.DOFade(1, 0.2f).OnComplete(() =>
        {
            DOTween.KillAll();
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        });
    }

    public void LoadScene(string sceneName)
    {
        UIManager.Instance.LoadingCanvas.DOFade(1, 0.2f).OnComplete(() =>
        {
            DOTween.KillAll();
            SceneManager.LoadScene(sceneName);
        });
    }
}