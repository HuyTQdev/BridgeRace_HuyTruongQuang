using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public string filePath = "CSV_Map/Map_1.csv"; // Relative path to the CSV file within StreamingAssets

    void Start()
    {
        List<List<int>> matrix = LoadCSV(filePath);

        // Output to debug the matrix content
        foreach (List<int> row in matrix)
        {
            Debug.Log(string.Join(", ", row));
        }
    }

    public static List<List<int>> LoadCSV(string relativePath)
    {
        // Combine the relative path with the data path
        string fullPath = Path.Combine(Application.streamingAssetsPath, relativePath);
        string csvData = null;

        if (Application.platform == RuntimePlatform.Android)
        {
            // Android requires using UnityWebRequest to read from StreamingAssets synchronously
            using (UnityEngine.Networking.UnityWebRequest reader = UnityEngine.Networking.UnityWebRequest.Get(fullPath))
            {
                reader.SendWebRequest();
                while (!reader.isDone) { }
                if (reader.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError || reader.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Failed to read file: " + reader.error);
                    return new List<List<int>>(); // Return an empty list to avoid further errors
                }
                csvData = reader.downloadHandler.text;
            }
        }
        else
        {
            if (!File.Exists(fullPath))
            {
                //Debug.LogError("File not found: " + fullPath);
                return new List<List<int>>(); // Return an empty list to avoid further errors
            }
            csvData = File.ReadAllText(fullPath);
        }

        return ParseCSV(csvData);
    }

    private static List<List<int>> ParseCSV(string csvData)
    {
        List<List<int>> matrix = new List<List<int>>();
        string[] lines = csvData.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue; // Skip empty lines
            List<int> row = new List<int>();
            string[] entries = line.Split(',');
            foreach (string entry in entries)
            {
                if (int.TryParse(entry.Trim(), out int value))
                {
                    row.Add(value);
                }
                else
                {
                    row.Add(-1); // Add -1 for missing or invalid data
                }
            }
            matrix.Add(row);
        }

        return matrix;
    }
}
