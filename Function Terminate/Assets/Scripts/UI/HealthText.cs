using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    [SerializeField]private PlayerHealth health;
    [SerializeField] private Text text;


    // Update is called once per frame
    void Update()
    {
        text.text = health.CurrentHealthPoints.ToString();
    }
}
