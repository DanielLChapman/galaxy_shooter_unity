using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _deleteTime = 3.0f;

     private AudioSource _explosionSound;
    void Start()
    {
        Destroy(gameObject, _deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
