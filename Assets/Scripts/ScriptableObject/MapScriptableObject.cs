/*
 * Author: CharSui
 * Created On: 2025.01.12
 * Description: 定义地图数据
 */
using UnityEngine;

public class MapScriptableObject : ScriptableObject
{
    /// <summary>
    /// 地图名字（简中用于给开发者识别）
    /// </summary>
    public string mapName;
    
    /// <summary>
    /// 地图资源名
    /// </summary>
    public string prefabAssetName;
}
