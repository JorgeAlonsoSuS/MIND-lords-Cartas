using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Deck
{
public class UI : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Se ha salido del juego.");
    }
        
}
}