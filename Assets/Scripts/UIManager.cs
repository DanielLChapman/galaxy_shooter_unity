using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //handle to text
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Image _LivesImg;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private bool _flicker = false;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //assign txt component to the handle    
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!_gameManager) {
            Debug.Log("No Game Manager");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScoreText(int newScore)
    {
        _scoreText.text = "Score: " + newScore;
    }

    public void UpdateLives(int lives)
    {
        if (lives < 0 || lives > 4)
        {
            return;
        }

        //display img sprite
        _LivesImg.sprite = _liveSprites[lives];
        //give it a new one
    }

    private Coroutine _flickerRoutine;

    public void GameOverText()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _flicker = true;
        _flickerRoutine = StartCoroutine(GameOverTextFlicker());
        if (_gameManager)
        {
            _gameManager.GameOver();
        }

    }

    /*   public void Reset() {
          _flicker = false;
          StopCoroutine(_flickerRoutine);
          _gameOverText.gameObject.SetActive(false);
          _restartText.gameObject.SetActive(false);
          Instantiate(_playerObject, new Vector3(0,0, 0), Quaternion.identity);
      } */

    IEnumerator GameOverTextFlicker()
    {
        bool currentState = true;
        while (_flicker)
        {

            _gameOverText.gameObject.SetActive(!currentState);
            currentState = !currentState;
            yield return new WaitForSeconds(0.5f);

        }
    }
}
