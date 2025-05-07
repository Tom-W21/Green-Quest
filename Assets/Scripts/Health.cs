using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health; // Vida atual
    public int heartsCount; // Quantidade m�xima de cora��es

    public Image[] hearts;
    public Sprite fullHeart;

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null) continue; // pula se o cora��o foi destru�do

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
            }
            else if (i < heartsCount)
            {
                hearts[i].enabled = false;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}

