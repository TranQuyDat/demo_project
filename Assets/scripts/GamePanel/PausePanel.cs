using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : IState
{
    public Uigame uiGame;
    Vector3 oldPosSelectMap;
    bool isOpentSelectMap = false;
    int idSceneSelect = 0;
    GameManager gameManager;
    public PausePanel() 
    {
        this.uiGame = GameManager.instance.uiGame; ;
        this.oldPosSelectMap = uiGame.ui_selectMap.anchoredPosition;
    }
    public void Enter()
    {
        gameManager = GameManager.instance;
        uiGame.panelType = PanelType.pause;
        uiGame.ui_PausePanel.SetActive(true);
        gameManager.statGame.isStart = false;
        gameManager.playerCtrl.rb.gravityScale = 0f;
        gameManager.playerCtrl.rb.velocity = Vector2.zero;
    }

    public void Execute()
    {
        //btn openSelectMap
        if(gameManager.getBtnClked() == ButtonTyle.openSelectMap)
        {
            Vector3 pos = new Vector3(oldPosSelectMap.x * -1, oldPosSelectMap.y,oldPosSelectMap.z);
            
            uiGame.ui_selectMap.anchoredPosition =
                Vector3.MoveTowards(uiGame.ui_selectMap.anchoredPosition, pos, 1000 * Time.deltaTime);

            if(uiGame.ui_selectMap.anchoredPosition.x == -oldPosSelectMap.x)
            {
                gameManager.onClick(0);
                oldPosSelectMap.x = -oldPosSelectMap.x;

                isOpentSelectMap = (!isOpentSelectMap) 
                    ? true 
                    : false;

                uiGame.btn_OpentSelectMap.transform.localScale = (isOpentSelectMap)
                    ? new Vector3(1,1,1)
                    : new Vector3(-1, 1, 1);
                
            }
        }
        //btn select Map
        if (gameManager.getBtnClked() == ButtonTyle.selectMap)
        {
            gameManager.onClick(0);
            if (idSceneSelect == gameManager.setting.idScene) return;
            gameManager.changScene(gameManager.dataGame.allMap[idSceneSelect].sceneType);
        }
        //btn arowlefp or arowright
        btn_left_right(gameManager.getBtnClked());
        
        //dk chuyen sang play
        if (Input.GetKeyDown(KeyCode.Escape) || gameManager.getBtnClked() == ButtonTyle.resume)
        {
            gameManager.stateManager.changeState(new PlayPanel());
        }

        // dk quit to menu
        if(gameManager.getBtnClked() == ButtonTyle.quit)
        {
            gameManager.onClick(0);
            Application.Quit();
        }

    }

    public void btn_left_right(ButtonTyle btn)
    {
        if (btn != ButtonTyle.arowLeft && btn != ButtonTyle.arowRight) return;
        gameManager.onClick(0);
        idSceneSelect += ((btn == ButtonTyle.arowLeft) ? -1 : 1);
        idSceneSelect = (idSceneSelect + gameManager.dataGame.allMap.Length)
            % gameManager.dataGame.allMap.Length;
        uiGame.txt_selectMap.text ="Select "+ gameManager.dataGame.allMap[idSceneSelect].sceneType;
        uiGame.img_fishSelectMap.sprite = gameManager.dataGame.allMap[idSceneSelect].spriteFish;
    }

    public void Exit()
    {
        gameManager.onClick(0);
        gameManager.statGame.isStart = true;
        uiGame.ui_selectMap.anchoredPosition = new Vector2(Mathf.Abs(oldPosSelectMap.x)
            ,oldPosSelectMap.y);
        uiGame.btn_OpentSelectMap.transform.localScale = new Vector3(-1, 1, 1);
        uiGame.ui_PausePanel.SetActive(false);

        gameManager.playerCtrl.rb.gravityScale = 1f;
    }
}
