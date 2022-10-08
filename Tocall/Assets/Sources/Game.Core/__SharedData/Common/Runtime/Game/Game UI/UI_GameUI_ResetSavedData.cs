using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_GameUI_ResetSavedData : MonoBehaviour
{
    private Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    private void OnEnable()
    {
        btn.onClick.AddListener(PressReset);
    }
    private void OnDisable()
    {
        btn.onClick.RemoveListener(PressReset);
    }


    private void PressReset()
    {
        Debug.Log("Reseting game ");
        Data_SavedLocal.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
