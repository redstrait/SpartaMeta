using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    //[Range(1f, 20f)][SerializeField] private float speed = 3;
    [Range(1f, 20f)] public float speed = 5;

    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }
}
