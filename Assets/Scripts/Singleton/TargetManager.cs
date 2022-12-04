using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoSingleton<TargetManager>
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private List<string> targetDescription;

    public string currentTargetDescription;

    public int numberOfTarget;

    int randomTarget;
    int nuberOfTargetHit;

    private Queue<string> targetDescriptionQueue = new Queue<string>();

    public void Start()
    {
        for (int i = 0; i < numberOfTarget; i++)
        {
            randomTarget = Random.Range(0, targets.Count);

            targets[randomTarget].SetActive(true);
            targetDescriptionQueue.Enqueue(targetDescription[randomTarget]);
            targets.RemoveAt(randomTarget);
        }
        UIManager.Instance.GameCanvas.FirstRefeshTargetCounter();

        currentTargetDescription = targetDescriptionQueue.Peek();
        targetDescriptionQueue.Dequeue();
    }

    public void TargetHit(GameObject target)
    {
        Target currentTarget = target.GetComponent<Target>();
        if (!currentTarget.isHit)
        {
            target.GetComponent<Rigidbody>().useGravity = true;
            currentTarget.isHit = true;
            nuberOfTargetHit++;
            UIManager.Instance.GameCanvas.RefreshTargetCounter(numberOfTarget - nuberOfTargetHit);
            if (nuberOfTargetHit == numberOfTarget)
            {
               GameManager.Instance.EndGame();
            }
            if (currentTargetDescription.Length > 0)
            {
                currentTargetDescription = targetDescriptionQueue.Peek();
                targetDescriptionQueue.Dequeue();
            }
        }
    }
}
