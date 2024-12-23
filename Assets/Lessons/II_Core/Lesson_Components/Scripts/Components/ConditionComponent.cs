using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class ConditionComponent : MonoBehaviour
    {
        protected CompositeCondition CompositeCondition = new();
        
        public void AddCondition(Func<bool> condition)
        {
            CompositeCondition.AddCondition(condition);
        }
    }
}