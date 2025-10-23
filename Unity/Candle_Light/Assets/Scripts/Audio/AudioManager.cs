using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public float masterVolume = 1.0f;
    public float sfxVolume = 1.0f;
    public float musicVolume = 1.0f;
    public Dictionary<string, AudioPlayer> playersList = new Dictionary<string, AudioPlayer>();
    public Dictionary<string, int> playerInstanceCounter = new Dictionary<string, int>();



    #region volume setup 

    public void SetMaster(float value)
    {
        masterVolume = value / 5;
        UpdateAudioListVolume();
    }
    public void SetSfx(float value)
    {
        sfxVolume = value / 5;
        UpdateAudioListVolume();
    }
    public void SetMusic(float value)
    {
        musicVolume = value / 5;
        UpdateAudioListVolume();
    }

    #endregion


    #region Audio List Management

    public void AddPlayerToList(string playerName, AudioPlayer player)
    {
        if (playersList.ContainsKey(playerName))
        {
            if (player.canHaveMultipleInstances)
            {
                if (playerInstanceCounter.ContainsKey(playerName))
                {
                    playerInstanceCounter[playerName]++;
                    playersList[playerName + playerInstanceCounter[playerName]] = player;
                }
                else
                {
                    playerInstanceCounter.Add(playerName, 0);
                    playersList[playerName + playerInstanceCounter[playerName]] = player;
                }
            }
            else
            {
                playersList[playerName] = player;
            }
        }
        else
        {
            playersList.Add(playerName, player);
        }
        UpdatePlayerVolume(player);

    }

    public void RemovePlayerFromList(string playerName)
    {
        playersList.Remove(playerName);
    }

    private void UpdateAudioListVolume()
    {
        foreach (var player in playersList.Values)
        {
            if (player == null) continue;
            UpdatePlayerVolume(player);
        }
    }

    private void UpdatePlayerVolume(AudioPlayer player)
    {
        if (player.channel == AudioPlayer.SoundChannel.SFX)
        {
            player.SetVolume(sfxVolume * masterVolume);
        }
        else if (player.channel == AudioPlayer.SoundChannel.MUSIC)
        {
            player.SetVolume(musicVolume * masterVolume);
        }

    }

    #endregion


    #region Audio Play

    public void PlaySound(string soundName)
    {
        if (playersList.ContainsKey(soundName) && playersList[soundName] != null)
        {
            playersList[soundName].PlaySound();
        }
        else if (playersList.ContainsKey(soundName) && playersList[soundName] == null)
        {
            playersList.Remove(soundName);
        }

    }

    public void PlaySound(string soundName, Vector3 position)
    {
        if (playersList.ContainsKey(soundName) && playersList[soundName] != null)
        {
            playersList[soundName].PlaySound(position);
        }
        else if (playersList.ContainsKey(soundName) && playersList[soundName] == null)
        {
            playersList.Remove(soundName);
        }
    }

    public void StopSound(string soundName)
    {
        if (playersList.ContainsKey(soundName) && playersList[soundName] != null)
        {
            playersList[soundName].StopAudio();
        }
        else if (playersList.ContainsKey(soundName) && playersList[soundName] == null)
        {
            playersList.Remove(soundName);
        }
    }

    #endregion

}
