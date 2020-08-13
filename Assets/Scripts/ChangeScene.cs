using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SoundEffectsSlider;

    public void LevelToggle()
    {
        GameManager.Instance.LevelOfTheDay = !GameManager.Instance.LevelOfTheDay;
    }

    public void SoundEffectsAdjust()
    {
        GameManager.Instance.SoundEffectsVolume = SoundEffectsSlider.value;
    }

    public void MusicLevelToggle()
    {
        GameManager.Instance.MusicVolume = MusicSlider.value;
    }

    public void TwoPlayerToggle()
    {
        GameManager.Instance.TwoPlayer = !GameManager.Instance.TwoPlayer;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Boot");
    }

    public void GoToOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over Screen");
    }
}
