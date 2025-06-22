/*
 * Author: CharSui
 * Created On: 2024.12.08
 * Description: 管理所有的组件的初始化
 * 所有的场景都有一个激活器，激活后按步骤启动场景，不依赖Unity默认的Update，去做到可控顺序初始化。
 */

using System;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    private void Awake()
    {
        // 游戏配置
        Application.targetFrameRate = 240;
        
        // 游戏存档

        
        // 游戏多语言
        
        
        // 初始化完成之后跳转到Title
    }
}
