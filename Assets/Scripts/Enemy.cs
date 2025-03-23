using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 4;
    [SerializeField]
    private float _bottomBounds = -5.6f;
    [SerializeField]
    private float _topBounds = 7.3f;
    [SerializeField]
    private Vector2 _spawnRange;
    //[SerializeField]
    //private Transform _player;
    private Player _player;

    [Header("Enemies")]
    [SerializeField]
    private EnemyID _enemyID;
    [SerializeField]
    private int _eyeballPointValue = 10;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();  
    }
    
    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        
        float randomX = Random.Range(_spawnRange.x,_spawnRange.y);
      
        if(transform.position.y <= _bottomBounds)
        {
            transform.position = new Vector3(randomX,_topBounds,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {   
            if(_player != null)
            {
                _player.Damage();
            }
            
            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {   
            if(_player == null)
            {
                Debug.Log("i don't have the script!");
                return;
            }
            
            Destroy(other.gameObject);

            switch(_enemyID)
            {
                case EnemyID.Eyeball:
                _player.addPoints(_eyeballPointValue);
                Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
