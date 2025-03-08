using System.Collections.Generic;
using UnityEngine;

public class UI_GoalManager : MonoBehaviour
{
    [SerializeField] private ItemInventorySO itemsInventory;
    [SerializeField] private RectTransform goalContainer;
    [SerializeField] private UI_GoalElement goalElement;

    private GoalManager goalManager;
    private Dictionary<ItemType, UI_GoalElement> goalElements;

    void Awake()
    {
        goalElements = new Dictionary<ItemType, UI_GoalElement>();
    }

    private void OnEnable()
    {
        goalManager = ServiceLocator.Get<GoalManager>();
        goalManager.OnGoalDefined += InitializeGoal;
        goalManager.OnGoalUpdated += UpdateGoal;
    }

    void OnDisable()
    {
        goalManager.OnGoalDefined -= InitializeGoal;
        goalManager.OnGoalUpdated -= UpdateGoal;
    }

    private void UpdateGoal(LevelGoal goal)
    {
        goalElements[goal.ItemType].UpdateValue(goal.Count);
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
