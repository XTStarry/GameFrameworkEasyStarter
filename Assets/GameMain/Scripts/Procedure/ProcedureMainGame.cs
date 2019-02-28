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
        

        /// <summary>
        /// 是否在首次加载后关闭菜单
        /// </summary>
        bool isFirstCloseMenu = false;

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

            // 启用OpenUIFormSuccess
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            // 加载MainGame场景
            GameEntry.Scene.LoadScene("Assets/GameMain/Scenes/MainGame.unity", this);

            // 加载Player
            GameEntry.Entity.ShowEntity<PlayerLogic>(1, "Assets/GameMain/Entities/Characters/Prefabs/Player.prefab", "Player");

            // 加载游戏内菜单
            int menuInGameId = GameEntry.UI.OpenUIForm("Assets/GameMain/UI/Prefabs/UI_MainGame.prefab", "Menu", 1, this);
            // 加入用于保存UI_MainGame序列编号的整型变量
            procedureOwner.SetData<VarInt>("MenuInGameId", menuInGameId);

            procedureOwner.SetData<VarBool>("IsMenuInGameOpen", true);
            isFirstCloseMenu = false;

        }

        // 每次轮询执行。
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (isFirstCloseMenu == false)
            {
                MenuInGame();
                isFirstCloseMenu = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuInGame();
            }
        }

        // 每次离开这个流程时执行。
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            // 关闭OpenUIFormSucess
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        }

        // 游戏退出时执行。
        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }

        // 打开UI界面事件回调函数
        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            Debug.Log("进入OnOpenUIFormSucess");
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            if (ne.UIForm.Logic as MainGameFormLogic)
            {
           
                if (m_procedureOwner.GetData<VarBool>("IsLoadingOpen") == true)
                {
                    GameEntry.UI.CloseUIForm(m_procedureOwner.GetData<VarInt>("LoadingSerialId"));
                    m_procedureOwner.SetData<VarBool>("IsLoadingOpen", false);
                }

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

        /// <summary>
        /// 开启/关闭游戏内菜单
        /// </summary>
        void MenuInGame()
        {
            if (m_procedureOwner.GetData<VarBool>("IsMenuInGameOpen") == true)
            {
                GameEntry.UI.CloseUIForm(m_procedureOwner.GetData<VarInt>("MenuInGameId"));
                m_procedureOwner.SetData<VarBool>("IsMenuInGameOpen", false);
            }
            else if (m_procedureOwner.GetData<VarBool>("IsMenuInGameOpen") == false)
            {
                int menuInGameId = GameEntry.UI.OpenUIForm("Assets/GameMain/UI/Prefabs/UI_MainGame.prefab", "Menu", 1, this);
                // 加入用于保存UI_MainGame序列编号的整型变量
                m_procedureOwner.SetData<VarInt>("MenuInGameId", menuInGameId);
                m_procedureOwner.SetData<VarBool>("IsMenuInGameOpen", true);
            }
        }
    }
}
