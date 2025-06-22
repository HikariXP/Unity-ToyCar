/*
 * Author: CharSui
 * Created On: 2024.12.08
 * Description: 场景快速跳转工具
 * TODO：可以考虑做成SO配置场景来做快速跳转的
 */
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// 一个快速的场景跳转Editor工具(相位裂缝)
/// </summary>
#if UNITY_EDITOR
public class QuickSceneNavEditor
{
    [MenuItem("CharSuiTool/ForceSceneJump/Initialize &1", false, 101)]
    static void SceneJump_Initizalize()
    {
        EditorSceneManager.OpenScene("Assets/ProjectAssets/Scenes/Initialize.unity");
    }

    [MenuItem("CharSuiTool/ForceSceneJump/Title &2", false, 102)]
    static void SceneJump_Title()
    {
        EditorSceneManager.OpenScene("Assets/ProjectAssets/Scenes/Title.unity");
    }

    [MenuItem("CharSuiTool/ForceSceneJump/Lobby &3", false, 103)]
    static void SceneJump_Lobby()
    {
        EditorSceneManager.OpenScene("Assets/ProjectAssets/Scenes/Lobby.unity");
    }

    [MenuItem("CharSuiTool/ForceSceneJump/Survival &4", false, 104)]
    static void SceneJump_Survival()
    {
        EditorSceneManager.OpenScene("Assets/ProjectAssets/Scenes/Survival.unity");
    }
}
#endif