using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] InputAction MoveAction;

    public int maxHealth = 5;
    [SerializeField] int currentHealth;
    public int Health { get { return currentHealth; } }

    Rigidbody2D rb;
    Vector2 move;
    void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        MoveAction.Enable();
    }

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + move * moveSpeed * Time.deltaTime;
        rb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth == 0)
        {
            //TODO 사망 처리
        }
    }
}
