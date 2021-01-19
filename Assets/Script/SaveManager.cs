using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{


    public static void Save(PlayerSprite player)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + " Player";
            FileStream file = new FileStream(path, FileMode.Create);


            SaveData data = new SaveData(player);
        

            bf.Serialize(file, data);

            file.Close();
            Debug.Log("Save");
            Debug.Log(Application.persistentDataPath);
        }
        catch(System.Exception e)
        {
            //에러 발생
            Debug.Log(e.Message);
        }
    }



    public static void ManagerSave(GameManager manager)
    {
        try
        {
            BinaryFormatter bf1 = new BinaryFormatter();
            string path = Application.persistentDataPath + " Manager";
            FileStream file = new FileStream(path, FileMode.Create);


            GameManagerData data = new GameManagerData(manager);


            bf1.Serialize(file, data);

            file.Close();
            Debug.Log("ManagerSave");
            
        }
        catch (System.Exception e)
        {
            //에러 발생
            Debug.Log(e.Message);
            
        }
    }

    public static void ItemSave(InventoryScript item)
    {
        try
        {
            BinaryFormatter bf1 = new BinaryFormatter();
            string path = Application.persistentDataPath + " Item";
            FileStream file = new FileStream(path, FileMode.Create);


            ItemData data = new ItemData(item);


            bf1.Serialize(file, data);

            file.Close();
            Debug.Log("item");

        }
        catch (System.Exception e)
        {
            //에러 발생
            Debug.Log(e.Message);

        }
    }
    public static SaveData Load()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + " Player";
            FileStream file = new FileStream(path, FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

        
            
            Debug.Log("Load");

            return data;

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            //에러 발생

            return default;
        }
    }
    public static GameManagerData ManagerLoad()
    {
        try
        {
            BinaryFormatter bf1 = new BinaryFormatter();
            string path = Application.persistentDataPath + " Manager";
            FileStream file = new FileStream(path, FileMode.Open);

            GameManagerData data = (GameManagerData)bf1.Deserialize(file);

            file.Close();



            Debug.Log("ManagerLoad");

            return data;

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            //에러 발생

            return default;
        }
    }
    public static ItemData ItemLoad()
    {
        try
        {
            BinaryFormatter bf1 = new BinaryFormatter();
            string path = Application.persistentDataPath + " Item";
            FileStream file = new FileStream(path, FileMode.Open);

            ItemData data = (ItemData)bf1.Deserialize(file);

            file.Close();



            Debug.Log("ItemLoad");

            return data;

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            //에러 발생

            return default;
        }
    }

    public static void Delete(string name)
    {
        File.Delete(Application.persistentDataPath + name);
    }
}

