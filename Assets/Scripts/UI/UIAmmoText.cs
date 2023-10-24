using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAmmoText : MonoBehaviour
{
    TMP_Text _ammoText;

    void Start()
    {
        PlayerCandyLauncher.Instance.OnAmmoChanged -= UpdateText;
        PlayerCandyLauncher.Instance.OnAmmoChanged += UpdateText;
        _ammoText = GetComponent<TMP_Text>();
    }

    void UpdateText() => _ammoText.SetText($"x {PlayerCandyLauncher.Instance.CurrentAmmo}");

    void OnDisable() => PlayerCandyLauncher.Instance.OnAmmoChanged -= UpdateText;

}
