using TMPro;
// to avoid problems when trying to build 
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.IO;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance;
    public int bestScore = 0;
    public string bestPlayer;
    public string currentPlayer;

    


    [System.Serializable]
    public class Data
    {
        public string bestPlayer;
        public int bestScore;
    }


    public void SaveData()
    {
        Data data = new Data();
        data.bestPlayer = bestPlayer;
        data.bestScore = bestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<Data>(json);
            bestPlayer = data.bestPlayer;
            bestScore = data.bestScore;
        }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;


            DontDestroyOnLoad(gameObject);

            



        }
        else
        {
            Destroy(gameObject);
        }


        LoadData();
    }

    

    public void Exit()
    {
        SaveData();
       

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif



    }


   
    



    
}
