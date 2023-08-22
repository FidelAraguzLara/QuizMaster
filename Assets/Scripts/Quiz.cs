using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI m_QuestionText;
    [SerializeField] List<QuestionSO> m_QuestionList = new List<QuestionSO>();
    QuestionSO m_CurrentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] m_AnswerButtons;
    int m_CorrectAnswerIndex;
    bool m_HasAnswerEarly = true;

    [Header("Buttons states")]
    [SerializeField] Sprite m_DefaultSprite;
    [SerializeField] Sprite m_CorrectSprite;

    [Header("Timer")]
    [SerializeField] Image m_TimerImage;
    Timer m_Timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI m_ScoreText;
    ScoreKeeper m_ScoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider m_ProgressBar;

    [Header("Game End")]
    public bool m_IsCompleted;

    void Awake()
    {
        m_ScoreKeeper = FindObjectOfType<ScoreKeeper>();
        m_Timer = FindObjectOfType<Timer>();
        m_ProgressBar.maxValue = m_QuestionList.Count;
        m_ProgressBar.value = 0;
    }

    private void Update()
    {
        m_TimerImage.fillAmount = m_Timer.m_FillFraction;

        if (m_Timer.m_LoadNextQuestion)
        {
            if (m_ProgressBar.value == m_ProgressBar.maxValue)
            {
                m_IsCompleted = true;
                return;
            }

            m_HasAnswerEarly = false;
            GetNextCuestion();
            m_Timer.m_LoadNextQuestion = false;
        }
        else if (!m_HasAnswerEarly && !m_Timer.m_IsAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }


    public void OnAnswerSelected(int a_Index)
    {
        m_HasAnswerEarly = true;
        DisplayAnswer(a_Index);
        SetButtonState(false);
        m_Timer.CancelTimer();
        m_ScoreText.text = "Score: " + m_ScoreKeeper.CalculateScore() + "%";
    }

    private void DisplayAnswer(int a_Index)
    {
        Image a_ButtonImage;

        if (a_Index == m_CurrentQuestion.GetCorrectAnswerIndex())
        {
            m_QuestionText.text = "¡Correcto!";
            a_ButtonImage = m_AnswerButtons[a_Index].GetComponent<Image>();
            a_ButtonImage.sprite = m_CorrectSprite;
            m_ScoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            m_QuestionText.text = "Uyy, la respuesta correcta era: \n" + m_CurrentQuestion.GetAnswer(m_CurrentQuestion.GetCorrectAnswerIndex());
            a_ButtonImage = m_AnswerButtons[m_CurrentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
            a_ButtonImage.sprite = m_CorrectSprite;
        }
    }

    private void GetNextCuestion()
    {
        if (m_QuestionList.Count > 0)
        {
            SetButtonState(true);
            SetButtonSpritesToDefault();
            GetRandonQuestion();
            DisplayCuestion();
            m_ProgressBar.value++;
            m_ScoreKeeper.IncrementAnsweredQuestion();
        } 
    }

    private void GetRandonQuestion()
    {
        int a_QuiestionIndex = Random.Range(0, m_QuestionList.Count);
        m_CurrentQuestion = m_QuestionList[a_QuiestionIndex];

        if (m_QuestionList.Contains(m_CurrentQuestion))
        {
            m_QuestionList.Remove(m_CurrentQuestion);
        }
    }

    public void DisplayCuestion()
    {
        m_QuestionText.text = m_CurrentQuestion.GetQuestion();

        for (int i = 0; i < m_AnswerButtons.Length; i++)
        {
            TextMeshProUGUI a_ButtonText = m_AnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            a_ButtonText.text = m_CurrentQuestion.GetAnswer(i);
        }
    }

    private void SetButtonState(bool a_State)
    {
        for (int i = 0; i < m_AnswerButtons.Length; i++)
        {
            Button a_Button = m_AnswerButtons[i].GetComponent<Button>();
            a_Button.interactable = a_State;
        }
    }

    private void SetButtonSpritesToDefault()
    {
        for (int i = 0; i < m_AnswerButtons.Length; i++)
        {
            Image a_ButtonSprite = m_AnswerButtons[i].GetComponent<Image>();
            a_ButtonSprite.sprite = m_DefaultSprite;
        }
    }
}
