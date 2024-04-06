using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CPUDetail;
    [SerializeField] TextMeshProUGUI practiceMode;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI difficultyDisplay;
    [SerializeField] TextMeshProUGUI useGravityDisplay;
    [SerializeField] TextMeshProUGUI gateMovementDisplay;
    [SerializeField] GameObject playerTurnIndicator;
    [SerializeField] GameObject popUpWindow;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI gameOverScore;
    [SerializeField] TextMeshProUGUI gameOverState;

    public Transform startPos;
    public bool useGravity;
    public bool playerTurn;
    public int playerScore;
    public int cpuScore;
    public float difficulty;
    public float distance;
    [SerializeField] Transform gate;
    [SerializeField] KickerController kickerController;
    [SerializeField] KickerCPU kickerCpu;
    float gateSpeed;
    int gateDirection;
    bool gateMovement;
    bool isPractice;
    int turns;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        turns = 0;
        popUpWindow.SetActive(false);
        GameDetails gameDetails = GameDetails.Instance;
        gateDirection = 1;
        gateSpeed = 60 / 3;
        useGravity = GameDetails.UseGravity;
        gateMovement = GameDetails.GateMovement;
        difficulty = GameDetails.DifficultyLevel;
        distance = GameDetails.Distance;
        isPractice = GameDetails.Practice;
        startPos.position = gate.position - Vector3.forward * distance;
        playerTurn = true;
        kickerController.NewRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn)
        {
            playerTurnIndicator.SetActive(true);
        }
        else
        {
            playerTurnIndicator.SetActive(false);
        }
        score.text = cpuScore + " - " + playerScore;
        difficultyDisplay.text = (difficulty / 30).ToString();
        useGravityDisplay.text = useGravity.ToString();
        gateMovementDisplay.text = gateMovement.ToString();
        practiceMode.text = isPractice.ToString();
        if (gateMovement)
        {
            if(gate.position.x >= 30)
            {
                gateDirection = -1;
            }
            else if(gate.position.x <= -30) 
            {
                gateDirection = 1;
            }
            gate.Translate(gateDirection * gateSpeed * Time.deltaTime, 0, 0);
        }
    }

        
    public void StartNewRound()
    {
        if (turns >= 20)
        {

        }
        else
        {


            turns++;
            popUpWindow.SetActive(false);
            if (playerTurn || isPractice)
            {
                kickerController.NewRound();
            }
            else
            {
                kickerCpu.NewRound();
            }
        }
    }
    public void Scored()
    {
        
        if(playerTurn)
        {
            playerScore++;
            if (!isPractice)
            {
                playerTurn = !playerTurn;
            }
            StartNewRound();
        }
        else
        {
            CPUDetail.text = "CPU Scorred";
            playerTurn = !playerTurn;
            popUpWindow.SetActive(true);
            cpuScore++;
        }
    }

    public void GameOver()
    {
        if(playerScore > cpuScore)
        {
            gameOverState.text = "You Win";
        }
        else
        {
            gameOverState.text = "You Lose";
        }
        gameOverScore.text = cpuScore.ToString() + " - " + playerScore.ToString();
        gameOverScreen.SetActive(true);
    }
    public void Missed()
    {
        if (!playerTurn)
        {
            CPUDetail.text = "CPU missed";
            popUpWindow.SetActive(true);
        }
        else
        {
            StartNewRound();
        }
        playerTurn = !playerTurn;
        
    }

    public void Back()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
