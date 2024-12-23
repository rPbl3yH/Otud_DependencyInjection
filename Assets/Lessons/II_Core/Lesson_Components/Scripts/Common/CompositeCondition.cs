using System;
using System.Collections.Generic;

namespace Lessons.Lesson_Components
{
    public class CompositeCondition
    {
        private List<Func<bool>> _conditions = new();

        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _conditions.Remove(condition);
        }

        public bool IsTrue()
        {
            for (int i = _conditions.Count - 1; i >= 0; i--)
            {
                var condition = _conditions[i];

                if (condition.Invoke() == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}