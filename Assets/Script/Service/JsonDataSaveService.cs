using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataSaveService : IDataSaveService
{
    public bool SaveData<T>(string relativePath, T data)
    {
        var path = Application.persistentDataPath + relativePath;
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
                using (FileStream stream = File.Create(path))
                {
                    stream.Close();
                    File.WriteAllText(path, JsonConvert.SerializeObject(data));
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return false;
            }
        }
        else
        {
            try
            {
                using (FileStream stream = File.Create(path))
                {
                    stream.Close();
                    File.WriteAllText(path, JsonConvert.SerializeObject(data));
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }

    public T LoadData<T>(string relativePath)
    {
        var path = Application.persistentDataPath + relativePath;
        if (!File.Exists(path))
        {
            Debug.LogError($"path {path} does not exist");
            throw new FileNotFoundException($"{path} does not exist");
        }

        try
        {
            var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}