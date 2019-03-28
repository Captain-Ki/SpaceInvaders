using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreManager : MonoBehaviour
{

    public GameObject HighScoreDisplay;

    public GameObject NameInput;

    private Text nameInputObject;

    public Text scoreDisplay;

    public Text[] highScoresTables;

    public int score = 0;

    private int i;

    public string p_name = "PLAYER";

    private void Start()
    {
        if (HighScoreDisplay == null || scoreDisplay == null)
        {
            return;
        }
        SetInitialValues();
    }

    private void Update()
    {
        scoreDisplay.text = score.ToString();
    }

    public void ModifyScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void SetInitialValues()
    {
        if (nameInputObject == null)
        {
            nameInputObject = NameInput.GetComponentInChildren<Text>();
        }

        HighScores.aScoreHasBeenSet = false;

        Time.timeScale = 1;

        score = 0;

        i = 0;

        nameInputObject.text = "";

        HighScoreDisplay.SetActive(false);

    }

    public void PlayerDied()
    {
        HighScores.LoadHighScores();

        NameInput.SetActive(true);

        nameInputObject.text += Input.inputString;

        p_name = nameInputObject.text;

        if (Input.GetKeyDown(KeyCode.Return))
        {

            ShowHighScores();

        }

        Time.timeScale = 0;
    }


    private void ShowHighScores()
    {
        if (!HighScores.scoreTable.Contains(new HighScores.scoreKeeper(p_name, score)))
        {
            HighScores.AddScore(p_name, score);
        }

        highScoresTables[0].text = HighScores.scoreTable[0].name + HighScores.scoreTable[0].score;
        highScoresTables[1].text = HighScores.scoreTable[1].name + HighScores.scoreTable[1].score;
        highScoresTables[2].text = HighScores.scoreTable[2].name + HighScores.scoreTable[2].score;
        highScoresTables[3].text = HighScores.scoreTable[3].name + HighScores.scoreTable[3].score;

        HighScores.SaveHighScores();

        HighScoreDisplay.SetActive(true);
    }

}

public static class HighScores
{


    public static scoreKeeper[] scoreTable = new scoreKeeper[] { new scoreKeeper("Nobody", 0),
                                                                        new scoreKeeper("Nobody", 0),
                                                                        new scoreKeeper("Nobody", 0),
                                                                        new scoreKeeper("Nobody", 0)};



    public static bool aScoreHasBeenSet;

    public static void AddScore(string Name, int Score)
    {
        foreach (scoreKeeper scoreHelper in scoreTable)
        {
            if (scoreHelper.score < Score && !aScoreHasBeenSet)
            {
                scoreHelper.name = Name;
                scoreHelper.score = Score;

                aScoreHasBeenSet = true;
            }
        }
    }

    public static void SaveHighScores()
    {
        PlayerPrefs.SetString("Slot1_name", scoreTable[0].name);
        PlayerPrefs.SetString("Slot2_name", scoreTable[1].name);
        PlayerPrefs.SetString("Slot3_name", scoreTable[2].name);
        PlayerPrefs.SetString("Slot4_name", scoreTable[3].name);


        PlayerPrefs.SetInt("Slot1_score", scoreTable[0].score);
        PlayerPrefs.SetInt("Slot2_score", scoreTable[1].score);
        PlayerPrefs.SetInt("Slot3_score", scoreTable[2].score);
        PlayerPrefs.SetInt("Slot4_score", scoreTable[3].score);

    }

    public static void LoadHighScores()
    {
        scoreTable[0].name = (PlayerPrefs.GetString("Slot1_name") == "") ? "Nobody" : PlayerPrefs.GetString("Slot1_name");
        scoreTable[1].name = (PlayerPrefs.GetString("Slot2_name") == "") ? "Nobody" : PlayerPrefs.GetString("Slot2_name");
        scoreTable[2].name = (PlayerPrefs.GetString("Slot3_name") == "") ? "Nobody" : PlayerPrefs.GetString("Slot3_name");
        scoreTable[3].name = (PlayerPrefs.GetString("Slot4_name") == "") ? "Nobody" : PlayerPrefs.GetString("Slot4_name");


        scoreTable[0].score = PlayerPrefs.GetInt("Slot1_score");
        scoreTable[1].score = PlayerPrefs.GetInt("Slot2_score");
        scoreTable[2].score = PlayerPrefs.GetInt("Slot3_score");
        scoreTable[3].score = PlayerPrefs.GetInt("Slot4_score");


    }


    public class scoreKeeper
    {
        public string name;
        public int score;

        public scoreKeeper(string Name, int Score)
        {
            name = Name;
            score = Score;

        }
    }

}
