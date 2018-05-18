using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int whoseTurn; // 0 = x and 1 = o
    public int turnCount; // counts the number of turn played
    public GameObject[] turnIcons; // displays whos turn it is
    public Sprite[] playerIcons; // 0 = x icon and 1 = y icon
    public Button[] ticktactoeSpaces; // playable space for our game
    public int[] markedSpaces; // ID's which spcae was marked by wich player
    public Text winnerText;// Hold the text component of the winner text;
    public GameObject[] winningLine;// Hold all the different line for show that there is a winner
    public GameObject winnerPanel;
    public int xPlayerScore;
    public int oPlayerScore;
    public Text xPlayerScoreText;
    public Text oPlayerScoreText;
    public Button xPlayerButton;
    public Button oPlayerButton;
    public AudioSource winAudio;
    public AudioSource drawAudio;
    public GameObject GameMode;
    public GameObject MenuObjects;
    int prevTurn;
    int currCount;
    short currMode;
    bool isWinner;
    bool isDraw;

    int computerTurn;
    int playerTurn;
    const short AI_VS_AI = 2;
    const short PLAYER_VS_AI = 1;
    const short PLAYER_VS_PLAYER = 0;
    bool isGameStarts;
    bool pauseTurn;

    void Awake()
    {
        isGameStarts = false;
    }

    public void choosePvPMode()
    {
        currMode = PLAYER_VS_PLAYER;
        GameMode.SetActive(false);
        MenuObjects.SetActive(false);
        Restart();
    }

    public void choosePvAIMode()
    {
        currMode = PLAYER_VS_AI;
        GameMode.SetActive(false);
        MenuObjects.SetActive(false);
        Restart();
    }

    public void chooseAIvAIMode()
    {
        currMode = AI_VS_AI;
        GameMode.SetActive(false);
        MenuObjects.SetActive(false);
        Restart();
    }

    int choseAITurn()
    {
        int enemyIcon = playerTurn + 1;
        int thisIcon = computerTurn + 1;
        int thisTurn = 0;
        if ((turnCount == 0) || (turnCount == 1))
        {
           if (markedSpaces[4] == -100)
           {
                thisTurn = 4;
           }
           else if ((markedSpaces[0] == -100) || (markedSpaces[2] == -100) || (markedSpaces[6] == -100) || (markedSpaces[8] == -100))
           {
                if (markedSpaces[0] == -100)
                {
                    thisTurn = 0;
                }
                else if (markedSpaces[2] == -100)
                {
                    thisTurn = 2;
                }
                else if (markedSpaces[6] == -100)
                {
                    thisTurn = 6;
                }
                else
                {
                    thisTurn = 8;
                }
           }
           else
           {
                int j = 0;
                while (markedSpaces[j] != -100)
                {
                    ++j;
                }

                thisTurn = j;
           }
        }
        else
        {
            if ((((markedSpaces[8] == thisIcon) && (markedSpaces[5] == thisIcon)) || 
                 ((markedSpaces[0] == thisIcon) && (markedSpaces[1] == thisIcon)) ||
                 ((markedSpaces[6] == thisIcon) && (markedSpaces[4] == thisIcon)) ||
                 ((markedSpaces[8] == enemyIcon) && (markedSpaces[5] == enemyIcon)) || 
                 ((markedSpaces[0] == enemyIcon) && (markedSpaces[1] == enemyIcon)) ||
                 ((markedSpaces[6] == enemyIcon) && (markedSpaces[4] == enemyIcon))) && (markedSpaces[2] == -100))
            {
                thisTurn = 2;
            }
            else if ((((markedSpaces[2] == thisIcon) && (markedSpaces[4] == thisIcon)) || 
                      ((markedSpaces[8] == thisIcon) && (markedSpaces[7] == thisIcon)) ||
                      ((markedSpaces[0] == thisIcon) && (markedSpaces[3] == thisIcon)) ||
                      ((markedSpaces[8] == enemyIcon) && (markedSpaces[7] == enemyIcon)) || 
                      ((markedSpaces[0] == enemyIcon) && (markedSpaces[3] == enemyIcon)) ||
                      ((markedSpaces[2] == enemyIcon) && (markedSpaces[4] == enemyIcon))) && (markedSpaces[6] == -100))
            {
                thisTurn = 6;
            }
            else if ((((markedSpaces[2] == thisIcon) && (markedSpaces[5] == thisIcon)) || 
                      ((markedSpaces[6] == thisIcon) && (markedSpaces[7] == thisIcon)) ||
                      ((markedSpaces[0] == thisIcon) && (markedSpaces[4] == thisIcon)) ||
                      ((markedSpaces[6] == enemyIcon) && (markedSpaces[7] == enemyIcon)) ||
                      ((markedSpaces[0] == enemyIcon) && (markedSpaces[4] == enemyIcon)) ||
                      ((markedSpaces[2] == enemyIcon) && (markedSpaces[5] == enemyIcon))) && (markedSpaces[8] == -100))
            {
                thisTurn = 8;
            }
            else if ((((markedSpaces[6] == thisIcon) && (markedSpaces[3] == thisIcon)) || 
                      ((markedSpaces[8] == thisIcon) && (markedSpaces[4] == thisIcon)) ||
                      ((markedSpaces[2] == thisIcon) && (markedSpaces[1] == thisIcon)) ||
                      ((markedSpaces[8] == enemyIcon) && (markedSpaces[4] == enemyIcon)) ||
                      ((markedSpaces[2] == enemyIcon) && (markedSpaces[1] == enemyIcon)) ||
                      ((markedSpaces[6] == enemyIcon) && (markedSpaces[3] == enemyIcon))) && (markedSpaces[0] == -100))
            {
                thisTurn = 0;
            }
            else if ((((markedSpaces[6] == thisIcon) && (markedSpaces[8] == thisIcon)) ||
                      ((markedSpaces[1] == thisIcon) && (markedSpaces[4] == thisIcon)) ||
                      ((markedSpaces[6] == enemyIcon) && (markedSpaces[8] == enemyIcon)) ||
                      ((markedSpaces[1] == enemyIcon) && (markedSpaces[4] == enemyIcon))) && (markedSpaces[7] == -100))
            {
                thisTurn = 7;
            }
            else if ((((markedSpaces[0] == thisIcon) && (markedSpaces[2] == thisIcon)) ||
                      ((markedSpaces[7] == thisIcon) && (markedSpaces[4] == thisIcon)) ||
                      ((markedSpaces[0] == enemyIcon) && (markedSpaces[2] == enemyIcon)) ||
                      ((markedSpaces[7] == enemyIcon) && (markedSpaces[4] == enemyIcon))) && (markedSpaces[1] == -100))
            {
                thisTurn = 1;
            }
            else if ((((markedSpaces[0] == thisIcon) && (markedSpaces[6] == thisIcon)) ||
                      ((markedSpaces[5] == thisIcon) && (markedSpaces[4] == thisIcon)) ||
                      ((markedSpaces[0] == enemyIcon) && (markedSpaces[6] == enemyIcon)) ||
                      ((markedSpaces[5] == enemyIcon) && (markedSpaces[4] == enemyIcon))) && (markedSpaces[3] == -100))
            {
                thisTurn = 3;
            }
            else if ((((markedSpaces[2] == thisIcon) && (markedSpaces[8] == thisIcon)) || 
                      ((markedSpaces[3] == thisIcon) && (markedSpaces[4] == thisIcon)) ||
                      ((markedSpaces[2] == enemyIcon) && (markedSpaces[8] == enemyIcon)) ||
                      ((markedSpaces[3] == enemyIcon) && (markedSpaces[4] == enemyIcon))) && (markedSpaces[5] == -100))
            {
                thisTurn = 5;
            }
            else if ((((markedSpaces[0] == thisIcon) && (markedSpaces[8] == thisIcon)) ||
                      ((markedSpaces[6] == thisIcon) && (markedSpaces[2] == thisIcon)) ||
                      ((markedSpaces[3] == thisIcon) && (markedSpaces[5] == thisIcon)) ||
                      ((markedSpaces[1] == thisIcon) && (markedSpaces[7] == thisIcon)) ||
                      ((markedSpaces[1] == enemyIcon) && (markedSpaces[7] == enemyIcon)) ||
                      ((markedSpaces[3] == enemyIcon) && (markedSpaces[5] == enemyIcon)) ||
                      ((markedSpaces[6] == enemyIcon) && (markedSpaces[2] == enemyIcon)) ||
                      ((markedSpaces[0] == enemyIcon) && (markedSpaces[8] == enemyIcon))) && (markedSpaces[4] == -100))
            {
                thisTurn = 4;
            }
            else
            {
                List<int> emptySpaces = new List<int>();
                for (int j = 0; j < markedSpaces.Length; ++j)
                {
                    if (markedSpaces[j] == -100)
                    {
                        emptySpaces.Add(j);
                    }
                }

                int newx = Random.Range(0, emptySpaces.Count - 1);
                thisTurn = emptySpaces[newx];
            }
        }

        return thisTurn;
    }

    public IEnumerator SetPouse()
    {
        pauseTurn = true;
        yield return new WaitForSeconds(0.5f);
        pauseTurn = false;
    }

    void Update()
    {
        if ((currMode != PLAYER_VS_PLAYER) && (isGameStarts) && (!isWinner) && (!isDraw))
        {
            if ((currMode == AI_VS_AI) && !pauseTurn)
            {
                StartCoroutine("SetPouse");
                int whichCell = choseAITurn();
                TicTacToeButton(whichCell);
            }
            else if ((currMode == PLAYER_VS_AI) && (whoseTurn == computerTurn))
            {
                int whichCell = choseAITurn();
                TicTacToeButton(whichCell);
            }
        }
    }

    void GameSetup(short currMode)
    {
        if (currMode == PLAYER_VS_AI)
        {
            playerTurn = 0;
            computerTurn = 1;
        } 
        else if (currMode == PLAYER_VS_AI)
        {
            computerTurn = 0;
            computerTurn = 1;
        }
        pauseTurn = false;
        isDraw = false;
        isGameStarts = true;
        isWinner = false;

        whoseTurn = playerTurn; // 0
        turnCount = 0;
        currCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < ticktactoeSpaces.Length; ++i)
        {
            ticktactoeSpaces[i].interactable = true;
            ticktactoeSpaces[i].GetComponent<Image>().sprite = null;
        }

        for (int i = 0; i < markedSpaces.Length; ++i)
        {
            markedSpaces[i] = -100;
        }
    }
    
    public void ReturnInTurn()
    {
        if ((turnCount != 0) && (turnCount != currCount) && !isWinner && !isDraw)
        {
            ticktactoeSpaces[prevTurn].interactable = true;
            ticktactoeSpaces[prevTurn].GetComponent<Image>().sprite = null;
            markedSpaces[prevTurn] = -100;
            turnCount--;
            currCount = turnCount;
            if (whoseTurn == 0)
            {
                SwitchTurn(1, 0);
            }
            else
            {
                SwitchTurn(0, 1);
            }
        } 
    }
    
    public void Rematch()
    {
        GameSetup(currMode);

        for (int i = 0; i < winningLine.Length; ++i)
        {
            winningLine[i].SetActive(false);
        }

        winnerPanel.SetActive(false);
        xPlayerButton.interactable = true;
        oPlayerButton.interactable = true;
    }

    public void Restart()
    {
        Rematch();
        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayerScoreText.text = "0";
        oPlayerScoreText.text = "0";
    }

    public void TicTacToeButton(int whichNumber)
    {
        xPlayerButton.interactable = false;
        oPlayerButton.interactable = false;
        ticktactoeSpaces[whichNumber].image.sprite = playerIcons[whoseTurn];
        ticktactoeSpaces[whichNumber].interactable = false;

        markedSpaces[whichNumber] = whoseTurn + 1;
        prevTurn = whichNumber;
        turnCount++;
        if (turnCount > 4)
        {
            isWinner = WinnerCheck();
            if (turnCount == 9 && !isWinner)
            {
                isDraw = Draw();
            }
        }

        if (whoseTurn == 0)
        {
            SwitchTurn(1, 0);
        }
        else
        {
            SwitchTurn(0, 1);
        }
    }

    // Проверка на победителя
    bool WinnerCheck()
    {
        var solutions = new int[] {
            markedSpaces[0] + markedSpaces[1] + markedSpaces[2],
            markedSpaces[3] + markedSpaces[4] + markedSpaces[5],
            markedSpaces[6] + markedSpaces[7] + markedSpaces[8],
            markedSpaces[0] + markedSpaces[3] + markedSpaces[6],
            markedSpaces[1] + markedSpaces[4] + markedSpaces[7],
            markedSpaces[2] + markedSpaces[5] + markedSpaces[8],
            markedSpaces[0] + markedSpaces[4] + markedSpaces[8],
            markedSpaces[2] + markedSpaces[4] + markedSpaces[6]
        };

        for (int i = 0; i < solutions.Length; ++i)
        {
            if (solutions[i] == 3*(whoseTurn + 1))
            {
                winAudio.Play();
                WinnerDisplay(i);

                return true;
            }
        }

        return false;
    }

    // Отображает победителя и отрисовывает линию победы
    void WinnerDisplay(int indexIn)
    {
        isGameStarts = false;
        winnerPanel.gameObject.SetActive(true);
        if (whoseTurn == 0)
        {
            xPlayerScore++;
            xPlayerScoreText.text = xPlayerScore.ToString();
            winnerText.text = "Player X Wins!";
        }
        else if (whoseTurn == 1)
        {
            oPlayerScore++;
            oPlayerScoreText.text = oPlayerScore.ToString();
            winnerText.text = "Player O Wins!";
        }

        winningLine[indexIn].SetActive(true);
    }

    public void SwitchPlayer(int whitchPlayer)
    {
        int otherPlayer = 0;
        if (whitchPlayer == 0)
        { 
            otherPlayer = 1;
            computerTurn = otherPlayer;
        } 
        else
        {
            computerTurn = otherPlayer;
        }

        SwitchTurn(whitchPlayer, otherPlayer);
    }

    void SwitchTurn(int whitchPlayer, int otherPlayer)
    {
        whoseTurn = whitchPlayer;
        turnIcons[whitchPlayer].SetActive(true);
        turnIcons[otherPlayer].SetActive(false);
    }
    
    // активирует панель и выводит надпись Ничья с пригрышем музыки
    bool Draw()
    {
        drawAudio.Play();
        winnerPanel.SetActive(true);
        winnerText.text = "DRAW!";
        return true;
    }
}
