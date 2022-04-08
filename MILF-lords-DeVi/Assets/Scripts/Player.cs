using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private List<MonsterDC> gameMonster;
        [SerializeField]
        private int cardsToDraft;
        [SerializeField]
        private GameObject card;

        private List<MonsterDC> invocations;
        private List<int> cardsSelected = new List<int>();
        Card carta;
        void Awake()
        {
            invocations = new List<MonsterDC>();
            if (cardsToDraft > gameMonster.Count) cardsToDraft = gameMonster.Count;
            cardDraft();
        }

        
        public void cardDraft()
        {
            for(int i = 0; i<cardsToDraft;)
            {
                int num =Random.Range(0, gameMonster.Count);
                if (CanAddCard(num))
                {
                    AddCarta(num);
                    cardsSelected.Add(num);
                    i++;
                }
            }
        }
        public bool CanAddCard(int num)
        {
            for(int i=0; i < cardsSelected.Count; i++)
            {
                if (num == cardsSelected[i]) return false;
            }
            return true;
        }
        public void AddCarta(int index)
        {
            Vector3 v = new Vector3(transform.position.x, transform.position.y + 0.29f, transform.position.z);
            Debug.Log("NUMERO MANDADO:"+index);
            carta = new Card(card, gameMonster[index], v, GetComponent<Player>()); 
        }
        public void AddMonster(MonsterDC monster)
        {
            invocations.Add(monster);
            Debug.Log("Invocations:"+invocations.Count);
        }
    }
}