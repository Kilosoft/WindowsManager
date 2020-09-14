using System;
using System.Collections.Generic;

public class StringEnum
{
    public string value;

    private static Dictionary<Type, List<StringEnum>> allByType = new Dictionary<Type, List<StringEnum>>();

    public StringEnum(string val)
    {
        value = val;
        Type t = this.GetType();
        GetAll(t).Add(this);
    }

    public static List<StringEnum> GetAll(Type t)
    {
        if (allByType.ContainsKey(t))
        {
            return allByType[t];
        }
        else
        {
            List<StringEnum> list = new List<StringEnum>();
            allByType[t] = list;
            return list;
        }
    }

    public override string ToString()
    {
        return value;
    }
}