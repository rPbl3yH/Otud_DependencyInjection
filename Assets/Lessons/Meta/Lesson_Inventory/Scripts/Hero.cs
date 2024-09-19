using System;
using UnityEngine;

namespace Lessons.Meta.Lesson_Inventory
{
    public class Hero : MonoBehaviour
    {
        public static Hero Instance;

        public int MaxHitPoints;
        public float Mana;
        
        public void Awake()
        {
            Instance = this;
        }
    }
}