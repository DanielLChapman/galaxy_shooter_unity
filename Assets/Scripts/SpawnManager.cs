using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _waitTime = 5f;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject[] powerups;

    [SerializeField]
    private float _powerupTimeMin = 3f;
    [SerializeField]
    private float _powerupTimeMax = 7f;



    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator SpawnPowerupRoutine() {
        while (!_stopSpawning) {
             yield return new WaitForSeconds(Random.Range(_powerupTimeMin, _powerupTimeMax+1));
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 2);
             GameObject newSprite = Instantiate(powerups[randomPowerUp],postToSpawn, Quaternion.identity);
        }
    }


    IEnumerator SpawnEnemyRoutine() {

        while (!_stopSpawning) {
            
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(7.0f, 9.0f), 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_waitTime);
            _waitTime -= 0.1f;
            if (_waitTime <= 1f) {
                _waitTime = 1f;
            } 
        }
    }

    public void OnPlayerDeath() {
        _stopSpawning = !_stopSpawning;
    }
}
