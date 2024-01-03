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

    void Start()
    {
         StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //spawn game objects every 5 seconds
    //create a coroutine of type IEnumberator -- yield events

    IEnumerator SpawnRoutine() {

        while (!_stopSpawning) {
            
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(7.0f, 9.0f), 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_waitTime);
        }
    }

    public void OnPlayerDeath() {
        _stopSpawning = !_stopSpawning;
    }
}
