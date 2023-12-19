using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class UserManager : MonoBehaviour
{
    public static UserManager Instance;

    public string UserName;
    public int high_score;
    public string champName;

    public TMP_InputField input;
    public TextMeshProUGUI BestScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
        BestScoreText.text = $"Best Score:{champName}:{high_score}";
        input.text = champName;
    }

    public void OnEndedInputField()
    {
        UserName = input.text;
        Debug.Log(UserName);
    }

    public void UpdateHighScore(int new_high_score)
    {
        high_score = new_high_score;
        champName = UserName;
        SaveScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string champName;
        public int highScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.champName = champName;
        data.highScore = high_score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            high_score = data.highScore;
            champName = data.champName;
        }
    }
}
