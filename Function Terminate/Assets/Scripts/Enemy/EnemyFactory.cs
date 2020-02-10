using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{

    /// <summary>
    /// A faire
    /// </summary>

    // type of enemy : first digit level number , second enemy type
    [SerializeField] int enemyType = 0;
    // enemy hp
    [SerializeField] private int hp;
    // enemy damage 1/4 player's max health ?
    [SerializeField] private int damage;



    void Start()
    {
        if(enemyType == 11)
        {
            this.hp = 150;
            this.damage = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
