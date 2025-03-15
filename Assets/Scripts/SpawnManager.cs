using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform _enemyManager;

    [Header("Enemies")]
    [SerializeField]
    private Transform _enemyEyeballPrefab;

     private bool _stopSpawning;
    void Start()
    {
        _stopSpawning = false;
        StartCoroutine(SpawnEnemy());
    }

    
    // Update is called once per frame
    void Update()
    {
                   
    }
    public void playerIsDead()
    {
        _stopSpawning = true;
    }
    IEnumerator SpawnEnemy()
    {
        //While Loop
        while(_stopSpawning == false)
        {
            float randomX = Random.Range(-9.5f,9.5f);
            //instantiate enemy eyeball prefab
            Instantiate(_enemyEyeballPrefab,new Vector3(randomX,7.5f,0),Quaternion.identity,_enemyManager);
            //yield new wait for 5 sec
            yield return new WaitForSeconds(2f);
        }    
    }
}
