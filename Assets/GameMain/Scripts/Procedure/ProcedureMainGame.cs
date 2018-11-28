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
        ProcedureOwner m_procedureOwner;
        // 游戏初始化时执行。
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        // 每次进入这个流程时执行。
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_procedureOwner = procedureOwner;
            // 启用事件组件
            EventComponent eventComponent = GameEntry.GetComponent<EventComponent>();
            // 启用OpenUIFormSuccess
            eventComponent.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            // 加载MainGame场景
            SceneComponent scene = GameEntry.GetComponent<SceneComponent>();
            scene.LoadScene("Assets/GameMain/Scenes/MainGame.unity", this);

            UIComponent UI_LoadingObject = GameEntry.GetComponent<UIComponent>();
            UI_LoadingObject.OpenUIForm("Assets/GameMain/UI/Prefabs/UI_MainGame.prefab", "Menu", 1, this);

            
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
            if(ne.UserData!=this)
            {
                return;
            }
            if (ne.UIForm.Logic as MainGameFormLogic)
            {
                UIComponent UI_LoadingObject = GameEntry.GetComponent<UIComponent>();
                UI_LoadingObject.CloseUIForm(m_procedureOwner.GetData<VarInt>("LoadingSerialId"));
                m_procedureOwner.SetData<VarBool>("IsLoadingOpen", false);
            }
        }
        /// <summary>
        /// 返回主菜单
        /// </summary>
        public void IsBackMenu()
        {
            m_procedureOwner.SetData<VarString>("NextSceneName", "MainMenu");

            ChangeState<ProcedureChangeScene>(m_procedureOwner);
        }
    }
}
