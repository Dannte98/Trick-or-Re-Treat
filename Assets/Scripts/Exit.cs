using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered Exit");
        if (collision.CompareTag("Player"))
            GameManager.Instance.Win();
    }
}
