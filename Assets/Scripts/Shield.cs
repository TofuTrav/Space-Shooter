using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {       
        if(other.tag == "Enemy" && gameObject.activeSelf)
        {
            StartCoroutine(ShieldColorRoutine());
        }
    }

    IEnumerator ShieldColorRoutine()
    {
        
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
