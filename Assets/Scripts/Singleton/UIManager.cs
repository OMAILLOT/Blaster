using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    public MenuCanvas menuCanvas;

    public CanvasGroup LoadingCanvas;

    [SerializeField] CanvasGroup _menuCanvasGroup;

    public void Init()
    {
        menuCanvas.Init();
    }

    public void StartGame()
    {
        LoadingCanvas.DOFade(1, 0.2f);
    }
}
