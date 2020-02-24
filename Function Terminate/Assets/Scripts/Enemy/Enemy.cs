using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public abstract class Enemy : MonoBehaviour
{

    // enemy hp
    private int hp = 150;
    // enemy damage 1/4 player's max health ?
    private int damage = 100;

    public int Hp { get => hp; set => hp = value; }
    public int Damage { get => damage; set => damage = value; }


    // enemy take damage function
    public void TakeDamage(int damage)
    {
        this.Hp -= damage;
    }

    // check if the enemy is dead
    public bool IsDead()
    {
        return (this.hp <= 0);
    }

}