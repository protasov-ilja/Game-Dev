using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class GameController : MonoBehaviour
{
    public int lives = 3;
    public int bricks = 0;
    public int score = 0;
    public float resetDelay = 2.4f;
    public Text livesText;
    public Text scoreText;
    public GameObject gameOver;
    public GameObject gameWon;
    public GameObject bricksPrefab;
    public GameObject arrayOfBricks;
    public GameObject bar;
    public GameObject deathParticles;
    public static GameController instance = null;
    public GameObject brick;
    public GameObject SoundController;
    public AudioSource DestroySound;
    public AudioSource WinSound;
    public AudioSource LoseSound;

    private GameObject cloneBar;
    private GameObject cloneDeathParticle;

    void Start ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Setup();
    }

    public void Setup()
    {
        livesText.text = "Lives: " + lives;
        bricksPrefab = LoadLevel(MainMenu.choselvl);
        cloneBar = Instantiate(bar, transform.position, Quaternion.identity) as GameObject;
    }

    void CheckGameOver()
    {
        if (bricks < 1)
        {
            WinSound.Play();
            gameWon.SetActive(true);
            Time.timeScale = .5f;
            Invoke("Resert", 2.5f);
        }
        
        if (lives < 1)
        {
            LoseSound.Play();
            gameOver.SetActive(true);
            Time.timeScale = .5f;
            Invoke("Resert", 3.4f);
        }
    }

    void Resert()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
	
	public void LoseLife()
    {
        --lives;
        livesText.text = "Lives: " + lives;
        cloneDeathParticle = Instantiate(deathParticles, cloneBar.transform.position, Quaternion.identity);
        Destroy(cloneBar);
        Destroy(cloneDeathParticle, 1.5f);
        if ((lives < 1) || (bricks < 1))
        {
            Invoke("SetupBar", 3.5f);
        }
        else
        {
            Invoke("SetupBar", resetDelay);
        }

        CheckGameOver();
    }

    void SetupBar()
    {
        cloneBar = Instantiate(bar, transform.position, Quaternion.identity) as GameObject;
    }

    private GameObject LoadLevel(int currLevel)
    {
        StreamReader lvlFile = new StreamReader("D:\\Work\\Game-Dev\\Arcanoid\\Assets\\Resources\\Level" + currLevel + ".txt");
        GameObject objectLvl = new GameObject("Level");
        objectLvl.transform.position = Vector3.zero;
        Vector3 position1 = new Vector3(-2.5f, 5.5f, 0);
        objectLvl.transform.Translate(position1);
        float i = 8f;
        while (!lvlFile.EndOfStream)
        {
            string line = lvlFile.ReadLine();
            float f = -7.5f;
            for (int j = 0; j < line.Length; ++j)
            {
                f += 2.5f;
                if (line[j] == 'X')
                {
                    ++bricks;
                    Vector3 position = new Vector3(f, i, 0);
                    GameObject box = Instantiate(brick, position, Quaternion.identity) as GameObject;
                    box.transform.SetParent(objectLvl.transform, false);
                }
            }

            i -= 2.5f;
        }

        return objectLvl;
    }

    public void DestroyBrick()
    {
        DestroySound.Play();
        score += 10;
        if ((score >= 100) && (scoreText.fontSize != 10))
        {
            scoreText.fontSize = 10;
        }

        scoreText.text = "Score: " + score;
        --bricks;
        CheckGameOver();
    }

    public void OpenMainMenu()
    {
        Invoke("LoadMainMenuScene", 0.2f);
    }

    private void LoadMainMenuScene()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
