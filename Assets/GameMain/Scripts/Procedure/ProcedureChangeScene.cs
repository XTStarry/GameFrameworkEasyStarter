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
    /// 菜单流程
    /// </summary>
    public partial class ProcedureChangeScene : ProcedureBase
    {
        private int m_LoadingFormLogicId;

        private bool isLoadingOpen = false;

        // 游戏初始化时执行。
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

            //启用事件组件
            EventComponent eventComponent = GameEntry.GetComponent<EventComponent>();
            eventComponent.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        }

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
            


        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // 加载框架UI组件
            UIComponent UI_LoadingObject = GameEntry.GetComponent<UIComponent>();
            if(!isLoadingOpen)
            {
                // 加载UI
                m_LoadingFormLogicId = UI_LoadingObject.OpenUIForm("Assets/GameMain/UI/Prefabs/UI_Loading.prefab", "Loading");
                isLoadingOpen = true;
            }
            

            string nextSceneName = procedureOwner.GetData<VarString>("NextSceneName").Value;
            Debug.Log(nextSceneName);
            switch (nextSceneName)
            {
                case "MainMenu":
                    ChangeState<ProcedureMenu>(procedureOwner);
                    break;
            }

        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);


        }


        // UIForm打开成功事件回调函数
        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            Debug.Log("Loading界面加载成功");
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;

            // 判断userData是否为自己
            if (ne.UserData != this)
            {
                return;
            }
            
            if (ne.UIForm.Logic as LoadingFormLogic)
            {
                
            }

        }

    }

}
