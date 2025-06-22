/*
 * Author: CharSui
 * Created On: 2024.12.03
 * Description: 无参事件
 */

using System;

public sealed class NoArgEvent
{
    private event Action _Callback;

    public void Notify()
    {
        _Callback?.Invoke();
    }

    public NoArgEvent Register(Action registerAction)
    {
        if (registerAction == null)
        {
            return null;
        }

        _Callback += registerAction;
        return this;
    }
    
    public NoArgEvent Unregister(Action callBack)
    {
        _Callback -= callBack;
        return this;
    }
}
