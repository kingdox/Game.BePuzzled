using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Varaibles
    private int destroyed_goals = 0;
    private int playable_click_count = 0;

    [Header("Game Settings")]
    [Space]
    [SerializeField, Min(0)] private int max_playable_click = 0; // 0 == infinite

    [Header("Enabler | Disabler")]
    [Space]
    [SerializeField] private GameObject[] objs_enable_when_win = default;
    [SerializeField] private GameObject[] objs_disable_when_win = default;

    [Header("DEBUG")]
    [Space]
    [SerializeField] private int current_goals = 0;
    [SerializeField] private int current_playables = 0;

    public static ActionSuscriber<Vector2Int> OnCurrentPlayables = new ActionSuscriber<Vector2Int>(Vector2Int.zero);
    #endregion
    #region Events
    private void Awake()
    {
        Data_SavedLocal.Save(nameof(Data_SavedLocal.LEVEL), gameObject.scene.buildIndex);
        Application.targetFrameRate = 60;
        Invoke_PlayablesStatus();
    }
    private void Start()
    {
        Toggle_ObjectsWhenWin(false);
    }
    private void OnEnable() => OnSubscribe(true);
    private void OnDisable() => OnSubscribe(false);
    #endregion
    #region Private Reactions
    private void OnSubscribe(bool condition)
    {
        // Playable
        condition.Subscribe(ref Playable.OnMouseClick, OnPlayableClick);
        condition.Subscribe(ref Playable.OnCreated, OnCreated_Playable);

        // Goal
        condition.Subscribe(ref Goal.OnCollision, OnGoalInteraction);
        condition.Subscribe(ref Goal.OnTrigger, OnGoalInteraction);
        condition.Subscribe(ref Goal.OnCreated, OnCreated_Goal);
        condition.Subscribe(ref Goal.OnDestroyGoal, OnCreated_DestroyGoal);

        // UI_GameMenu_Reset
        condition.Subscribe(ref UI_GameMenu_Reset.OnClick, OnReset);

        // UI_GameMenu_Achievements
        condition.Subscribe(ref UI_GameMenu_Achievements.OnClick, OnAchievements);

        // UI_GameMenu_Review
        condition.Subscribe(ref UI_GameMenu_Review.OnClick, OnReview);

        // UI_GameMenu_Audio
        condition.Subscribe(ref UI_GameMenu_Audio.OnClick, OnAudio);

        // UI_GameMenu_Web
        condition.Subscribe(ref UI_GameMenu_Web.OnClick, OnWeb);

        // UI_GameWin_Next
        condition.Subscribe(ref UI_GameWin_Next.OnClick, OnNextLevel);
    }

    private void OnCreated_Playable(Playable playable) => current_playables++;

    private void OnCreated_Goal(Goal goal) => current_goals++;

    private void OnCreated_DestroyGoal(Goal goal)
    {
        destroyed_goals++;
        if (destroyed_goals.Equals(current_goals)) Toggle_ObjectsWhenWin(true);
    }

    private void OnPlayableClick(Playable playable)
    {
        if (max_playable_click == 0 || max_playable_click > playable_click_count)
        {
            playable_click_count++;
            Instantiate(playable.gameObject, transform.parent);
            Invoke_PlayablesStatus();
        }
    }

    private void OnGoalInteraction((Goal goal, Collision collision) data)
    {
        if (data.collision.gameObject.TryGetComponent(out Playable _playable))
        {
            Destroy(data.goal.gameObject);
        }
    }
    private void OnGoalInteraction((Goal goal, Collider other) data)
    {
        if (data.other.gameObject.TryGetComponent(out Playable _playable))
        {
            Destroy(data.goal.gameObject);
        }
    }

    private void OnAchievements()
    {
        Debug.Log("Achievements");
    }

    private void OnReview()
    {
        Debug.Log("Review");
    }

    private void OnAudio()
    {
        Debug.Log("Audio");
    }

    private void OnWeb()
    {
        Debug.Log("Web");
		Application.OpenURL("https://arpaxavier.itch.io/");
    }

    private void OnReset()
    {
        Data_SavedLocal.Save(nameof(Data_SavedLocal.RESET_TIMES), Data_SavedLocal.RESET_TIMES + 1);
        SceneManager.LoadScene(gameObject.scene.buildIndex);
    }

    private void OnNextLevel()
    {
        Data_SavedLocal.Save(nameof(Data_SavedLocal.LEVEL), gameObject.scene.buildIndex + 1);
        SceneManager.LoadScene(gameObject.scene.buildIndex + 1);
    }
    #endregion
    #region Methods
    private void Toggle_ObjectsWhenWin(bool condition)
    {
        foreach (var item in objs_enable_when_win)
        {
            if (item != null) item.SetActive(condition);
            
        }
        foreach (var item in objs_disable_when_win)
        {
            if (item != null) item.SetActive(!condition);
        }
    }
    private void Invoke_PlayablesStatus() => OnCurrentPlayables?.Invoke(new Vector2Int(playable_click_count, max_playable_click));
    #endregion
}
