using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] InputAction MoveAction;
    public InputAction talkAction;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public int maxHealth = 5;
    [SerializeField] int currentHealth;
    public int Health { get { return currentHealth; } }

    Rigidbody2D rb;
    Vector2 move;

    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
    bool isstop = false;

    PlayerAudio playerAudio;

    public GameObject projectilePrefab;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<PlayerAudio>();
        currentHealth = maxHealth;
    }
    void Start()
    {
        MoveAction.Enable();
        talkAction.Enable();
    }

    void Update()
    {
        if (isstop)
        {
            move = new Vector2(0,0);
            return;
        }
        if (talkAction.triggered)
        {
            FindFriend();
        }

        move = MoveAction.ReadValue<Vector2>();
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Launch());
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

    void FindFriend()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            UIHandler.instance.DisplayDialogue();
        }
        
    }

    IEnumerator Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 1.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 25);
        animator.SetTrigger("Launch");
        isstop = true;
        yield return new WaitForSeconds(0.25f);
        isstop = false;
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
