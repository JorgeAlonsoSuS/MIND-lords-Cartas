using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    public class Player : MonoBehaviour
    {
        private List<GameObject> invocations;

        void Start()
        {
            invocations = new List<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < invocations.Count; i++)
            {
                GameObject monster = invocations[i];
            }

        }

        public void AddMonster(GameObject monster)
        {
            invocations.Add(monster);
            Debug.Log(invocations.Count);
        }
    }

}

