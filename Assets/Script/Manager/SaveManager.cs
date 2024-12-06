using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{
    private string directoryPath;

    // 생성자에서 초기화
    public SaveManager()
    {
        directoryPath = Application.persistentDataPath + "/SaveData";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Debug.Log($"Directory created at: {directoryPath}");
        }
        else
        {
            Debug.Log($"Save directory exists at: {directoryPath}");
        }
    }

    // 범용 저장 메서드
    public void SaveData<T>(string fileName, T data)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("fileName is null or empty.");
            return;
        }

        string filePath = Path.Combine(directoryPath, fileName + ".dat");

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }
            Debug.Log($"Data saved at: {filePath}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to save data to {filePath}. Error: {ex.Message}");
        }
    }

    // 범용 로드 메서드
    public T LoadData<T>(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("fileName is null or empty.");
            return default(T);
        }

        string filePath = Path.Combine(directoryPath, fileName + ".dat");

        if (File.Exists(filePath))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    return (T)formatter.Deserialize(stream);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to load data from {filePath}. Error: {ex.Message}");
                return default(T);
            }
        }
        else
        {
            Debug.LogWarning($"No file found at: {filePath}");
            return default(T);
        }
    }
}
