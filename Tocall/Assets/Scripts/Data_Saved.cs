using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data_SavedLocal
{
    public static object INIT_TIMES = 0;
    public static object LEVEL = 1;
    public static object RESET_TIMES = 0;
    public static object USER_NAME = "User";


    public static bool Exist() => PlayerPrefs.HasKey(nameof(INIT_TIMES));

    public static void SaveAll()
    {
        var _type = typeof(Data_SavedLocal);
        var _fields = _type.GetFields();
        foreach (var f in _fields)
        {
            var _name = f.Name;
            var _value = f.GetValue(null);

            switch (_value)
            {
                case int _int:
                    Debug.Log($"int {_name} = {_int}");
                    PlayerPrefs.SetInt(_name, _int);
                    break;
                case float _float:
                    Debug.Log($"float {_name} = {_float}");
                    PlayerPrefs.SetFloat(_name, _float);
                    break;
                case string _string:
                    Debug.Log($"string {_name} = {_string}");
                    PlayerPrefs.SetString(_name, _string);
                    break;
                default:
                    Debug.LogError($"Ignorado al crear {_name} = {_value}");
                    break;
            }
        }
    }

    public static void Save(string _name, object value)
    {
        if (PlayerPrefs.HasKey(_name))
        {
            Debug.Log($"Guardando {_name} con {value}");
        }
        else
        {
            Debug.Log($"Creando {_name} con {value}");
        }
        switch (value)
        {
            case int _int:
                PlayerPrefs.SetInt(_name, _int);
                break;
            case float _float:
                Debug.Log($"float {_name} = {_float}");
                PlayerPrefs.SetFloat(_name, _float);
                break;
            case string _string:
                Debug.Log($"string {_name} = {_string}");
                PlayerPrefs.SetString(_name, _string);
                break;
            default:
                Debug.LogError($"Ignorado al crear {_name} = {value}");
                break;
        }
    }


    public static void DeleteAll()
    {
        var _type = typeof(Data_SavedLocal);
        var _fields = _type.GetFields();
        foreach (var f in _fields)
        {
            var _name = f.Name;


            if (PlayerPrefs.HasKey(_name))
            {
                Debug.Log($"DELETE {_name}");
                PlayerPrefs.DeleteKey(_name);
            }
            else
            {
                Debug.Log($"FAIL DELETE {_name}. 404");
            }
        }
    }
}
