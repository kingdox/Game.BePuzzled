using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootsrap : MonoBehaviour
{
    #region Variables
    #endregion
    #region Events
    private IEnumerator Start()
    {

        //### SAVED DATA
        yield return null;

        //PlayerPrefs.DeleteKey("");
        //PlayerPrefs.SetFloat()


        //GO TO LEVEL
        //SceneManager.LoadScene(gameObject.scene.buildIndex + 1);
    }
    #endregion
    #region __DEBUG
    [ContextMenu("Remove Data")]
    private void Remove_Data()
    {
        Data_SavedLocal.DeleteAll();
    }
    [ContextMenu("Save Data")]
    private void Save_Data()
    {
        Data_SavedLocal.SaveAll();
    }
    #endregion
}
