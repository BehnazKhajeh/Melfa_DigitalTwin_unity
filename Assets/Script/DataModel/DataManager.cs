using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager
{
    public void savejson(string name,string json)
    {
        File.WriteAllText(Application.persistentDataPath + "/" + name + ".json", json);
    }
    public string loadjson(string name)
    {
        try{
            return File.ReadAllText(Application.persistentDataPath + "/" + name + ".json");
        }catch{
             return "";
        }
       
    }
    
}
