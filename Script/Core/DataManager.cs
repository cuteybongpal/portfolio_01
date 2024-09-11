using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static Define;

public class DataManager
{
    public Dictionary<int,MonsterInfo> MonsterInfoDict = new Dictionary<int,MonsterInfo>();
    public List<ItemInfo> ItemInfoList = new List<ItemInfo>();
    public enum GameState
    {
        Run,
        Pause,
        GameOver
    }
    GameState _state = GameState.Run;
    public GameState State
    {
        get { return _state; }
        set {
            _state = value;
            switch (_state)
            {
                case GameState.Run:
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }
        }
    }

    public float PlayerHP { get; set; }
    public int PlayerMaxHP { get; set; }
    public float Playerspeed { get; set; }
    public float PlayerJumpPower {  get; set; } 
    public Vector3 PlayerPos { get; set; }

    public string PlayerDataPath = "Assets/@Resources/Data/PlayerData.xml";


    bool isActivate = false;
    public void Init()
    {
        if (isActivate)
            return;
        ReadPlayerData();
        ReadMonsterData();
    }

    void ReadPlayerData()
    {
        TextAsset txt = Managers.Resource.Load<TextAsset>("PlayerData.data");
        XDocument xdoc = XDocument.Parse(txt.text);

        int MaxHP = int.Parse(xdoc.Root.Element("Data").Attribute("MaxHP").Value);
        float HP = float.Parse(xdoc.Root.Element("Data").Attribute("HP").Value);
        float Speed = float.Parse(xdoc.Root.Element("Data").Attribute("speed").Value);
        float JumpPower = float.Parse(xdoc.Root.Element("Data").Attribute("jumpPower").Value);
        float PlayerXPos = float.Parse(xdoc.Root.Element("Position").Attribute("x").Value);
        float PlayerYPos = float.Parse(xdoc.Root.Element("Position").Attribute("y").Value);
        bool isModified = bool.Parse(xdoc.Root.Element("bool").Attribute("isModify").Value);

        if (isModified)
            PlayerPos = new Vector3(PlayerXPos, PlayerYPos);

        PlayerHP = HP;
        PlayerMaxHP = MaxHP;
        Playerspeed = Speed;
        PlayerJumpPower = JumpPower;
    }
    void SavePlayerData()
    {

    }


    void ReadMonsterData()
    {
        TextAsset txt = Managers.Resource.Load<TextAsset>("MonsterData.data");
        XDocument xdoc = XDocument.Parse(txt.text);
        foreach (XElement xele in xdoc.Root.Elements("Monster"))
        {
            MonsterInfo _monster = new MonsterInfo(xele.Attribute("name").Value, int.Parse(xele.Attribute("MaxHP").Value),
                int.Parse(xele.Attribute("Atk").Value), float.Parse(xele.Attribute("speed").Value),
                float.Parse(xele.Attribute("AtkCoolDown").Value), int.Parse(xele.Attribute("Id").Value));
            Managers.DataManager.MonsterInfoDict.Add(_monster.Id, _monster);
        }
    }

}

public class MonsterInfo
{
    public int Id;
    public string Name;
    public int MaxHP;
    public int Atk;
    public float Speed;
    public float AtkCoolDown;
    public MonsterInfo(string _name, int _maxHP, int _atk, float speed, float atkCoolDown, int id)
    {
        this.Name = _name;
        this.MaxHP = _maxHP;
        this.Atk = _atk;
        Speed = speed;
        AtkCoolDown = atkCoolDown;
        Id = id;
    }
}
public class ItemInfo
{
    public string Name;
    public string description;
    public int addHP;
    public float Atk;

    public ItemInfo(string name, string description, int addHP, float atk)
    {
        Name = name;
        this.description = description;
        this.addHP = addHP;
        Atk = atk;
    }
}
