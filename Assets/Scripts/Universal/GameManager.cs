using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public Player player;


    // Update is called once per frame
    void Update()
    {

        player = FindObjectOfType<Player>();
        if (player == null)
        {
            PlayerIsDead();
        }
    }

    public static void PlayerIsDead()
    {
        ScoreManager scoreKeeper = FindObjectOfType<ScoreManager>();

        scoreKeeper.PlayerDied();

    }

    public void RetryLevel()
    {
        EnemySpawner.enemies.Clear();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
