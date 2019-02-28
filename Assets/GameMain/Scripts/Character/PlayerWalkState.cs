using GameFramework;
using GameFramework.Fsm;
using UnityEngine;

namespace GameMain
{
    public class PlayerWalkState : FsmState<PlayerLogic>
    {
        /// <summary>
        /// 有限状态机状态初始化时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected override void OnInit(IFsm<PlayerLogic> fsm) { }

        /// <summary>
        /// 有限状态机状态进入时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected override void OnEnter(IFsm<PlayerLogic> fsm)
        {
            Debug.Log("进入行走状态");
        }

        /// <summary>
        /// 有限状态机状态轮询时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected override void OnUpdate(IFsm<PlayerLogic> fsm, float elapseSeconds, float realElapseSeconds)
        {
            float inputHorizontal = Input.GetAxis("Horizontal");
            if (inputHorizontal != 0)
            {
                // 移动
                fsm.Owner.Forward(elapseSeconds * inputHorizontal);
            }

            if (inputHorizontal == 0)
            {
                // 站立
                ChangeState<PlayerIdleState>(fsm);
            }

            if (Input.GetKeyDown(KeyCode.Space) && fsm.Owner.IsJumping == false)
            {
                Debug.Log("jump");
                // 跳跃
                fsm.Owner.Jump();
            }

        }

        /// <summary>
        /// 有限状态机状态离开时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="isShutdown">是否是关闭有限状态机时触发。</param>
        protected override void OnLeave(IFsm<PlayerLogic> fsm, bool isShutdown) { }

        /// <summary>
        /// 有限状态机状态销毁时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected override void OnDestroy(IFsm<PlayerLogic> fsm)
        {
            base.OnDestroy(fsm);
        }

    }
}
