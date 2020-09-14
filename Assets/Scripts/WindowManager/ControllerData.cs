using System;
using System.Collections.Generic;


[Serializable]
public class ControllerData
{
    private Dictionary<string, object> list;

    public ControllerData()
    {
        if (list == null) list = new Dictionary<string, object>();
    }

    public ControllerData SetParam(string name, object param)
    {
        if (!list.ContainsKey(name)) list[name] = param;
        return this;
    }

    public T GetParam<T>(string name, T nullValue = default)
    {
        T result = nullValue;
        if (list.ContainsKey(name))
        {
            result = (T)list[name];
        }
        return result;
    }

    public void Remove(string key)
    {
        list.Remove(key);
    }

    public static ControllerData Build(object obj)
    {
        var signal = new ControllerData();
        var fields = obj.GetType().GetFields();
        foreach (var field in fields)
        {
            signal.SetParam(field.Name, field.GetValue(obj));
        }
        return signal;
    }

    public static T Parse<T>(ControllerData signal) where T : new()
    {
        var obj = new T();
        if (signal != null)
        {
            foreach (var p in signal.list)
            {
                obj.GetType().GetField(p.Key).SetValue(obj, p.Value);
            }
        }
        return obj;
    }
}
