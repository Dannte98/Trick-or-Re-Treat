using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    [SerializeField] float _timeToDestroy = 1.0f;
    void Start() => Destroy(gameObject, _timeToDestroy);
}
