using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform _enemyManager;

    [Header("Enemies")]
    [SerializeField]
    private Transform _enemyEyeballPrefab;

     private bool isRunning;
    void Start()
    {
        isRunning = true;
        StartCoroutine(SpawnEnemy());
    }

    
    // Update is called once per frame
    void Update()
    {
                   
    }

    IEnumerator SpawnEnemy()
    {
        //While Loop
        while(isRunning)
        {
            float randomX = Random.Range(-9.5f,9.5f);
            //instantiate enemy eyeball prefab
            Instantiate(_enemyEyeballPrefab,new Vector3(randomX,7.5f,0),Quaternion.identity,_enemyManager);
            //yield new wait for 5 sec
            yield return new WaitForSeconds(2f);
        }

    
    
    }
}
