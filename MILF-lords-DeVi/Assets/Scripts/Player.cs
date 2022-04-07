using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Player : MonoBehaviour
    {
        private List<GameObject> invocations;
        [SerializeField]
        private List<MonsterDC> gameMonster;
        [SerializeField]
        private int cardsToDraft;
        [SerializeField]
        private GameObject prefabCarta;

        private List<Card> deckCards = new List<Card>();
        private List<int> cardsSelected = new List<int>();
        void Awake()
        {
            invocations = new List<GameObject>();
            if (cardsToDraft > gameMonster.Count) cardsToDraft = gameMonster.Count;
            //Debug.Log(cardsToDraft);
            cardDraft();
            cardSpawn();
        }

        
        public void cardDraft()
        {
            for(int i = 0; i<cardsToDraft;)
            {
                int num =Random.Range(0, gameMonster.Count);
                if (CanAddCard(num))
                {
                    AddCarta(num, deckCards);
                    cardsSelected.Add(num);
                    Debug.Log(gameMonster[num].GetNombre());
                    i++;
                }
            }
        }
        public void cardSpawn()
        {
            for(int i=0; i<deckCards.Count;i++) {
                GameObject carta = Instantiate(deckCards[i].GetCard());
                Vector3 v = new Vector3(transform.position.x, transform.position.y + 0.29f, transform.position.z);
                carta.transform.position = v;
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
        public void AddCarta(int index, List<Card> deckCards)
        {
            Card carta = new Card();
            carta.SetInvocation(gameMonster[index].GetPrefab());
            Debug.Log(carta.GetMonster());
            carta.SetOwner(GetComponent<Player>());
            carta.SetPrefab(prefabCarta);
            deckCards.Add(carta);
        }
        public void AddMonster(GameObject monster)
        {
            invocations.Add(monster);
            Debug.Log("Invocations:"+invocations.Count);
        }
    }
}