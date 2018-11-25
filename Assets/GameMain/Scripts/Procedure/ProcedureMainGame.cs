//------------------------------------------------------------
// Game Framerwork Easy Starter
// Powered By Game Framework v3.x
// Copyright © 2017-2018 Gao Xiaotian. All rights reserved.
// Homepage: http://www.xtstarry.top/
// Feedback: mailto:xtstarry@qq.com
//------------------------------------------------------------

using System;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameMain
{
    /// <summary>
    /// 主游戏流程类
    /// </summary>
    public partial class ProcedureMainGame : ProcedureBase
    {
        // 游戏初始化时执行。
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);


        }

        // 每次进入这个流程时执行。
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);


            //关闭所有场景
            SceneComponent scene = GameEntry.GetComponent<SceneComponent>();
            string[] loadedSceneAssetNames = scene.GetLoadedSceneAssetNames();
            for (int i = 0; i < loadedSceneAssetNames.Length; i++)
            {
                scene.UnloadScene(loadedSceneAssetNames[i]);
            }


            // 加载MainGame场景
            scene.LoadScene("MainMenu", this);

            // 加载框架UI组件
            UIComponent UI_LoadingObject = GameEntry.GetComponent<UIComponent>();
            UI_LoadingObject.OpenUIForm("Assets/GameMain/UI/Prefabs/UI_MainMenu.prefab", "Menu", 1, this);

        }
    }
}
