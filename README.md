# GameFrameworkEasyStarter

## 概述

Game Framework Easy Starter是一个适合新手、基于Game Framework的游戏开发模板。

Game Framework 是一个基于 Unity 5.3+ 引擎的游戏框架，主要对游戏开发过程中常用模块进行了封装，很大程度地规范开发过程、加快开发速度并保证产品质量。

本模板根据GF官方demo—[EllanJiang / StarForce](https://github.com/EllanJiang/StarForce)精简得到，移除了大量封装和功能（包括配置文件、网络部分等等），并做了大量修改，补充注释使得更适合新人上手。

原版地址：https://github.com/EllanJiang/GameFramework



## 入门

在使用本模板前，希望你能够阅读笨木头的系列教程：

[[GameFramework\]Demo1-如何创建一个GameFramework项目](http://www.benmutou.com/archives/2535)

[[GameFramework\]Demo2-切换流程和场景](http://www.benmutou.com/archives/2548)

[[GameFramework\]Demo3-加载UI](http://www.benmutou.com/archives/2564)

[[GameFramework\]Demo4-内置事件订阅](http://www.benmutou.com/archives/2571)

[[GameFramework\]Demo5-加载配置文件](http://www.benmutou.com/archives/2579)

[[GameFramework\]Demo6-创建实体](http://www.benmutou.com/archives/2587)

[[GameFramework\]Demo7-发起Web请求](http://www.benmutou.com/archives/2603)

[[GameFramework\]Demo8-NetWork网络连接、发送、接收数据](http://www.benmutou.com/archives/2630)

[[GameFramework\]Demo9-AssetBundle打包的一些坑](http://www.benmutou.com/archives/2615)

[[GameFramework\]Demo10-有限状态机的使用](http://www.benmutou.com/archives/2643)



## 示例

### 流程示例：

```c#
using System;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
/// <summary>
/// 启动流程类
/// </summary>
public class ProcedureLaunch : ProcedureBase
{

    // 游戏初始化时执行。
    protected override void OnInit(ProcedureOwner procedureOwner)
    {
        base.OnInit(procedureOwner);
    }

    // 每次进入这个流程时执行。
    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
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
    }

    // 游戏退出时执行。
    protected override void OnDestroy(ProcedureOwner procedureOwner)
    {
        base.OnDestroy(procedureOwner);
    }

}
```
### 加载场景：

```c#
SceneComponent scene = GameEntry.GetComponent<SceneComponent>();
scene.LoadScene("Assets/GameMain/Scenes/MainMenu.unity", this);
```

### 加载UI：

```c#
UIComponent UI_LoadingObject = GameEntry.GetComponent<UIComponent>();
UI_LoadingObject.OpenUIForm("Assets/GameMain/UI/Prefabs/UI_MainMenu.prefab", "Menu", 1, this);
```

### 事件组件：

```c#
// 启用事件组件
EventComponent eventComponent = GameEntry.GetComponent<EventComponent>();
// 启用OpenUIFormSuccess和OnLoadSceneSucess
eventComponent.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
eventComponent.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSucess);
```

### 事件回调函数示例：

```C#
private void OnLoadSceneSucess(object sender, GameEventArgs e)
{
	// 启用事件组件并订阅相应事件后，每次达到条件都会执行一次相应事件回调函数
	Debug.Log("成功加载了一个场景");
}
```

### UI逻辑示例：

```c#
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
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
```