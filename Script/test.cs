using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class test : MonoBehaviour
{
    public void OnClick()
    {
        string file = string.Format(playerDataFormat, Managers.DataManager.PlayerMaxHP, Managers.DataManager.PlayerHP, Managers.DataManager.Playerspeed);
        File.WriteAllText(Managers.DataManager.PlayerDataPath, file);
        Debug.Log(file);
    }

    public void OnClick2()
    {
        Managers.DataManager.PlayerMaxHP = 90;
        Managers.DataManager.PlayerHP = 65;
        Managers.DataManager.Playerspeed = 3;

        Debug.Log(Managers.DataManager.PlayerMaxHP);
        Debug.Log(Managers.DataManager.PlayerHP);
        Debug.Log(Managers.DataManager.Playerspeed);
    }
    string playerDataFormat = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<PlayerData>
	<Data MaxHP =""{0}"" HP =""{1}"" speed =""{2}""/>
</PlayerData>
";
}
