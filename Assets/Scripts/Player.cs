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
    private float _currentSpeed = 5f;

    [SerializeField]
    private bool isTripleFireActive = false;

    [SerializeField]
    private bool isSpeedActive = false;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = -1f;

    [SerializeField]
    private float _health = 30f;

    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject shieldPrefab;
    private GameObject spawnedShieldPrefab;

    [SerializeField]
    private int _score = 0;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private AudioSource _fireLaserSound;
    



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

        if (!uiManager) {
            Debug.LogError("UI manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlayerMovement();
        //spawn game object

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        if (_health <= 0)
        {
            Debug.Log("Game Over");
            uiManager.UpdateScoreText(0);
            uiManager.GameOverText();
            Destroy(this.gameObject);
        }
    }

    void CalculatePlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _currentSpeed * Time.deltaTime);

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

        Vector3 position = transform.position;
        float newFireRate = _fireRate;
        if (isSpeedActive)
        {
            newFireRate /= 2;
        }

        if (isTripleFireActive)
        {
            _canFire = Time.time + newFireRate * 1.3f;
            // Adjust the x position by adding half of the object's width
            Vector3 tripleShotPosition = transform.position + new Vector3(0, 0, 0);

            Instantiate(_tripleShotPrefab, tripleShotPosition, Quaternion.identity);
        }
        else
        {
            _canFire = Time.time + newFireRate;

            Instantiate(_laserPrefab, new Vector3(position.x, position.y + 1.08f, position.z), Quaternion.identity);
        }

        //play audio
        _fireLaserSound.Play();
    }




    public void Damage(float dmg)
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            Destroy(spawnedShieldPrefab);
        }
        else
        {
            
            _health -= dmg;
            uiManager.UpdateLives((int)_health/10);

            if (_health <= 20) {
                transform.Find("Right_Engine").gameObject.SetActive(true);
            } 
            if (_health <= 10) {
                transform.Find("Left_Engine").gameObject.SetActive(true);
            }

            // Check if dead
            if (_health <= 0)
            {
                // Communicate with spawn manager
                if (_spawnManager)
                {
                    _spawnManager.OnPlayerDeath();
                }

                uiManager.GameOverText();
                // Tell it to stop spawning
                Destroy(this.gameObject);
            }
        }
    }

    private Coroutine _tripleShotCoroutine;
    private Coroutine _speedCoroutine;

    public void ActivateSprite(string spriteType)
    {
        switch (spriteType)
        {
            case "Triple_Shot_Powerup":
                if (_tripleShotCoroutine != null)
                {
                    StopCoroutine(_tripleShotCoroutine);
                }
                _tripleShotCoroutine = StartCoroutine(ActiveTripleShot());
                break;
            case "Speed_Powerup":
                if (_speedCoroutine != null)
                {
                    StopCoroutine(_speedCoroutine);
                }
                _speedCoroutine = StartCoroutine(ActiveSpeed());
                break;
            case "Shield_Powerup":
                _isShieldActive = true;
                if (spawnedShieldPrefab == null)
                {
                    spawnedShieldPrefab = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
                }
                break;
            // You can have any number of case statements
            default:
                Debug.Log(spriteType);
                break;
        };


    }

    IEnumerator ActiveTripleShot()
    {
        isTripleFireActive = true;
        yield return new WaitForSeconds(5f);
        isTripleFireActive = false;
    }

    IEnumerator ActiveSpeed()
    {
        _currentSpeed = 8.5f;
        isSpeedActive = true;
        yield return new WaitForSeconds(5f);
        _currentSpeed = _speed;
        isSpeedActive = false;
    }

    public void AddToScore(int amount) {
        _score += amount;
        //communicate with the UI to update the score
        uiManager.UpdateScoreText(_score);
    }

}
