using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerScript : MonoBehaviour
{
    [SerializeField]BossBlueprint boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player" && !boss.IsActivated()) {
            boss.TargetPlayer(other.gameObject);
        }
    }
}
