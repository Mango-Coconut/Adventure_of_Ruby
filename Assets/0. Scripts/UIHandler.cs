using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    [Range(0f, 1f)]public float health = 0.5f;

    UIDocument uiDocument;
    VisualElement healthBar;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
    }
    void Update()
    {
        healthBar.style.width = Length.Percent(health * 100.0f);
    }
}
