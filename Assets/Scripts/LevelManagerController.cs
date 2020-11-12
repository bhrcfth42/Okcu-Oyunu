using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerController : MonoBehaviour
{
    public Text scoreValueText;
    public void nextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void closePanel(string parentName)
    {
        GameObject.Find(parentName).SetActive(false);
    }
    public void addScore(int score)
    {
        int scoreValue = int.Parse(scoreValueText.text);
        scoreValue += score;
        scoreValueText.text = scoreValue.ToString();
    }
}
