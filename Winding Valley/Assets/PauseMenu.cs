using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource globalSoundFx;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundFXSlider;
    [SerializeField] GameObject dialogQuit;
    private void Awake()
    {
        dialogQuit.SetActive(false);
        pauseMenuUI.SetActive(false);
        musicSlider.value = music.volume;
        soundFXSlider.value = globalSoundFx.volume;

    }
    private void Update()
    {
        if(File.Exists(Application.persistentDataPath + "/save.data"))
        {
            pauseMenuUI.transform.GetChild(3).GetComponent<Button>().enabled = true;
        }
        else
        {
            pauseMenuUI.transform.GetChild(3).GetComponent<Button>().enabled = false;
        }
        music.volume = musicSlider.value;
        globalSoundFx.volume = soundFXSlider.value;

    }
    public void openDialogQuit()
    {
        dialogQuit.SetActive(true);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        music.Play();
        globalSoundFx.Play();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        music.Pause();
        globalSoundFx.Pause();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
