using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_FinalScoreText;
    ScoreKeeper m_ScoreKeeper;

    private void Awake()
    {
        m_ScoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        m_FinalScoreText.text = "¡FELICIDADES!\n" +
                                "Tu puntaje fue: " + m_ScoreKeeper.CalculateScore() + "%";
    }
}
