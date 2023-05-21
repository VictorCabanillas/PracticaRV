using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SongSelector : MonoBehaviour
{
    private TMP_Dropdown desplegable;
    FileInfo[] info;
    void Awake()
    {
        desplegable = GetComponent<TMP_Dropdown>();
        DirectoryInfo SoundsDir = new DirectoryInfo(Application.streamingAssetsPath + "/Sounds"); //Saco la ruta de donde estan los archivos
        info = SoundsDir.GetFiles("*.mp3"); //Saco todos los archivos .mp3 a un array
    }

    private void Start()
    {
        foreach (FileInfo f in info)
        {
            desplegable.options.Add(new TMP_Dropdown.OptionData(Path.GetFileNameWithoutExtension(f.Name))); //Muestro y guardo los nombres sin la extension en una lista
        }
        PlayerPrefs.SetString("selectedSong", desplegable.options[desplegable.value].text);
        desplegable.onValueChanged.AddListener(delegate { DropdownValueChanged(desplegable); }); //Elijo una cancion aleatoria
    }

    void DropdownValueChanged(TMP_Dropdown change) 
    {
        PlayerPrefs.SetString("selectedSong", desplegable.options[desplegable.value].text); //almaceno la cancion elegida para usarla en otras escenas
    }
}
