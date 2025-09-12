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
    float speed = 1f;
    float timer;
    float changeTime = 3f;
    public bool vertical;
    int direction = 1;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > changeTime)
        {
            timer = 0;
            direction = -direction;
        }

        Vector2 position = rb.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("X", direction);
            animator.SetFloat("Y", 0);
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

