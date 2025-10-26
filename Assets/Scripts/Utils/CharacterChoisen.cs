using System.Collections.Generic;
using UnityEngine;

public static class CharacterChoisen
{
    static Dictionary<string, RootContainer> path_root = new();
    public static string pathOfCharacter1 = "Humans/Human3";
    public static string pathOfCharacter2 = "Humans/Human4";
    public static RootContainer GetRootFromPath(string path)
    {
        if (!path_root.ContainsKey(path))
        {
            RootContainer root = Resources.Load<RootContainer>("ScriptableObjects/Roots/" + path);
            path_root[path] = root;
        }
        return path_root[path];
    }
}
