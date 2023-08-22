using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string m_Question = "";
    [SerializeField] string[] m_Answers = new string[4];
    [SerializeField] int m_CorrectAnswer;
    
    public string GetQuestion()
    {
        return m_Question;
    }

    public int GetCorrectAnswerIndex()
    {
        return m_CorrectAnswer;
    }

    public string GetAnswer(int a_Index)
    {
        return m_Answers[a_Index];
    }
}
