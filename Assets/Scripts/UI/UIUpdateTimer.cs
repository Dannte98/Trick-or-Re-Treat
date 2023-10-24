using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpdateTimer : MonoBehaviour
{
    [SerializeField] TMP_Text _timeText;

    void Update()
    {
        _timeText.SetText($"{GameManager.Instance.CycleGameHour} : {GameManager.Instance.CycleGameMin}");
    }
}
