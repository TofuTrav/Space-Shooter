using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _fireRate = .5f;
    private float _canFire = -1;
    [SerializeField]
    private int _lives = 3;

    [Header("Player Bounds")]
    [SerializeField]
    private float _topBounds = 5f;
    [SerializeField]
    private float _bottomBounds = -3.8f;
    [SerializeField]
    private float _leftBounds = 11.4f;
    [SerializeField]
    private float _rightBounds = -11.4f;
    
    [Header("Laser Stats")]
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private Transform _projectileManager;
    private SpawnManager _spawnManager;
   
   [SerializeField]
   private Transform _shield;
   [SerializeField]
   private bool _isShieldActive = false;
   private int _currentShieldHealth = 3;
   [SerializeField] 
   private int _maxShieldHealth = 3;
    

    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _shield.gameObject.SetActive(false);
        
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager Is NULL");
        }
    }
   
    void Update()
    {
        MovePlayer();
        SetBounds();
        FireLaser();

        if(_isShieldActive)
        {
            ActivateShield(true);
        }
    }

    public void Damage()
    {
        _lives --;
        
        if(_lives < 1)
        {
            _spawnManager.playerIsDead();
            Destroy(this.gameObject);
        }
    }
    
    private void MovePlayer()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);

        transform.Translate(direction * _speed * Time.deltaTime);
    }

    private void SetBounds()
    {
        if(transform.position.y >= _topBounds)
        {
            transform.position = new Vector3(transform.position.x, _topBounds, 0);
        }
        else if(transform.position.y <= _bottomBounds)
        {
            transform.position = new Vector3(transform.position.x, _bottomBounds, 0);
        }

        if(transform.position.x >= _rightBounds)
        {
            transform.position = new Vector3(_rightBounds, transform.position.y, 0);
        }
        else if(transform.position.x <= _leftBounds)
        {
            transform.position = new Vector3(_leftBounds, transform.position.y, 0);
        }
    }
    
    private void FireLaser()
    {   
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Laser.Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + .8f, 0), Quaternion.identity, _projectileManager);
        }
    }

    public void ActivateShield(bool isActive)
    {
        _shield.gameObject.SetActive(isActive);
        _isShieldActive = isActive;
        _currentShieldHealth = _maxShieldHealth;
    }
}