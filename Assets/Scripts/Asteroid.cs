using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 30f;
    // Start is called before the first frame update

    [SerializeField]
    private GameObject _explosionPrefab;

    private float _xMovement;
    private float _yMovement;

    [SerializeField]
    private SpawnManager _spawnManager;



    void Start()
    {
        if (!_explosionPrefab)
        {
            Debug.LogError("No Explosion Prefab");
        }
        //transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(7.0f, 9.0f), 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (!_spawnManager)
        {
            Debug.LogError("No spawn Manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), _rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If other is player

        if (other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();
            if (player)
            {
                player.Damage(10);
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        // If other is laser
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject); // Destroy the laser
            //add 10 to score
            Player _player = GameObject.Find("Player").GetComponent<Player>();
            if (_player)
            {
                _player.AddToScore(20);
            }



            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();

            Destroy(gameObject); // Destroy the enemy or object this script is attached to
        }

        //Instantiate(_enemyPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(7.0f, 9.0f), 0), Quaternion.identity);


    }
}
