using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    float speed = 10f;
    float timer;
    float changeTime = 3f;
    public bool vertical;
    int direction = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rb.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rb.MovePosition(position);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}

