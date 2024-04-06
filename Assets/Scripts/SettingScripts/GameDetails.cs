using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDetails
{
    public static GameDetails instance = null;
    public static float DifficultyLevel {  get; private set; }
    public static bool UseGravity {  get; private set; }
    public static bool GateMovement { get; private set; }
    public static float Distance {  get; private set; }
    public static bool Practice {  get; private set; }

    // Start is called before the first frame update
    public static GameDetails Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameDetails();
                DifficultyLevel = 30;
                UseGravity = true;
                GateMovement = false;
                Distance = 10;
            }
            return instance;
        }
    }
    public static void SetDifficulty(int difficulty)
    {
        DifficultyLevel = difficulty;
    }

    public static void SetGravity(bool gravity)
    {
        UseGravity = gravity;
    }

    public static void SetGateMovement(bool gateMovement)
    {
        GateMovement = gateMovement;
    }

    public static void SetDistance(float distance)
    {
        Distance = distance;
    }

    public static void SetPractice(bool practice)
    {
        Practice = practice;
    }

    public static void PrintDetails()
    {
        Debug.Log("DIfficulty: " + DifficultyLevel);
        Debug.Log("Distance: " + Distance);
        Debug.Log("UseGravity: " + UseGravity.ToString());
        Debug.Log("GateMovement: " + GateMovement.ToString());
        Debug.Log("PracticeMode: " +  Practice.ToString());

    }
}
