using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int m_CorrectAnswers = 0;
    int m_AnsweredQuestion = 0;

    public int GetCorrectAnswers()
    {
        return m_CorrectAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        m_CorrectAnswers++;
    }

    public int GetAnsweredQuestion()
    {
        return m_AnsweredQuestion;
    }

    public void IncrementAnsweredQuestion()
    {
        m_AnsweredQuestion++;
    }

    public int CalculateScore()
    {
        return  Mathf.RoundToInt(m_CorrectAnswers / (float)m_AnsweredQuestion * 100);
    }
}