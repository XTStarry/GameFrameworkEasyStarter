//------------------------------------------------------------
// Game Framerwork Easy Starter
// Powered By Game Framework v3.x
// Copyright © 2017-2018 Gao Xiaotian. All rights reserved.
// Homepage: http://www.xtstarry.top/
// Feedback: mailto:xtstarry@qq.com
//------------------------------------------------------------
using GameFramework.Localization;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;
using GameFramework.Event;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;


namespace GameMain
{
    /// <summary>
    /// 菜单流程类
    /// </summary>
    public class ProcedureMenu : ProcedureBase
    {
        private ProcedureOwner m_ProcedureOwner;

        // 游戏初始化时执行。
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
            //启用事件组件
            EventComponent eventComponent = GameEntry.GetComponent<EventComponent>();
            // 启用OpenUIFormSuccess
            eventComponent.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

        }

        // 每次进入这个流程时执行。
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_ProcedureOwner = procedureOwner;


            


            //关闭所有场景
            SceneComponent scene = GameEntry.GetComponent<SceneComponent>();
            string[] loadedSceneAssetNames = scene.GetLoadedSceneAssetNames();
            for (int i = 0; i < loadedSceneAssetNames.Length; i++)
            {
                scene.UnloadScene(loadedSceneAssetNames[i]);
            }

            
            // 加载MainMenu场景
            scene.LoadScene("MainMenu", this);

            // 加载框架UI组件
            UIComponent UI_LoadingObject = GameEntry.GetComponent<UIComponent>();
            UI_LoadingObject.OpenUIForm("Assets/GameMain/UI/Prefabs/UI_MainMenu.prefab", "Menu", 1, this);

        }



        // 每次轮询执行。
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        // 每次离开这个流程时执行。
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            // 启用事件组件
            EventComponent eventComponent = GameEntry.GetComponent<EventComponent>();
            // 关闭OpenUIFormSucess
            eventComponent.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        }

        // 游戏退出时执行。
        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }


        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
           
            // 判断userData是否为自己，需要在使用OpenUIForm时使用UserData
            if (ne.UserData != this)
            {
                return;
            }
            
            // 判断是否加载的是MainMenu界面
            if (ne.UIForm.Logic as MainMenuFormLogic)
            {
                Debug.Log("MainMenu界面加载成功");

                // 根据之前保存的Loading的编号，将其关闭
                UIComponent uIComponent = GameEntry.GetComponent<UIComponent>();
                uIComponent.CloseUIForm(m_ProcedureOwner.GetData<VarInt>("LoadingSerialId").Value);
                m_ProcedureOwner.SetData<VarBool>("IsLoadingOpen", false);

            }
        }
        
    }
}

