using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Deck
{
    public class InterfaceManager : MonoBehaviour
    {
        public static InterfaceManager Instance { get; private set; }

        [SerializeField]
        private Button skipPlayCardPhaseButton;
        
        public Button SkipPlayCardPhaseButton => skipPlayCardPhaseButton;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            skipPlayCardPhaseButton.gameObject.SetActive(false);
        }
    }
}
