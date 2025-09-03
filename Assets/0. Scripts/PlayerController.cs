using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] float moveSpeed = 5;
    public InputAction MoveAction;
    void Start()
    {
        MoveAction.Enable();
    }

    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        Vector2 position = (Vector2)transform.position + move * moveSpeed * Time.deltaTime;
        transform.position = position;
    }
}
