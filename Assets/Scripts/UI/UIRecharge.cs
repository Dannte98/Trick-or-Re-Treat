using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecharge : MonoBehaviour
{
    [SerializeField] GameObject _objectHasRecharge;
    [SerializeField] Slider _slider;

    IRecharge _rechargeable;

    void Start() => _rechargeable = _objectHasRecharge.GetComponent<IRecharge>();
    void Update() => _slider.value = _rechargeable.RemainingTime / _rechargeable.UseTime;
}
