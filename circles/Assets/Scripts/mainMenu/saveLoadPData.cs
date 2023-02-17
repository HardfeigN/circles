using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class saveLoadPData
{
    public static void SavePlayerData(saveData sData, GameHost gamehost)
    {
        if (gamehost.IsSaving())
        {
            Save(sData);
        }
    }
    public static void SavePlayerData(saveData sData, shopMenu shopMenu)
    {
        if (shopMenu.IsSaving())
        {
            Save(sData);
        }
    }
    public static saveData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fstream = new FileStream(path, FileMode.Open);

            saveData sClass = formatter.Deserialize(fstream) as saveData;
            fstream.Close();
            return sClass;
        }
        else
        {
            saveData sClass = new saveData();
            Save(sClass);
            return sClass;
        }
    }
    private static void Save(saveData sData)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path)) File.Delete(path);

        FileStream fstream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fstream, sData);
        fstream.Close();
    }
}
