using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthIndex : MonoBehaviour
{
    [SerializeField] private Image _depthbarSprite;

    public void UpdateDepthBar(float maxHealth , float currentHealth)
    {
        _depthbarSprite.fillAmount = currentHealth / maxHealth;
    }
}
