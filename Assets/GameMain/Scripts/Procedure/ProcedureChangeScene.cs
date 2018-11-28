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
    /// 菜单流程类
    /// </summary>
    public partial class ProcedureChangeScene : ProcedureBase
    {
        


        private ProcedureOwner m_ProcedureOwner;


        // 游戏初始化时执行。
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

            
        }

        // 每次进入这个流程时执行。
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_ProcedureOwner = procedureOwner;

            //启用事件组件
            EventComponent eventComponent = GameEntry.GetComponent<EventComponent>();
            // 启用OpenUIFormSuccess
            eventComponent.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            //关闭所有场景
            SceneComponent scene = GameEntry.GetComponent<SceneComponent>();
            string[] loadedSceneAssetNames = scene.GetLoadedSceneAssetNames();
            for (int i = 0; i < loadedSceneAssetNames.Length; i++)
            {
                scene.UnloadScene(loadedSceneAssetNames[i]);
            }

            // 加载框架UI组件
            UIComponent UI_LoadingObject = GameEntry.GetComponent<UIComponent>();

            // 加载Loading界面
            int LoadingFormLogicId = UI_LoadingObject.OpenUIForm("Assets/GameMain/UI/Prefabs/UI_Loading.prefab", "Loading", int.MinValue, this);
            // 加入用于保存Loading界面序列编号的整型变量
            procedureOwner.SetData<VarInt>("LoadingSerialId", LoadingFormLogicId);

        }

        // 每次轮询执行。
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            bool isLoadingOpen = procedureOwner.GetData<VarBool>("IsLoadingOpen").Value;
            if(isLoadingOpen == true)
            {
                string nextSceneName = procedureOwner.GetData<VarString>("NextSceneName").Value;
                Debug.Log(nextSceneName);
                switch (nextSceneName)
                {
                    case "MainMenu":
                        
                        ChangeState<ProcedureMenu>(procedureOwner);
                        break;
                    case "MainGame":
                        ChangeState<ProcedureMainGame>(procedureOwner);
                        break;

                }
            }
            

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


        // UIForm打开成功事件回调函数
        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;

            // 判断userData是否为自己，需要在使用OpenUIForm时使用UserData
            if (ne.UserData != this)
            {
                return;
            }

            // 判断是否加载的是Loading界面
            if (ne.UIForm.Logic as LoadingFormLogic)
            {
                Debug.Log("Loading界面加载成功");
                m_ProcedureOwner.SetData<VarBool>("IsLoadingOpen", true);
            }

        }

    }

}
