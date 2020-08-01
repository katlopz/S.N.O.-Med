using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float respawnDelay;
    public PlayerController gamePlayer;
    public int coins;
    //public Text coinText;

    public Text icons;
   
    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = FindObjectOfType<PlayerController>();
        //coinText.text = "Coins: " + coins;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Restart();
        }
    }

    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    public IEnumerator RespawnCoroutine()
    {
        gamePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        gamePlayer.transform.position = gamePlayer.respawnPoint;
        gamePlayer.gameObject.SetActive(true);
    }

    public void Restart()
    {
        StartCoroutine("RestartCoroutine");
    }

    public IEnumerator RestartCoroutine()
    {
        gamePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        gamePlayer.transform.position = gamePlayer.restartPoint;
        gamePlayer.gameObject.SetActive(true);
    }

    public void AddCoins(int numberOfCoins)
    {
        coins += numberOfCoins;
        //coinText.text = "Coins: " + coins;
    }
}
