using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Candy", menuName = "Candy")]
public class Candy : ScriptableObject
{
    public CandyReference Reference;
    public Sprite[] GeneralSprite;
    public Sprite SpecialSprite;
    public float DamageDone;
    public float HealthGiven;

    public float ThrowSpeed;
}

public enum CandyReference
{
    General,
    Damage,
    Survivals
}