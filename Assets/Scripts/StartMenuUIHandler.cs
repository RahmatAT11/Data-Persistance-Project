using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuUIHandler : MonoBehaviour
{
    [SerializeField] private InputField _usernameInput;
    [SerializeField] private Text _bestScoreText;

    private void Start()
    {
        _usernameInput.text = GameManager.Instance.GetUsername();
        _bestScoreText.text = "Best Score : " + GameManager.Instance.GetUsername() +
                              " : " + GameManager.Instance.GetHighScore();
    }

    public void GetUsername()
    {
        GameManager.Instance.SetUsername(_usernameInput.text);
    }
}
