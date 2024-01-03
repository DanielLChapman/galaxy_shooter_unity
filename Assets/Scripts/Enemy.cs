using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _speed = 4f;
    // Start is called before the first frame update

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    public float _damage = 10;
    void Start()
    {

        transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(7.0f, 9.0f), 0);
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

    void OnTriggerEnter(Collider other)
    {
        // If other is player
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player)
            {
                player.Damage(_damage);
            } 



            Destroy(gameObject); // Destroy the enemy or object this script is attached to
        }
        // If other is laser
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject); // Destroy the laser
            Destroy(gameObject); // Destroy the enemy or object this script is attached to
        }

        //Instantiate(_enemyPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(7.0f, 9.0f), 0), Quaternion.identity);


    }

}
