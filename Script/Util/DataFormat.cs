using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFormat 
{
    string PlayerDataFormat = @"
<PlayerData>
	<Data MaxHP =""{1}"" HP =""{2}"" speed =""5"" jumpPower =""5""/>
	<Position x =""{3}"" y =""{4}""/>
	<bool isModify =""true""/>
	{}
</PlayerData>";
	public static string DataSaveFormat = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<{0}>
	{1}
</{0}>
";
	public static string PositionFormat = @"<Position x = ""{0}"" y ""{1}"">";
}
