using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{   
    [SerializeField] float moveSpeed = 5;
    public InputAction LeftAction;
    public InputAction RightAction;
    void Start()
    {
        LeftAction.Enable();
    }

    void Update()
    {
        float h = 0;
        float v = 0;
        if (LeftAction.IsPressed())
        {
            h = -1;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            h = 1;
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            v = 1;
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            v = -1;
        }
        Vector3 position = transform.position;
        Vector3 newv = new Vector3(h, v, 0).normalized * moveSpeed * Time.deltaTime;
        transform.position += newv;
    }
}
