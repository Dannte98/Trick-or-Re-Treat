using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMHelper : MonoBehaviour
{
    public void Restart() => GameManager.Instance.Restart();
    public void Win() => GameManager.Instance.Win();
    public void Lose() => GameManager.Instance.Lose();
}
