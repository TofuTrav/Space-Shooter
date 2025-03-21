using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {       
        if(other.tag == "Enemy")
        {
            StartCoroutine(ShieldColorRoutine());
        }
    }

    IEnumerator ShieldColorRoutine()
    {
        Debug.Log("The enemy has hit my shield!");
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color _originalColor;
            Color _damageColor;
            _originalColor = sr.color;
            _damageColor = Color.red;

            sr.color = _damageColor;
            yield return new WaitForSeconds(.2f);
            sr.color = _originalColor;
    }
}
