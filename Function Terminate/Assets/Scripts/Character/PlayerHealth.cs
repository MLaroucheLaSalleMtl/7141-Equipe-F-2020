using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int maxHealthPoint = 500;
    [SerializeField] private int currentHealthPoints = 475;

    public int MaxHealthPoint { get => maxHealthPoint; set => maxHealthPoint = value; }
    public int CurrentHealthPoints { get => currentHealthPoints; set => currentHealthPoints = value; }


    public void TakeDamage(int damage) {
        CurrentHealthPoints -= damage;
    }

    public void AddHealth(int healing) {
        if ((CurrentHealthPoints += healing) > MaxHealthPoint) {
            CurrentHealthPoints = MaxHealthPoint;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
