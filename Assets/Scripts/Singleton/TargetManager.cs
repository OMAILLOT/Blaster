using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : MonoSingleton<TargetManager>
{
    [SerializeField] private List<Target> targets;

    public string currentTargetDescription;

    public int numberOfTarget;
    public int nuberOfTargetHit;

    int randomTarget;
    private Dictionary<int,string> targetDescriptionDictionnary = new Dictionary<int, string>();

    public void Start()
    {
        for (int i = 0; i < numberOfTarget; i++)
        {
            randomTarget = Random.Range(0, targets.Count);

            targets[randomTarget].gameObject.SetActive(true);
            targetDescriptionDictionnary.Add(targets[randomTarget].ID, targets[randomTarget].Desc);

            targets.RemoveAt(randomTarget);
        }
        UIManager.Instance.GameCanvas.RefreshTargetCounter();

        currentTargetDescription = targetDescriptionDictionnary.First().Value;
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

            targetDescriptionDictionnary.Remove(currentTarget.ID);

            if (targetDescriptionDictionnary.Count > 0) currentTargetDescription = targetDescriptionDictionnary.First().Value;

        }
    }
}
