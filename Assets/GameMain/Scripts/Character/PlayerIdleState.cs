using GameFramework;
using GameFramework.Fsm;
using UnityEngine;

namespace GameMain
{
    public class PlayerIdleState : FsmState<PlayerLogic>
    {
        /// <summary>
        /// 有限状态机状态初始化时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用</param>
        protected override void OnInit(IFsm<PlayerLogic> fsm) { }

        /// <summary>
        /// 有限状态机状态进入时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用</param>
        protected override void OnEnter(IFsm<PlayerLogic> fsm)
        {
            Debug.Log("进入站立状态");
        }

        /// <summary>
        /// 有限状态机状态轮询时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected override void OnUpdate(IFsm<PlayerLogic> fsm, float elapseSeconds, float realElapseSeconds)
        {
            /* 按a、d或者左右方向键移动 */
            float inputHorizontal = Input.GetAxis("Horizontal");
            if (inputHorizontal != 0)
            {
                /* 移动 */
                ChangeState<PlayerWalkState>(fsm);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && fsm.Owner.IsJumping == false)
            {
                // 跳跃
                fsm.Owner.Jump();
            }
        }
    }

}
