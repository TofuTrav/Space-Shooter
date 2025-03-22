using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField]
    private Vector2 _enemySpawnXRange;
    [SerializeField]
    private float _enemySpawnY = 7.3f;
    [SerializeField]
    private Transform _enemyEyeballPrefab;
    [SerializeField]
    private Transform _enemyContainer;

    [Header("Powerups")]
    [SerializeField]
    private Vector2 _powerupSpawnXRange;
    [SerializeField]
    private float _powerupSpawnY = 7.3f;
    [SerializeField]
    private Vector2 _powerupSpawnTimer;
    [SerializeField]
    private Transform _tripleShotPrefab;
    [SerializeField]
    private Transform _powerupContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerupPrefabs;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerupRoutine());
    }
    IEnumerator SpawnEnemy()
    {
        while(_stopSpawning == false)
        {
            float _enemySpawnX = Random.Range(_enemySpawnXRange.x,_enemySpawnXRange.y);

            Instantiate(_enemyEyeballPrefab,new Vector3(_enemySpawnX,_enemySpawnY,0),Quaternion.identity,_enemyContainer);
            yield return new WaitForSeconds(2f);
        }    
    }
    IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawning == false)
        {
            float _powerupSpawnX = Random.Range(_powerupSpawnXRange.x,_powerupSpawnXRange.y);
            float _timeBetweenSpawn = Random.Range(_powerupSpawnTimer.x,_powerupSpawnTimer.y);
            int randomPowerup = Random.Range(0,_powerupPrefabs.Length);
            
            Debug.Log("Stop Spawning is false!");
            Instantiate(_powerupPrefabs[randomPowerup],new Vector3(_powerupSpawnX,_powerupSpawnY,0),Quaternion.identity,_powerupContainer);
            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
    }
    public void playerIsDead()
    {
        _stopSpawning = true;
        StartCoroutine(DestroyAllEnemiesRoutine());
    }
    IEnumerator DestroyAllEnemiesRoutine()
    {
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");

        yield return new WaitForSeconds(2f);
        foreach (GameObject _enemy in _enemies)
        {
            Destroy(_enemy);
        }
    }
}
