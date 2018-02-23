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
    public AudioSource buttonClickAudio;
    public AudioSource gridClickAudio;
    public AudioSource winAudio;
    public AudioSource drawAudio;

    // Use this for initialization
    void Start () {
        GameSetup();
    }
	
    void GameSetup()
    {
        whoseTurn = 0;
        turnCount = 0;
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

	// Update is called once per frame
	void Update () {
		
	}

    public void TicTacToeButton(int whichNumber)
    {
        xPlayerButton.interactable = false;
        oPlayerButton.interactable = false;
        ticktactoeSpaces[whichNumber].image.sprite = playerIcons[whoseTurn];
        ticktactoeSpaces[whichNumber].interactable = false;

        markedSpaces[whichNumber] = whoseTurn+1;
        turnCount++;
        if (turnCount > 4)
        {
            bool isWinner = WinnerCheck();
            if (turnCount == 9 && !isWinner)
            {
                Draw();
            }
        }

        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoseTurn = 0;
            turnIcons[1].SetActive(false);
            turnIcons[0].SetActive(true);
        }
    }

    bool WinnerCheck ()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < solutions.Length; ++i)
        {
            if (solutions[i] == 3*(whoseTurn+1))
            {
                winAudio.Play();
                WinnerDisplay(i);
                Debug.Log("player " + whoseTurn + " won!");
                return true;
            }
        }

        return false;
    }

    void WinnerDisplay(int indexIn)
    {
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

    public void Rematch()
    {
        GameSetup();

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

    public void SwitchPlayer(int whitchPlayer)
    {
        if (whitchPlayer == 0)
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if (whitchPlayer == 1)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }

    void Draw()
    {
        drawAudio.Play();
        winnerPanel.SetActive(true);
        winnerText.text = "DRAW!";
    }

    public void PlayButtonClick()
    {
        buttonClickAudio.Play();
    }

    public void PlayGridClick()
    {
        gridClickAudio.Play();
    }
}
