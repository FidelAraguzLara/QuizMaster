using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Quiz m_Quiz;
    private EndScreen m_EndScreen;

    private void Awake()
    {
        m_Quiz = FindObjectOfType<Quiz>();
        m_EndScreen = FindObjectOfType<EndScreen>();
    }

    void Start()
    {
        m_Quiz.gameObject.SetActive(true);
        m_EndScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (m_Quiz.m_IsCompleted)
        {
            m_Quiz.gameObject.SetActive(false);
            m_EndScreen.gameObject.SetActive(true);
            m_EndScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
