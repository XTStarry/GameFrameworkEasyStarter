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

    /// <summary>
    /// 主菜单逻辑类
    /// </summary>
    public class MainMenuFormLogic : UIFormLogic
    {

        private ProcedureMenu m_ProcedureChangeScene;
        protected MainMenuFormLogic() { }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            // 打开UI的时候我们把ProcedureMenu作为参数传递了进去，在这里OnOpen事件会把它传递过来
            m_ProcedureChangeScene = (ProcedureMenu)userData;
            if (m_ProcedureChangeScene == null)
            {
                return;
            }
        }
    }
}

