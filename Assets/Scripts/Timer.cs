using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float m_TimeToAnswerQuestion = 30f;
    [SerializeField] float m_TimeToShowCorrectAnswer = 10f;

    public bool m_LoadNextQuestion;
    public bool m_IsAnsweringQuestion;

    public float m_FillFraction;
    float m_TimerValue;

    
    private void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        m_TimerValue = 0f;
    }

    public void UpdateTimer()
    {
        m_TimerValue -= Time.deltaTime;

        if (m_IsAnsweringQuestion)
        {
            if(m_TimerValue > 0)
            {
                m_FillFraction = m_TimerValue / m_TimeToAnswerQuestion;
            }
            else
            {
                m_IsAnsweringQuestion = false;
                m_TimerValue = m_TimeToShowCorrectAnswer;
            }
        }
        else
        {
            if(m_TimerValue > 0)
            {
                m_FillFraction = m_TimerValue / m_TimeToShowCorrectAnswer; ;
            }
            else
            {
                m_IsAnsweringQuestion = true;
                m_TimerValue = m_TimeToAnswerQuestion;
                m_LoadNextQuestion = true;
            }
        }

        Debug.Log(m_IsAnsweringQuestion + "  " + m_TimerValue + "  " + m_FillFraction);
    }
}
