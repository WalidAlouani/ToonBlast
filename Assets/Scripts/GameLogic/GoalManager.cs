using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public Action<List<LevelGoal>> OnGoalDefined;
    public Action<LevelGoal> OnGoalUpdated;
    public Action<LevelGoal> OnGoalCompleted;
    public Action OnAllGoalsCompleted;

    private List<LevelGoal> levelGoals;

    private void OnEnable()
    {
        ServiceLocator.Get<EventManager>().OnTilesDestroyed.Subscribe(UpdateGoal, this, 1);
    }

    private void OnDisable()
    {
        if (ServiceLocator.TryGet<EventManager>(out var eventManager))
            eventManager.OnTilesDestroyed.Unsubscribe(UpdateGoal);
    }

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
                    OnGoalUpdated?.Invoke(levelGoal);

                    if (levelGoal.Count == 0)
                        OnGoalCompleted?.Invoke(levelGoal);
                }
            }
        }

        if (levelGoals.All(el => el.Count <= 0))
        {
            OnAllGoalsCompleted?.Invoke();
        }
    }
}
