using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{

    [SerializeField] private BossBlueprint boss;

    public void SendDamage(float Damage) {
        boss.TakeDamage(Damage);
    }
}
