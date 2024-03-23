using UnityEngine;

public class ChallengeObject : InteractableObject
{
    public bool challengeComplete = false;
    public GameObject rewardItem;
    public string keyTag;

    protected override void Start()
    {
        base.Start();

        if(rewardItem != null)
        {
            rewardItem.tag = keyTag;
        }

        if (logic.isChallengeComplete(gameObject.name))
        {
            CompleteChallenge();
        }
        
    }

    public override bool GetChallengeCompletionStatus()
    {
        return challengeComplete;
    }

    public virtual void CompleteChallenge()
    {
        if(rewardItem != null && !logic.isChallengeComplete(gameObject.name))
        {
            inventory.AddToInventory(rewardItem);
        }

        challengeComplete = true;

        logic.AddCompletedChallenge(gameObject.name);
        Debug.Log(gameObject.name);
    }
}