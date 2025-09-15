using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] InputAction MoveAction;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public int maxHealth = 5;
    [SerializeField] int currentHealth;
    public int Health { get { return currentHealth; } }

    Rigidbody2D rb;
    Vector2 move;

    Animator animator;
    Vector2 moveDirection = new Vector2(1,0);
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    void Start()
    {
        MoveAction.Enable();
    }

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + move * moveSpeed * Time.deltaTime;
        rb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
        if (currentHealth == 0)
        {
            //TODO 사망 처리
        }
    }
}
