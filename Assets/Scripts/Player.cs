using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpawnManager _spawnManager;
    
    [Header("Player Stats")]
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _fireRate = .5f;
    private float _canFire = -1;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _speedBoostMultiplier = 2;

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
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private Transform _projectileContainer;
   
   [Header("Shield Stats")]
   [SerializeField]
   private Transform _shield;
   [SerializeField]
   private bool _isShieldActive = false;
   private int _currentShieldHealth = 3;
   [SerializeField] 
   private int _maxShieldHealth = 3;

   private bool _isTripleShotActive = false;
   private Coroutine _tripleShotTimerRoutine;
   private bool _isSpeedBoosterActive = false;
   private Coroutine _speedBoosterTimerRoutine;

    

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
        SetBounds();
        MovePlayer();
        
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
        
        if(_isShieldActive)
        {
            ActivateShield(true);
        }
         
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
            transform.position = new Vector3(_leftBounds, transform.position.y, 0);
        }
        else if(transform.position.x <= _leftBounds)
        {
            transform.position = new Vector3(_rightBounds, transform.position.y, 0);
        }
    }
    
    private void MovePlayer()
    {
        float _horizontalMove = Input.GetAxis("Horizontal");
        float _verticalMove = Input.GetAxis("Vertical");
        Vector3 _moveDirection = new Vector3(_horizontalMove,_verticalMove,0);

        if(_isSpeedBoosterActive)
        {
            transform.Translate(_moveDirection * ((_speed * _speedBoostMultiplier) * Time.deltaTime));
        }
        else
        {
            transform.Translate(_moveDirection * (_speed * Time.deltaTime));
        }
    }

    private void FireLaser()
    {   
        if(_isTripleShotActive)
        {
            _canFire = Time.time + _fireRate;
            Laser.Instantiate(_tripleShotPrefab, new Vector3(transform.position.x, transform.position.y + .9f, 0), Quaternion.identity, _projectileContainer);
        }
        else if(_isTripleShotActive == false)
        {
            _canFire = Time.time + _fireRate;
            Laser.Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.3f, 0), Quaternion.identity, _projectileContainer);
        }
    }

    public void ActivateShield(bool isActive)
    {
        _shield.gameObject.SetActive(isActive);
        _isShieldActive = isActive;
        _currentShieldHealth = _maxShieldHealth;
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

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;

        if(_tripleShotTimerRoutine == null)
        {
            _tripleShotTimerRoutine = StartCoroutine(TripleShotPowerDownRoutine());
        }
        else
        {
            Debug.Log("I collected a tripleshot while tripleshot was active");
            StopCoroutine(_tripleShotTimerRoutine);
            _tripleShotTimerRoutine = StartCoroutine(TripleShotPowerDownRoutine());
        }
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
        _tripleShotTimerRoutine = null;
    }

    public void ActivateSpeedBooster()
    {
        _isSpeedBoosterActive = true;
        
        if(_speedBoosterTimerRoutine == null)
        {
            _speedBoosterTimerRoutine = StartCoroutine(SpeedBoosterPowerDownRoutine());
        }
        else if(_speedBoosterTimerRoutine != null)
        {
            StopCoroutine(_speedBoosterTimerRoutine);
            _speedBoosterTimerRoutine = StartCoroutine(SpeedBoosterPowerDownRoutine());
        }
        
        
        //if speed is already boosted
        //stop that boost
        StartCoroutine(SpeedBoosterPowerDownRoutine());
    }

    IEnumerator SpeedBoosterPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedBoosterActive = false;
        _speedBoosterTimerRoutine = null;
    }
}