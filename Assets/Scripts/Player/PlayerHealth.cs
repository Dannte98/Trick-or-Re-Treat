using System;
using UnityEngine;

public class PlayerHealth : HasHealth
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerCandyLauncher.Instance.CurrentAmmo >= 1)
                PlayerCandyLauncher.Instance.AddAmmo(-1);
            else
                return;
            bool healed = Heal(PlayerCandyLauncher.Instance.GetHealValue());
        }
    }
}
