using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class Shield : MonoBehaviour
{
    
    private Coroutine _shieldColorTimerRoutine;
    private Color _originalColor = Color.white;
    private Color _damageColor = Color.red;
    private SpriteRenderer sr;
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {       
        
        if(other.tag == "Enemy" && gameObject.activeSelf)
        {
            if(_shieldColorTimerRoutine == null)
            {
                //sr.color = _originalColor;
                _shieldColorTimerRoutine = StartCoroutine(ShieldColorRoutine());
            }
            else if (_shieldColorTimerRoutine != null)
            {
                StopCoroutine(_shieldColorTimerRoutine);
                _shieldColorTimerRoutine = StartCoroutine(ShieldColorRoutine());
            }
        }
    }

    IEnumerator ShieldColorRoutine()
    {
        sr.color = _damageColor;
        yield return new WaitForSeconds(.2f);
        sr.color = _originalColor;
        _shieldColorTimerRoutine = null;
    }
}
