using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    private int health = 100;
    public int damage = 10;
    void Start()
    {
        healthText.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
    }

    public void TakeDamage()
    {
        health -= damage;
    }
}
