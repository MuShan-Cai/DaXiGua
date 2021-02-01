using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreContro : MonoBehaviour
{

    public DynamicNumberShower scoreShower;

    public static ScoreContro Instance
    {
        get
        {
            return instance;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
    }


    private static ScoreContro instance;
    private int score = 0;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            throw new UnityException("已有实例：" + name);
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreShower.RunNumber(score);   
    }

    public void Restart()
    {
        score = 0;
        scoreShower.RunNumber(score);
    }



}
