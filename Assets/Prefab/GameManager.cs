using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private int score;
    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private InputField highScoreInput;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject loadLevelPanel;
    [SerializeField] private int numberOfBricks;
    [SerializeField] private Transform[] levels;
    [SerializeField] private Ball ball;
    [SerializeField] private Paddle paddle;
    public bool gameOver;
    public int currentLevelIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;

        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
        
        livesText.text = "Lives: " + lives;
    }
    
    public void UpdateScore(int points)
    {
        score += points;
        
        scoreText.text = "Score: " + score;
    }

    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)
        {
            if (currentLevelIndex >= levels.Length - 1)
            {
                GameOver();
            }
            else
            {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "Level " + (currentLevelIndex + 2);
                gameOver = true;

                Invoke("LoadLevel", 3f);
                ball.rb.velocity = Vector2.zero;
                ball.inPlay = false;
            }
            
        }
    }

    void LoadLevel()
    {
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);
        
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score > highScore)
        {   
            PlayerPrefs.SetInt("HIGHSCORE", score);
            
            highScoreText.text = "New High Score! " + "\nEnter Your Name Below.";
            highScoreInput.gameObject.SetActive(true);
        }
        else
        {
            string highScoreName = PlayerPrefs.GetString("HIGHSCORENAME");
            if (highScoreName != "")
            {
                highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s High score was " + highScore + "\n Can u beat it ?";                
            }
            highScoreInput.gameObject.SetActive(false);
        }
    }

    public void NewHighScore()
    {
        string highScoreName = highScoreInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", highScoreName);
        highScoreInput.gameObject.SetActive(false);
        highScoreText.text = "Congratulations " + highScoreName + "\nYour New High Score is : " + score;

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
 
    
    public void Quit()
    {
        SceneManager.LoadScene("Start Menu");
    }

}
