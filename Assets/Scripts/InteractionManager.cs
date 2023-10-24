using System;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    public Interactable CurrentInteractable;

    public event Action<bool> InRange;
    public event Action<int> OnRewardsGiven;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CurrentInteractable)
        {
            CurrentInteractable.Interact();
        }
    }

    public void ShowHint(bool visible) => InRange?.Invoke(visible);
    public void ShowReward(int amount) => OnRewardsGiven?.Invoke(amount);
}
