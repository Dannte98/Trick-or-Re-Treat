using UnityEngine;
using TMPro;

public class InteractionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _hintText;
    [SerializeField] TMP_Text _rewardText;
    [SerializeField] Animator _rewardAnimator;

    void Start()
    {
        _hintText.enabled = false;
        InteractionManager.Instance.InRange += ShowHint;
        InteractionManager.Instance.OnRewardsGiven += ShowReward;
    }

    void OnDisable()
    {
        InteractionManager.Instance.InRange -= ShowHint;
        InteractionManager.Instance.OnRewardsGiven -= ShowReward;
    }

    void ShowHint(bool show)
    {
        _hintText.enabled = show;
    }

    void ShowReward(int amountGiven)
    {
        _rewardText.SetText($"+ {amountGiven}");
        _rewardAnimator.SetTrigger("Reward");
    }
}
