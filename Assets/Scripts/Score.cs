using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static TMP_Text scoreText;

    private static int m_score = 0;
    public static int score {
        get { return m_score; }
        set {
            m_score += value;
            scoreText.text = "Score : " + m_score;
        }
    }

    private void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
    }
}
