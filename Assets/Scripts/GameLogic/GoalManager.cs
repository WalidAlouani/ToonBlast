using System;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private List<LevelGoal> levelGoals;
    public Action<List<LevelGoal>> OnGoalDefined;
    public Action<LevelGoal> OnGoalUpdated;
    public Action OnGoalsCompleted;

    public void Init(List<LevelGoal> levelGoals)
    {
        this.levelGoals = levelGoals;
        OnGoalDefined?.Invoke(levelGoals);
    }

    public void UpdateGoal(List<TileItem> matches)
    {
        foreach (var item in matches)
        {
            foreach (var levelGoal in levelGoals)
            {
                if (levelGoal.Count <= 0)
                    continue;

                if (levelGoal.ItemType == item.ItemType || levelGoal.ItemType == ItemType.None)
                {
                    levelGoal.Count--;
                    OnGoalUpdated.Invoke(levelGoal);
                }
            }
        }
    }
}
