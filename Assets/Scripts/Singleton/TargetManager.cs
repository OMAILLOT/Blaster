using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoSingleton<TargetManager>
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private int numberOfTarget;

    int randomTarget;
    List<int> targetTakeByRandomTarget;
    public void Init()
    {
        for (int i = 0; i < targets.Count;)
        {
            randomTarget = Random.Range(0, targets.Count);

            targets[randomTarget].SetActive(true);

        }
    }
}
