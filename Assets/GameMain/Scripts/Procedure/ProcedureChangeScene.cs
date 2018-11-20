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
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameMain
{
    /// <summary>
    /// 菜单流程
    /// </summary>
    public partial class ProcedureChangeScene : ProcedureBase
    {
        /// <summary>
        /// Loading界面编号
        /// </summary>
        public static int LoadingSerialId;

        private bool m_IsChangeSceneComplete = false;
        private bool m_IsLoadingFormCpmplete = false;
        private bool m_Flag = false;


        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_IsChangeSceneComplete = false;
            m_IsLoadingFormCpmplete = false;
            m_Flag = false;



//           // 卸载所有场景
//           string[] loadedSceneAssetNames = GameEntry.Scene.GetLoadedSceneAssetNames();
//           for (int i = 0; i < loadedSceneAssetNames.Length; i++)
//           {
//               GameEntry.Scene.UnloadScene(loadedSceneAssetNames[i]);
//           }

            // 加载Loading界面
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            LoadingSerialId = GameEntry.UI.OpenUIForm("Assets/GameMain/UI/Prefabs/UI_Loading.prefab", "Loading", this);





        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!m_IsChangeSceneComplete && m_IsLoadingFormCpmplete && !m_Flag)
            {
                GameEntry.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
                string m_nextSceneName = procedureOwner.GetData<VarString>("NextSceneName").Value;
                GameEntry.Scene.LoadScene(m_nextSceneName, this);
                m_Flag = true;
            }

            if (!m_IsChangeSceneComplete || !m_IsLoadingFormCpmplete)
            {
                return;
            }

            string nextSceneName = procedureOwner.GetData<VarString>("NextSceneName").Value;
            switch (nextSceneName)
            {
                case "MainMenu":
                    ChangeState<ProcedureMenu>(procedureOwner);
                    break;
                case "MainGame":
                    ChangeState<ProcedureMain>(procedureOwner);
                    break;
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            // 离开时取消事件订阅
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
        }


        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_IsChangeSceneComplete = true;
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            Log.Debug("Loading界面加载成功");

            m_IsLoadingFormCpmplete = true;

        }

    }

}
