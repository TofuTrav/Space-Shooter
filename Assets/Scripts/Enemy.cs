using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        transform.Translate(Vector3.down * 4 * Time.deltaTime);

        //if at bottom of screen respawn at top of screen in a random x position
        if(transform.position.y <= -5.6f)
        {
            transform.position = new Vector3(Random.Range(-8.3f,8.3f),7.3f,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("The Enemy Ran Into Something!");
        
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if(player != null)
            {
                player.Damage();
            }
            
            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
