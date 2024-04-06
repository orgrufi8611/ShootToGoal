using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingScreenScript : MonoBehaviour
{
    [SerializeField] Button easy;
    [SerializeField] Button mid;
    [SerializeField] Button hard;
    [SerializeField] Toggle gateMovement;
    [SerializeField] Toggle useGravity;
    [SerializeField] TMP_InputField distance;
    [SerializeField] Toggle randomDistance;
    [SerializeField] Image highlight;

    private void Start()
    {
        GameDetails gameDetails = GameDetails.Instance;
        highlight.gameObject.SetActive(false);
        gateMovement.isOn = false;
        useGravity.isOn = false;
        randomDistance.isOn = false;
    }

    public void SetDifficulty(int id)
    {
        highlight.gameObject.SetActive(true);
        switch (id)
        {
            case 0:
                GameDetails.SetDifficulty(30);
                highlight.rectTransform.localPosition = easy.image.rectTransform.localPosition;
                break;
            case 1:
                GameDetails.SetDifficulty(60);
                highlight.rectTransform.localPosition = mid.image.rectTransform.localPosition;
                break;
            case 2:
                GameDetails.SetDifficulty(90);
                highlight.rectTransform.localPosition = hard.image.rectTransform.localPosition;
                break;
        }
    }

    public void StartGame()
    {
        if(randomDistance.isOn)
        {
            GameDetails.SetDistance(Random.Range(10, 30));
        }
        else
        {
            int setDistance = int.Parse(distance.text);
            setDistance = Mathf.Clamp(setDistance, 10, 30);
            GameDetails.SetDistance(setDistance);
        }
        GameDetails.SetGateMovement(gateMovement.isOn);
        GameDetails.SetGravity(useGravity.isOn);
        GameDetails.SetPractice(false);
        GameDetails.PrintDetails();
        NextScene();
    }

    public void PracticeGame()
    {
        if (randomDistance.isOn)
        {
            GameDetails.SetDistance(Random.Range(10, 30));
        }
        else
        {
            int setDistance = int.Parse(distance.text);
            setDistance = Mathf.Clamp(setDistance, 10, 30);
            GameDetails.SetDistance(setDistance);
        }
        GameDetails.SetGateMovement(gateMovement.isOn);
        GameDetails.SetGravity(useGravity.isOn);
        GameDetails.SetPractice(true);
        GameDetails.PrintDetails();
        NextScene();
    }

    void NextScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
