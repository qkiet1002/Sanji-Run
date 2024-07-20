using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPFollowerQuai : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    public float offsetY = 1f; // Gi� tr? offset Y ?? ??t thanh m�u cao h?n

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low,High,slider.normalizedValue);
    }

    void Update()
    {
       slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position +Offset);
    }

 


}
