using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Deck {
    public class WinnerManager : MonoBehaviour
    {
        GameObject gameManager;
        GameManager trueGameManager;
        Player winner;
        string winnerName;
        [SerializeField]
        GameObject texto;
        TextMeshProUGUI textPro;

        private void Awake()
        {
            gameManager = GameObject.Find("GameManager");
            trueGameManager = gameManager.GetComponent<GameManager>();
            winner = trueGameManager.GetCurrentPlayer();
            winnerName = winner.GetPlayerName().text;
            Destroy(gameManager);
            Debug.Log("Ha ganado " + winnerName);
            textPro = texto.GetComponent<TextMeshProUGUI>();
            textPro.text = "¡Ha ganado " + winnerName + "!" + " Tremendo Chad" ;
            
        }
    }

}