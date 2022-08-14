using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Varaibles
    private int destroyed_goals = 0;

    [Header("Game Settings")]
    [Space]
    [SerializeField] private int max_playable_instantiation = 0; // 0 == infinite

    [Header("Enabler | Disabler")]
    [Space]
    [SerializeField] private GameObject[] objs_enable_when_win = default;
    [SerializeField] private GameObject[] objs_disable_when_win = default;

    [Header("DEBUG")]
    [Space]
    [SerializeField] private int current_goals = 0;
    [SerializeField] private int current_playables = 0;
    #endregion
    #region Events
    private void Awake() => Data_SavedLocal.Save(nameof(Data_SavedLocal.LEVEL), gameObject.scene.buildIndex);
    private void Start() => Toggle_ObjectsWhenWin(false);
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
        if (max_playable_instantiation == 0 || max_playable_instantiation > current_playables)
        {
            Instantiate(playable.gameObject, transform.parent);
        }
    }

    private void OnGoalInteraction((Goal goal, Collision collision) data)
    {
        Destroy(data.goal.gameObject);
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
        SceneManager.LoadScene(gameObject.scene.buildIndex);
    }

    private void OnNextLevel()
    {
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
    
    #endregion
}
