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
        if (Data_SavedLocal.Exist) Data_SavedLocal.LoadAll();
        else Data_SavedLocal.SaveAll();

        //Añadimos 1 más a los guardados
        Data_SavedLocal.Save(nameof(Data_SavedLocal.INIT_TIMES), Data_SavedLocal.INIT_TIMES + 1);
        yield return null;

        //GO TO LEVEL
        SceneManager.LoadScene(Data_SavedLocal.LEVEL);
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
