using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class GameManager : MonoBehaviour
{
    public Transform player;
    public string fileName = "GameData.Xml";
    private GameData data = new GameData();

    private void Start()
    {
        string path = Application.dataPath + fileName;
    }
    public void Load(string path)
    {
        
    }
    public void Save (string path)
    {
        var serializer = new XmlSerializer(typeof(GameData));
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            //() = Parenthesis
            //[] = Brackets
            //{} = Braces
            data.position = player.position;
            data.rotation = player.rotation;
            data.dialogue = new string[] { "Hello", "World" };
            Save(Application.dataPath + fileName);
        }
    }
}
