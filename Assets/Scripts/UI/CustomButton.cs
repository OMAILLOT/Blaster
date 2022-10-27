using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    [SerializeField] float timeOfEffects = 0.2f;
    [SerializeField] RectTransform rt;
    [SerializeField] Animation _afterClickAnim;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        rt.DOScale(new Vector3(.9f, .9f, .9f), timeOfEffects).SetEase(Ease.OutExpo);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        rt.DOScale(Vector3.one, timeOfEffects).SetEase(Ease.OutExpo);

       // _afterClickAnim.Play(); // Open gif in fullscreen
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        //Change cursor
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        //Change cursor
    }
}
