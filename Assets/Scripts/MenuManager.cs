using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Drawing;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public int bestScore;
    public string playerName, currentPlayer;
    [SerializeField] private TMP_InputField playerNameField;
    [SerializeField] private TMP_Text bestScoreText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadGame();
        bestScoreText.text = "Hight Score : " + playerName + " : " + bestScore;
    }
    public void StartGame()
    {
        currentPlayer = playerNameField.text;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int score;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.score = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // Need System.IO
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.playerName;
            bestScore = data.score;
        }
    }
}
