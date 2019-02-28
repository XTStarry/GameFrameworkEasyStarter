using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.Fsm;
using UnityGameFramework;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class PlayerLogic : EntityLogic
    {
        /// <summary>
        /// Player有限状态机接口
        /// </summary>
        private GameFramework.Fsm.IFsm<PlayerLogic> m_PlayerFsm;

        /// <summary>
        /// 玩家移动速度
        /// </summary>
        public float PlayerVelocity = 10f;

        /// <summary>
        /// 玩家跳跃力度
        /// </summary>
        public float PlayerJumpForce = 500f;

        private Rigidbody2D rb2D;

        /// <summary>
        /// 判断是否跳跃中
        /// </summary>
        public bool IsJumping;

        protected PlayerLogic() { }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            rb2D = GetComponent<Rigidbody2D>();



        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            Debug.Log("Player已加载");



            // Player状态类
            FsmState<PlayerLogic>[] playerStates = new FsmState<PlayerLogic>[]
            {
                new PlayerIdleState(),
                new PlayerWalkState()
            };

            // 创建状态机
            m_PlayerFsm = GameEntry.Fsm.CreateFsm<PlayerLogic>(this, playerStates);

            // 启动站立状态
            m_PlayerFsm.Start<PlayerIdleState>();
        }

        /// <summary>
        /// 实体隐藏。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected override void OnHide(object userData)
        {

            // 离开时销毁状态机
            GameEntry.Fsm.DestroyFsm<PlayerLogic>();

        }

        /// <summary>
        /// 向前移动
        /// </summary>
        /// <param name="distance"></param>
        public void Forward(float distance)
        {
            transform.position += transform.right * distance * PlayerVelocity;
        }

        /// <summary>
        /// 跳跃
        /// </summary>
        public void Jump()
        {
            rb2D.AddForce(transform.up * PlayerJumpForce);
        }


        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Platform")
            {
                IsJumping = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Platform")
            {
                IsJumping = false;
            }
        }
    }
}

