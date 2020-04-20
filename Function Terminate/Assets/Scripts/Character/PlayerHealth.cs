using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameManager manager;

    [SerializeField] private float maxHealthPoint = 500;
    [SerializeField] private float currentHealthPoints = 475;
    [SerializeField] private float defence = 0;

    private AudioSource audio;
    [SerializeField] private AudioClip damageSound,healSound;
    public float MaxHealthPoint { get => maxHealthPoint; set => maxHealthPoint = value; }
    public float CurrentHealthPoints { get => currentHealthPoints; set => currentHealthPoints = value; }


    public void TakeDamage(float damage) {
        audio.PlayOneShot(damageSound);
        if (defence > 0)
        {
            defence -= damage;
            if (defence <= 0)
            {
                CurrentHealthPoints += defence;
            }
        }
        else
        {
            CurrentHealthPoints -= damage;
        }
       
        if (CurrentHealthPoints <=0) {
            Death();
        }
        //Debug.Log("defence = " + defence);
    }

    public void AddHealth(int healing) {
        audio.clip = healSound;
        audio.Play();
        if ((CurrentHealthPoints += healing) > MaxHealthPoint) {
            CurrentHealthPoints = MaxHealthPoint;
        }
    }

    public void AddDefence(int def)
    {
        audio.clip = healSound;
        audio.Play();
        defence += def;
    }

    private void Death()
    {
        manager.GameOver();
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
