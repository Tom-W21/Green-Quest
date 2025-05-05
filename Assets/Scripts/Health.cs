using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health; // Vida atual
    public int heartsCount; // Quantidade máxima de corações

    public Image[] hearts;
    public Sprite fullHeart;

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
            }
            else if (i < heartsCount)
            {
                hearts[i].enabled = false; // Some com o coração
            }
            else
            {
                hearts[i].enabled = false; // Fora do limite de corações
            }
        }
    }
}

