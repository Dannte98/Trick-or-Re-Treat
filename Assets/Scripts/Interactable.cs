using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Interactable : MonoBehaviour
{
    [Header("Rewards")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float _probability;
    [SerializeField] int _minAmountGiven;
    [SerializeField] int _maxAmountGiven;

    [Header("Resetting")]
    [SerializeField] float _minResetTime;
    [SerializeField] float _maxResetTime;
    [SerializeField] GameObject[] _resetables;

    bool _canInteract = true;
    float _resetTime;

    public bool CanInteract => _canInteract;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _canInteract)
        {
            if(InteractionManager.Instance.CurrentInteractable == null)
                InteractionManager.Instance.CurrentInteractable = this;
            InteractionManager.Instance.ShowHint(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (InteractionManager.Instance.CurrentInteractable == this)
                InteractionManager.Instance.CurrentInteractable = null;
            InteractionManager.Instance.ShowHint(false);
        }
    }

    public void Interact()
    {
        if (_canInteract == false)
            return;
        float chance = UnityEngine.Random.Range(0.0f, 1.0f);
        int amount = UnityEngine.Random.Range(_minAmountGiven, _maxAmountGiven + 1);
        int reward;
        //Chance to give candy
        if (chance <= (1.0f - _probability))
            reward = 0;
        else
            reward = amount;
        PlayerCandyLauncher.Instance.AddAmmo(reward);
        InteractionManager.Instance.ShowReward(reward);
        _canInteract = false;
        StartCoroutine(ResetInteraction());
    }

    IEnumerator ResetInteraction()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject resetable in _resetables)
            resetable.SetActive(false);
        _resetTime = UnityEngine.Random.Range(_minResetTime, _maxResetTime);
        yield return new WaitForSeconds(_resetTime);
        foreach (GameObject resetable in _resetables)
            resetable.SetActive(true);
        _canInteract = true;
    }
}
