using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Loading,
    WaitScreen,
    Playing,
    Paused,
    LevelCompleted,
    GameOver,
    NextLevelPopup,
    RetryPopup,
}

public class GameStateManager : MonoBehaviour
{
    private class StateChange
    {
        public GameState State { get; }
        public float Duration { get; }

        public StateChange(GameState state, float duration)
        {
            State = state;
            Duration = duration;
        }
    }

    public GameState CurrentState { get; private set; }

    private Queue<StateChange> stateChangeQueue = new Queue<StateChange>();

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        ServiceLocator.Get<EventManager>().OnGameStateChanged.Publish(newState);

        //Debug.Log($"GameState: {CurrentState}");
    }

    public void EnqueueStateChange(GameState newState, float duration)
    {
        stateChangeQueue.Enqueue(new StateChange(newState, duration));

        if (stateChangeQueue.Count == 1)
        {
            StartCoroutine(ProcessStateQueue());
        }
    }

    private IEnumerator ProcessStateQueue()
    {
        while (stateChangeQueue.Count > 0)
        {
            StateChange nextChange = stateChangeQueue.Dequeue();
            yield return new WaitForSeconds(nextChange.Duration);
            ChangeState(nextChange.State);
        }
    }
}
