using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private string _username;
    private int _highScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadGame();
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            _username = data.playerName;
            _highScore = data.highScore;
            
            Debug.Log(_username + " with highest score: " + _highScore);
        }
    }

    public void SetUsername(string input)
    {
        _username = input;
        SaveUsername();
    }

    public string GetUsername()
    {
        return _username;
    }

    public void SetHighScore(int value)
    {
        _highScore = value;
        SaveHighScore();
    }

    public int GetHighScore()
    {
        return _highScore;
    }

    private void SaveUsername()
    {
        SaveData data = new SaveData();
        data.playerName = _username;

        string path = Application.persistentDataPath + "/savedata.json";
        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(path, json);
    }
    
    private void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = _highScore;

        string path = Application.persistentDataPath + "/savedata.json";
        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(path, json);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int highScore;
    }
}
