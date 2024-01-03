using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public or private reference

    //data type

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = -1f;

    [SerializeField]
    private float _health = 30f;

    private SpawnManager _spawnManager;



    // Start is called before the first frame update
    void Start()
    {
        //take the current position and assign it a start position
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (!_spawnManager)
        {
            Debug.LogError("Spawn Manager Not Found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlayerMovement();
        //spawn game object

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        if (_health <= 0)
        {
            Debug.Log("Game Over");
            Destroy(this.gameObject);
        }
    }

    void CalculatePlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        //bounds
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), transform.position.z);

        if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
    }

    void FireLaser()
    {

        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), Quaternion.identity);

    }

    public void Damage(float dmg)
    {
        _health -= dmg;

        //check if dead
        if (_health <= 0)
        {
            //communicate with spawn manager
            if (_spawnManager)
            {
                _spawnManager.OnPlayerDeath();
            }

            //tell it to stop spawning
            Destroy(this.gameObject);
        }
    }

}
