
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
        gameState = GameState.START;

        UIManager.Instance.Init();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))// A modif avec le new input system
        {
            ReloadScene();
        }
    }
    public void StartScene(Scene newScene)
    {
        gameState = GameState.PLAY;

        SceneManager.LoadScene(newScene.name);

        UIManager.Instance.StartGame();
    }

    public void EndGame()
    {
        gameState = GameState.PLAY;

    }

    public void QuitApplication()
    {
        Application.Quit();
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