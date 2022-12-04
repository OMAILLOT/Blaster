using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoSingleton<TargetManager>
{
    [SerializeField] private List<GameObject> targets;
    public int numberOfTarget;
    public int nuberOfTargetHit;

    int randomTarget;
    public void Start()
    {
        for (int i = 0; i < numberOfTarget; i++)
        {
            randomTarget = Random.Range(0, targets.Count);

            targets[randomTarget].SetActive(true);
            targets.RemoveAt(randomTarget);
        }
        UIManager.Instance.GameCanvas.RefreshTargetCounter();
    }

    public void TargetHit(GameObject target)
    {
        Target currentTarget = target.GetComponent<Target>();
        if (!currentTarget.isHit)
        {
            target.GetComponent<Rigidbody>().useGravity = true;
            currentTarget.isHit = true;
            nuberOfTargetHit++;
            UIManager.Instance.GameCanvas.RefreshTargetCounter();
            if (nuberOfTargetHit == numberOfTarget)
            {
               GameManager.Instance.EndGame();
            }
        }
    }
}
