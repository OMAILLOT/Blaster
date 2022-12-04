
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

        LoadGameData();

        UIManager.Instance.Init();
    }

    void LoadGameData()
    {
        foreach (WeaponButton weapon in UIManager.Instance.menuCanvas.equipementScreen.Armory.weapons)
        {
            if (weapon.weaponData._name == PlayerPrefs.GetString("WeaponName", "Blaster Alpha"))
            {
                PlayerData.Instance.PlayerWeapon = weapon.weaponData;
            }
        }
    }


  

    public void LoadScene(string sceneName) => StartScene(sceneName);

    void StartScene(string sceneName)
    {
        UIManager.Instance._loadingCanvas.DOFade(1, 0.2f).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
            UIManager.Instance.StartGame();
            PlayerController.Instance.Init();

            PlayerController.Instance.ActivatePlayer();
        });

        gameState = GameState.PLAY;

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