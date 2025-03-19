using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*public GameManager c_current;
    protected static PlayerData s_playerStoreData = null;
    private void Awake()
    {
        s_current = this;
    }

    public void SavePlayerData()
    {
        var playerStats = FindObjectOfType<PlayerStats>();

        s_playerStoreData = new PlayerData(playerStats.lifes);
    }
    void Start()
    {
        var sanchoStats = FindObjectOfType<PlayerStats>();

        if(s_playerStoreData != null)
        {
            PlayerStats.RestoreStats(s_playerStoreData.lifes);
        }
    }
    public void OnPlayerDeath(GameObject playerObject)
    {
        Destroy (playerObject);

        Invoke("Respawn", 1f);
    }

    protected void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
}
