﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameManager manager;

    [SerializeField] private float maxHealthPoint = 500;
    [SerializeField] private float currentHealthPoints = 475;

    public float MaxHealthPoint { get => maxHealthPoint; set => maxHealthPoint = value; }
    public float CurrentHealthPoints { get => currentHealthPoints; set => currentHealthPoints = value; }


    public void TakeDamage(float damage) {
        CurrentHealthPoints -= damage;
        if (CurrentHealthPoints <=0) {
            Death();
        }
    }

    public void AddHealth(int healing) {
        if ((CurrentHealthPoints += healing) > MaxHealthPoint) {
            CurrentHealthPoints = MaxHealthPoint;
        }
    }

    private void Death() {
        manager.GameOver();
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
