using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;

    public void GameOver() {
        _isGameOver = true;
    }

    private void Update() {
        if (_isGameOver) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(1);//current game scene
            }
        }
    }
}
