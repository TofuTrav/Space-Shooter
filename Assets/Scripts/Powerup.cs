using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private float _bottomBounds = -4.9f;
    [SerializeField]
    private PowerupType _powerupID;

    // Update is called once per frame
    void Update()
    {
        PowerupMovement();
    }

    private void PowerupMovement()
    {
        Vector3 _moveDirection = Vector3.down;
        
        transform.Translate(_moveDirection * (_speed * Time.deltaTime));
        
        if(transform.position.y < _bottomBounds)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
           switch(_powerupID)
           {
                case PowerupType.None:
                    Debug.Log("No powerup type was assigned to this powerup!");
                    break;
                case PowerupType.TripleShot:
                    player.ActivateTripleShot();
                    break;
                case PowerupType.SpeedBooster:
                    player.ActivateSpeedBooster();
                    break;
                case PowerupType.Shield:
                    player.ActivateShield(true);
                    break;
                default:
                    break;
           }

            Destroy(this.gameObject);
        }
    }
}
