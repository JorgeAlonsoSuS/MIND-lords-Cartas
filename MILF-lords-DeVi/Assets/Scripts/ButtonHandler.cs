using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    GameObject boton;
    private void Awake()
    {
        boton.SetActive(false);
    }
    public void EnableButton(GameObject button)
    {
        button.SetActive(true);
    }
}
