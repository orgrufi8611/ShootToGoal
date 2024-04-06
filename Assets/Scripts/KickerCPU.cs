using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickerCPU : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] GameObject ballPrefub;
    [SerializeField] Transform topRightGate;
    [SerializeField] Transform bottomLeftGate;
    [SerializeField] Vector3 setTarget;
    [SerializeField] float shotPower;
    GameObject ballObject;
    Rigidbody ball;
    bool startRound;
    bool kicked;
    // Start is called before the first frame update
    void Start()
    {
        startRound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startRound)
        {
            target.Translate((setTarget- target.position) * Time.deltaTime);
            if(Vector3.Distance(target.position, setTarget) <= 0.1)
            {
                KickBall();
            }
        }
        if (kicked)
        {
            if (ballObject != null)
            {
                if (ball.velocity.z <= 0)
                {
                    Debug.Log("CPU Ball Stopped");
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


    public void NewRound()
    {
        Debug.Log("CPU Turn Start");
        target.position = new Vector3(0,2,target.position.z);
        ballObject = Instantiate(ballPrefub, gameLogic.startPos.position, Quaternion.identity);
        ball = ballObject.GetComponent<Rigidbody>();
        ball.useGravity = gameLogic.useGravity;
        int r = Random.Range(0, 100);
        if(r<gameLogic.difficulty)
        {
            float x = Random.Range(bottomLeftGate.position.x,topRightGate.position.x);
            float y = Random.Range(bottomLeftGate.position.y, topRightGate.position.y);
            float z = bottomLeftGate.position.z;
            setTarget = new Vector3(x,y,z);
        }
        else
        {
            float x = Random.Range(topRightGate.position.x, topRightGate.position.x + 5);
            float y = Random.Range(topRightGate.position.y, topRightGate.position.y + 5);
            float z = bottomLeftGate.position.z;
            setTarget = new Vector3(x, y, z);
        }
        startRound = true;
        kicked = false;
    }
    void KickBall()
    {
        Vector3 trajectory = target.position - gameLogic.startPos.position;
        trajectory.Normalize();
        ball.AddForce(trajectory * 50 * shotPower, ForceMode.Impulse);
        StartCoroutine(BallKicked());
        startRound = false;
    }
    IEnumerator BallKicked()
    {
        yield return new WaitForSeconds(0.25f);
        kicked = true;
    }
}
