using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_GoalManager : MonoBehaviour
{
    [SerializeField] private GoalManager goalManager;
    [SerializeField] private ItemInventorySO itemsInventory;
    [SerializeField] private RectTransform goalContainer;
    [SerializeField] private UI_GoalElement goalElement;

    private Dictionary<ItemType, UI_GoalElement> goalElements;

    void Awake()
    {
        goalManager.OnGoalDefined += InitializeGoal;
        goalManager.OnGoalUpdated += UpdateGoal;
        goalElements = new Dictionary<ItemType, UI_GoalElement>();
    }

    private void UpdateGoal(LevelGoal goal)
    {
        goalElements[goal.ItemType].UpdateValue(goal.Count);
    }

    void OnDestroy()
    {
        goalManager.OnGoalDefined -= InitializeGoal;
        goalManager.OnGoalUpdated -= UpdateGoal;
    }

    private void InitializeGoal(List<LevelGoal> levelGoals)
    {
        while (goalContainer.childCount > 0) 
        {
            Destroy(goalContainer.GetChild(0));
        }

        goalElements.Clear();

        foreach (var levelGoal in levelGoals)
        {
            var element = Instantiate(goalElement, goalContainer);
            var itemData = itemsInventory.GetItemByType(levelGoal.ItemType);
            element.Init(itemData, levelGoal.Count);

            goalElements[levelGoal.ItemType] = element;
        }
    }
}
