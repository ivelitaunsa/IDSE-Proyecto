using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAereo : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 100f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time*speed, distance);
        transform.position = startPosition + new Vector3(0, movement, 0);
    }
}
