﻿//------------------------------------------------------------
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
    /// loading界面逻辑类
    /// </summary>
    public class LoadingFormLogic : UIFormLogic
    {

        private ProcedureChangeScene m_ProcedureChangeScene;
        protected LoadingFormLogic() { }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            // 打开UI的时候我们把ProcedureChangeScene作为参数传递了进去，在这里OnOpen事件会把它传递过来
            m_ProcedureChangeScene = (ProcedureChangeScene)userData;
            if (m_ProcedureChangeScene == null)
            {
                return;
            }
        }
    }

}


