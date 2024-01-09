using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4f;
    // Start is called before the first frame update

    [SerializeField]
    public float _damage = 10;

    [SerializeField]
    private Player _player;

    //handle to animator component
    private Animator _anim;

    private AudioSource _audioSource;
    void Start()
    {
        transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(7.0f, 9.0f), 0);

        //asign the component to Anim
        _player = GameObject.Find("Player").GetComponent<Player>();
 
         _anim = GetComponent<Animator>();

        if (!_anim) {
            Debug.LogError("Anim is NULL");
        }

        _audioSource = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= -4f)
        {
            transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(7.0f, 9.0f), 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        // If other is player
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player)
            {
                player.Damage(_damage);
            }
            //trigger animation
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0.1f;
            _audioSource.Play();
            Destroy(gameObject, 2.8f); // Destroy the enemy or object this script is attached to
        }
        // If other is laser
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject); // Destroy the laser
            //add 10 to score
            
            if (_player)
            {
                _player.AddToScore(10);
            }

            //trigger animation
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0.1f;
            _audioSource.Play();
            Destroy(gameObject,2.8f); // Destroy the enemy or object this script is attached to
        }

        //Instantiate(_enemyPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(7.0f, 9.0f), 0), Quaternion.identity);


    }

}
