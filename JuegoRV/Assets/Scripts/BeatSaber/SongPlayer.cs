using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    private AudioSource audioPlayer;
    private AudioClip audioClip;
    private string selectedSong;

    // Start is called before the first frame update
    void Start()
    {
        selectedSong = PlayerPrefs.GetString("selectedSong");
        audioPlayer = GetComponent<AudioSource>();
        StartCoroutine(LoadAudio()); //Pido que se reproduzca la cancion elegida
    }
    private WWW GetAudioFromFile(string path)
    {
        WWW request = new WWW(path);
        return request;
    }

    private IEnumerator LoadAudio()
    {
        string soundPath = Application.streamingAssetsPath + "/Sounds/" + selectedSong + ".mp3"; //Construyo el path a la cancion
        WWW request = GetAudioFromFile(soundPath); //Pido que descargue el archivo
        yield return request; //Devuelvo a unity el control hasta que termine de descargar y despues continuo en este punto

        audioClip = request.GetAudioClip();  //Transformo el archivo a un audioClip
        audioClip.name = selectedSong; //Le asigno un nombre (no es necesario)
        audioPlayer.clip = audioClip; //Cargo el clip al audioPlayer
        yield return new WaitForSecondsRealtime(4f);
        audioPlayer.Play(); //le doy al play
    }
}
