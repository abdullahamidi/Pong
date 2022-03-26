using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum GameStates
{
    Start,
    Play,
    Serve,
    Done
}
public class GameManager : MonoBehaviour
{
    GameStates gameState;
    [SerializeField]
    private Ball ball;
    [SerializeField]
    private Racket p1;
    [SerializeField]
    private Racket p2;
    [SerializeField]
    private Text header;
    [SerializeField]
    private Text header2;
    [SerializeField]
    private Text p1ScoreText;
    [SerializeField]
    private Text p2ScoreText;
    private int p1Score = 0;
    private int p2Score = 0;



    // Start is called before the first frame update
    void Start()
    {
        header.text = "Tap anywhere to start the game";
        header2.text = "";
        SetScoreTexts();
        gameState = GameStates.Start;
        GetBallDirection(-3f, 3f, -0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameStates.Play)
        {
            MoveBall();

            if (ball.transform.position.x + ball.transform.localScale.x < p1.transform.position.x)
            {
                Score("P2");
            }
            else if (ball.transform.position.x > p2.transform.position.x + ball.transform.localScale.x)
            {
                Score("P1");
            }
        }

        if (gameState != GameStates.Play && Input.GetMouseButtonDown(0)) //&& !EventSystem.current.IsPointerOverGameObject())
        {
            ChangeGameState();
        }
    }

    private void ChangeGameState()
    {
        if (gameState == GameStates.Start)
        {
            header.text = "";
            header2.text = "";
            gameState = GameStates.Play;
        }
        else if (gameState == GameStates.Serve)
        {
            header.text = "";
            header2.text = "";
            gameState = GameStates.Play;
        }
        else if (gameState == GameStates.Done)
        {
            gameState = GameStates.Serve;
        }

        //ball.ResetBall();
    }

    public void GetBallDirection(float minX, float maxX, float minY, float maxY)
    {
        ball.DeltaX = Random.Range(minX, maxX);
        ball.DeltaY = Random.Range(minY, maxY);
    }

    private void MoveBall()
    {
        Vector2 destinationPos = new Vector2(ball.transform.position.x + ball.DeltaX, ball.transform.position.y + ball.DeltaY);
        ball.transform.position = Vector2.MoveTowards(ball.transform.position, destinationPos, ball.Speed * Time.deltaTime);
    }

    private void Score(string scorer)
    {
        if (scorer == "P1")
        {
            p1Score++;
            GetBallDirection(-3f, 0f, -0.5f, 0.5f);
            header.text = "Player 2 Serving";
            header2.text = "Tap to serve";
        }
        else if (scorer == "P2")
        {
            p2Score++;
            GetBallDirection(0f, 3f, -0.5f, 0.5f);
            header.text = "Player 1 Serving";
            header2.text = "Tap to serve";
        }
        ball.ResetBall();
        p1.ResetPos();
        p2.ResetPos();
        SetScoreTexts();
        gameState = GameStates.Serve;
    }
    private void SetScoreTexts()
    {
        p1ScoreText.text = p1Score.ToString();
        p2ScoreText.text = p2Score.ToString();
    }
}
