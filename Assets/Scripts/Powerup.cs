using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3f;
    // Start is called before the first frame update

    [SerializeField]
     private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= -4f)
        {
            Destroy(this.gameObject);
        }

        
    }

    void OnTriggerEnter2D(Collider2D other) {
         if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_clip, transform.position);
                player.ActivateSprite(this.gameObject.tag);
            }
            Destroy(gameObject); // Destroy the enemy or object this script is attached to
        }
    }
    //on trigger collision
    //only collectible by player
    //destoy on collection


}
