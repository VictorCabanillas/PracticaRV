using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SongSelector : MonoBehaviour
{
    List<string> names = new List<string>();
    private string selectedSong;
    // Start is called before the first frame update
    void Awake()
    {
        DirectoryInfo SoundsDir = new DirectoryInfo(Application.streamingAssetsPath + "/Sounds"); //Saco la ruta de donde estan los archivos
        FileInfo[] info = SoundsDir.GetFiles("*.mp3"); //Saco todos los archivos .mp3 a un array

        foreach (FileInfo f in info)
        {
            names.Add(Path.GetFileNameWithoutExtension(f.Name)); //Muestro y guardo los nombres sin la extension en una lista
        }
        selectedSong = names[1]; //Elijo una cancion aleatoria

        PlayerPrefs.SetString("selectedSong", selectedSong);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
