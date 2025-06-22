/*
 * Author: CharSui
 * Created On: 2024.12.09
 * Description: 由于在其他项目上遇到过问题，所有资源读取，场景加载，都必须以异步的形式加载。不许用同步的方式。
 * 对，你没听错，不许同步。这些流程本身就是有一个加载的流程，我就是不许你同步。
 */
using UnityEngine.SceneManagement;
using Util;

public static class SceneManager
{
    public static void LoadNextScene(string sceneName)
    {
        if(sceneName.IsNullOrEmpty()) return;
        
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }
}
