using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    [CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/MonsterScriptableObject", order = 1)]
    public class MonsterDC : ScriptableObject
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private string nombre;
        [SerializeField]
        private GameObject monster;
        [SerializeField]
        private int initialHealth = 10;
        [SerializeField]
        private int baseDamage = 5;

        public int Id => id;

        public int GetBaseDamage()
        {
            return baseDamage;
        }
        public int GetInitialHealth()
        {
            return initialHealth;
        }
        public string GetNombre()
        {
            return nombre;
        }
        public GameObject GetPrefab()
        {
            return monster;
        }
    }
}