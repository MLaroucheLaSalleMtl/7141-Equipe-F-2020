using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{

    [SerializeField] private float time = 2f;

    public void Destroy()
    {
        Destroy(this.gameObject, this.time);
    }

    public void Destroy(float _time)
    {
        Destroy(this.gameObject, _time);
    }
}
