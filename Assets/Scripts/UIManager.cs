using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    Sprite[] liveSprites;

    [SerializeField]
    Image livesDisplay;

    [SerializeField]
    TextMeshProUGUI gameOverText;

    [SerializeField]
    TextMeshProUGUI restartGameText;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        gameOverText.gameObject.SetActive(false);
        restartGameText.gameObject.SetActive(false);
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        if (!gameManager)
        {
            Debug.LogError("Game Manager is NULL.");
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int currentHealth)
    {
        livesDisplay.sprite = liveSprites[currentHealth];
        if (currentHealth < 1)
        {
            GameOverSequence();
        }
    }

    IEnumerator FlickerGameOverText()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    void GameOverSequence()
    {
        restartGameText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(FlickerGameOverText());
        gameManager.GameOver();
    }

}
