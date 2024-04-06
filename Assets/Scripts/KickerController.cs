using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KickerController : MonoBehaviour
{
    [SerializeField] GameLogic gameLogic;
    [SerializeField] TextMeshProUGUI Counter;
    [SerializeField] Transform target;
    [SerializeField] GameObject ballPrefub;
    [SerializeField] float targetMoveSpeed;
    [SerializeField] float counterSpeed;
    [SerializeField] float shotPower;
    GameObject ballObject;
    Rigidbody ball;
    float directionHori;
    float directionVert;
    bool countDown = true;
    float count = 0;
    bool kicked;
    // Start is called before the first frame update
    void Start()
    {
        countDown = false;
        Counter.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameLogic.playerTurn)
        {

            if (countDown)
            {
                count += Time.deltaTime * counterSpeed;
                Counter.text = ((int)count).ToString();
                if (count >= 100)
                {
                    count = 100;
                    KickBall();
                }
            }
            target.transform.Translate(directionHori * targetMoveSpeed * Time.deltaTime, directionVert * targetMoveSpeed * Time.deltaTime, 0);
        }
        if (kicked)
        {
            if(ballObject != null)
            {
                if (ball.velocity.z <= 0)
                {
                    Debug.Log("Player Ball Stopped");
                    StartCoroutine(BallStopped());
                    kicked = false;
                }
            }
        }
    }

    IEnumerator BallStopped()
    {
        yield return new WaitForSeconds(2);
        Destroy(ballObject);
        gameLogic.Missed();
        kicked = false;
    }

    public void ButtonPressed(string button)
    {
        switch (button)
        {
            case "Right":
                directionHori = 1;
                break;
            case "Left":
                directionHori = -1;
                break;
            case "StopHori":
                directionHori = 0;
                break;
            case "StopVert":
                directionVert = 0;
                break;
            case "Up":
                directionVert = 1;
                break;
            case "Down":
                directionVert = -1;
                break;
            case "Prepare":
                countDown = true;
                break;
            case "Shot":
                if(countDown)
                {
                    KickBall();
                }
                break;
        }
    }
    public void NewRound()
    {
        Debug.Log("Player Turn Start");
        count = 0;
        target.position = new Vector3(0, 2, target.position.z);
        ballObject = Instantiate(ballPrefub,gameLogic.startPos.position, Quaternion.identity);
        ball = ballObject.GetComponent<Rigidbody>();
        ball.useGravity = gameLogic.useGravity;
        kicked = false;
    }
    void KickBall()
    {
        Vector3 trajectory = target.position - gameLogic.startPos.position;
        trajectory.Normalize();
        ball.AddForce(trajectory * count * shotPower, ForceMode.Impulse);
        countDown = false;
        Counter.text = "";
        StartCoroutine(BallKicked());
    }
    IEnumerator BallKicked()
    {
        yield return new WaitForSeconds(0.25f);
        kicked = true;
    }
}
