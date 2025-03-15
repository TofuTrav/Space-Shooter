using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Laser : MonoBehaviour
{
    void Update()
    {
            transform.Translate(Vector3.up * 3 * Time.deltaTime);

            if(transform.position.y >= 8)
            {
                Destroy(gameObject);
            }
    }
}
