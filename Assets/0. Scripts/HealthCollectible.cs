using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController p = collision.GetComponent<PlayerController>();
            if (p != null && p.Health < p.maxHealth)
            {
                p.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
