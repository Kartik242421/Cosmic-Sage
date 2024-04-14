using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0";
        
    }

    public void IncreaseScore(int amoutToIncrease)
    {
        score += amoutToIncrease;
        scoreText.text = score.ToString();
    }
}
