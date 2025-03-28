﻿using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    
    void Update()
    {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));

            if(transform.position.y >= 8)
            {
                Destroy(gameObject);
            }
    }
}
