using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXEnemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GetComponent<AudioSource>().Play();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GetComponent<AudioSource>().Stop();
    }
}
