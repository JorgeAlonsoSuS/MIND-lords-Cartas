using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    [CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/MonsterScriptableObject", order = 1)]
    public class MonsterDC : ScriptableObject
    {
        [SerializeField]
        private GameObject monster;
        [SerializeField]
        private MonsterBehaviour behaviour;
        [SerializeField]
        private int initialHealth = 10;
        [SerializeField]
        private int baseDamage = 5;

    }
}

