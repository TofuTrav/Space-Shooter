using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform _enemyManager;

    [Header("Enemies")]
    [SerializeField]
    private Transform _enemyEyeballPrefab;
    [SerializeField]
    private Vector2 _spawnRange;


     private bool _stopSpawning = false;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    public void playerIsDead()
    {
        _stopSpawning = true;
        StartCoroutine(DestroyAllEnemiesRoutine());
    }
    IEnumerator SpawnEnemy()
    {
        //While Loop
        while(_stopSpawning == false)
        {
            float randomX = Random.Range(_spawnRange.x,_spawnRange.y);
            //instantiate enemy eyeball prefab
            Instantiate(_enemyEyeballPrefab,new Vector3(randomX,7.3f,0),Quaternion.identity,_enemyManager);
            //yield new wait for 5 sec
            yield return new WaitForSeconds(2f);
        }    
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
