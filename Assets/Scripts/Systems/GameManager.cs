using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverText;
    
    public void GameOver(string gameOverMessage)
    {
        Debug.Log(gameOverMessage);
    }

    public void NewGameButton()
    {
        
    }

    public void MainMenuButton()
    {
        
    }
}
