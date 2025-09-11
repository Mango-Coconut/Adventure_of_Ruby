using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    [Range(0f, 1f)] public float health = 0.5f;

    UIDocument uiDocument;
    VisualElement healthBar;

    void Awake()
    {
        instance = this;
        uiDocument = GetComponent<UIDocument>();
        healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f); 
    }
    
    public void SetHealthValue(float percentage)
    {
        healthBar.style.width = Length.Percent(100 * percentage);
    } 
}
