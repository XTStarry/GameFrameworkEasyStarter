//------------------------------------------------------------
// Game Framerwork Easy Starter
// Powered By Game Framework v3.x
// Copyright © 2017-2018 Gao Xiaotian. All rights reserved.
// Homepage: http://www.xtstarry.top/
// Feedback: mailto:xtstarry@qq.com
//------------------------------------------------------------
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class MainGameFormLogic : UIFormLogic
    {
        private ProcedureMainGame m_ProcedureMainGame;
        protected MainGameFormLogic() { }

        protected override void OnOpen(object userData)
        {
            // 打开UI的时候我们把ProcedureMainGame作为参数传递了进去，在这里OnOpen事件会把它传递过来
            m_ProcedureMainGame = (ProcedureMainGame)userData;
            if (m_ProcedureMainGame == null)
            {
                return;
            }
        }

        /// <summary>
        /// 按下返回菜单按钮时
        /// </summary>
        public void OnBackMenuClick()
        {
            m_ProcedureMainGame.IsBackMenu();
        }
    }
}

