using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck {
    public class WinnerManager : MonoBehaviour
    {
        GameObject gameManager;
        GameManager trueGameManager;
        Player winner;

        private void Awake()
        {
            gameManager = GameObject.Find("GameManager");
            trueGameManager = gameManager.GetComponent<GameManager>();
            winner = trueGameManager.GetCurrentPlayer();
            
        }
    }

}