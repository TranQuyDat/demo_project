using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct SettingMap
{
    public float mapScale;
}
[Serializable]
public class Uigame
{
    public PanelTyle panelTyle;
    [Header("Play")]
    public GameObject ui_PlayPanel;
    public Slider slider_Evolution;
    [Header("Pause")]
    public GameObject ui_PausePanel;
    public Slider slider_Music;
    public Slider slider_Sound;
    [Header("Lose")]
    public GameObject ui_LosePanel;
    [Header("Win")]
    public GameObject ui_WinPanel;
}
[Serializable]
public class StatGame
{
    public bool isWin;
    public bool isLose;
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public StateManager stateManager;
    public EnemyManager enemyManager;
    public SettingMap setting;
    public Uigame uiGame;
    public StatGame statGame;
    public ButtonTyle displayBtnClked = ButtonTyle.none;
    private ButtonTyle btnClicked = ButtonTyle.none;
    private void Awake()
    {
        if(instance == null) instance = this;
        if(instance !=null && instance != this)
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        stateManager = new StateManager();
        stateManager.changeState(new PlayPanel());
    }
    private void Update()
    {
        stateManager.excute();
    }
    public void onClick(int id)
    {
        btnClicked = (ButtonTyle)id;
        displayBtnClked = btnClicked;
    }

    public ButtonTyle getBtnClked()
    {
        return btnClicked;
    }
}
