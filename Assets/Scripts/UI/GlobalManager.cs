﻿using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GlobalManager : GameSignleton<GlobalManager>
{
    [SerializeField]
    GameSettings settings;
    [SerializeField]
    string filename;

    public event Action OnExit;
    public event Action OnLoaded;

    static public GameSettings Settings
    { 
        get
        {
            if (Instance.settings == null) LoadData();
            return Instance.settings;
        }
    }

    static public void SaveData()
    {
        string json = JsonUtility.ToJson(Settings);
        File.WriteAllText(Directory.GetCurrentDirectory() + "/" + Instance.filename, json);
    }

    static public void LoadData()
    {
        if (File.Exists(Instance.filename))
        {
            string json = File.ReadAllText(Instance.filename);
            Instance.settings = JsonUtility.FromJson<GameSettings>(json);
        }
        else
        {
            Instance.settings = new GameSettings();
        }
        Instance.OnLoaded?.Invoke();
    }

    private void Start()
    {
        LoadData();
    }

    static public void ExitGame()
    {
        Instance.OnExit?.Invoke();
        SaveData();
    }
}