using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMB.Game
{
    internal class SoundManager : MonoBehaviour
    {
        internal static SoundManager Instance { get; private set; }

        [InspectorName("Background Music (Audio Source)")]
        [SerializeField]
        private AudioSource _BackgroundMusicSource;

        [InspectorName("Sound Effects (Audio Source)")]
        [SerializeField]
        private AudioSource _SoundEffectsSource;

        private Dictionary<BackgroundMusic, AudioClip> _BackgroundMusicLibrary;
        private Dictionary<SoundEffect, AudioClip> _SoundEffectsLibrary;

        private void Awake()
        {
            // Only one instance can exist at a time.
            if (Instance == null)
            {
                Instance = this;
                GameObject.DontDestroyOnLoad(this.gameObject);

                // Load the sound effects and background music into memory.
                _BackgroundMusicLibrary = new Dictionary<BackgroundMusic, AudioClip>()
                {
                    { BackgroundMusic.Ground,       Resources.Load<AudioClip>("Audio/Background/Ground") },
                    { BackgroundMusic.StageClear,   Resources.Load<AudioClip>("Audio/Background/Stage_Clear") },
                    { BackgroundMusic.WorldClear,   Resources.Load<AudioClip>("Audio/Background/World_Clear") },
                    { BackgroundMusic.GameOver,     Resources.Load<AudioClip>("Audio/Background/Game_Over") }
                };

                _SoundEffectsLibrary = new Dictionary<SoundEffect, AudioClip>()
                {
                    { SoundEffect.JumpSmall,        Resources.Load<AudioClip>("Audio/SoundFX/Jump") },
                    { SoundEffect.JumpSuper,        Resources.Load<AudioClip>("Audio/SoundFX/Jump_Super") },
                    { SoundEffect.CoinCollected,    Resources.Load<AudioClip>("Audio/SoundFX/Collect_Coin") },
                    { SoundEffect.PowerUpCollected, Resources.Load<AudioClip>("Audio/SoundFX/Collect_PowerUp") },
                    { SoundEffect.PowerUpAppears,   Resources.Load<AudioClip>("Audio/SoundFX/PowerUp_Appears") },
                    { SoundEffect.Bump,             Resources.Load<AudioClip>("Audio/SoundFX/Bump") },
                    { SoundEffect.Kick,             Resources.Load<AudioClip>("Audio/SoundFX/Kick") },
                    { SoundEffect.Pause,            Resources.Load<AudioClip>("Audio/SoundFX/Jump") },
                    { SoundEffect.PipeTravel,       Resources.Load<AudioClip>("Audio/SoundFX/Pipe") },
                    { SoundEffect.PlayerDies,       Resources.Load<AudioClip>("Audio/SoundFX/Mario_Dies") },
                    { SoundEffect.Stomp,            Resources.Load<AudioClip>("Audio/SoundFX/Stomp") },
                    { SoundEffect.TimeWarning,      Resources.Load<AudioClip>("Audio/SoundFX/Time_Warning") },
                    { SoundEffect.VineGrowing,      Resources.Load<AudioClip>("Audio/SoundFX/Vine") },
                    { SoundEffect.FirePower,        Resources.Load<AudioClip>("Audio/SoundFX/Fireball") },
                    { SoundEffect.DownFlagPole,     Resources.Load<AudioClip>("Audio/SoundFX/Flagpole") },
                    { SoundEffect.Fireworks,        Resources.Load<AudioClip>("Audio/SoundFX/Fireworks") },
                    { SoundEffect.OneUpCollected,   Resources.Load<AudioClip>("Audio/SoundFX/1Up") },
                    { SoundEffect.BowserFalls,      Resources.Load<AudioClip>("Audio/SoundFX/Bowser_Falls") },
                    { SoundEffect.BowserFire,       Resources.Load<AudioClip>("Audio/SoundFX/Bowser_Fire") },
                    { SoundEffect.BrickSmash,       Resources.Load<AudioClip>("Audio/SoundFX/Break_Block") },
                };
            }
            else GameObject.Destroy(this);
        }

        private void Start()
        {

        }

        internal float GetBackgroundMusicVolume()
        {
            return _BackgroundMusicSource.volume;
        }

        internal float GetSoundEffectsVolume()
        {
            return _SoundEffectsSource.volume;
        }

        internal void SetBackgroundMusicVolume(float volume)
        {
            if (volume < 0.0f) volume = 0f;
            else if (volume > 1.0f) volume = 1f;

            _BackgroundMusicSource.volume = volume;
        }

        internal void SetSoundEffectsVolume(float volume)
        {
            if (volume < 0.0f) volume = 0f;
            else if (volume > 1.0f) volume = 1f;

            _SoundEffectsSource.volume = volume;
        }

        internal void Play(SoundEffect effect)
        {
            _SoundEffectsSource.PlayOneShot(_SoundEffectsLibrary[effect]);
        }

        internal void Play(SoundEffect effect, float volume)
        {
            if (volume < 0.0f) volume = 0f;
            else if (volume > 1.0f) volume = 1f;

            _SoundEffectsSource.PlayOneShot(_SoundEffectsLibrary[effect], volume);
        }

        internal void Play(BackgroundMusic music, bool shouldLoop = true)
        {
            // If a song is playing, stop it.
            if (_BackgroundMusicSource.isPlaying) _BackgroundMusicSource.Stop();

            // Switch the songs and play the new one.
            _BackgroundMusicSource.clip = _BackgroundMusicLibrary[music];
            _BackgroundMusicSource.loop = shouldLoop;
            _BackgroundMusicSource.Play();
        }

        internal void PauseAllSound()
        {
            PauseBackgroundMusic();
            PauseSoundEffects();
        }

        internal void UnpauseAllSound()
        {
            UnpauseBackgroundMusic();
            UnpauseSoundEffects();
        }

        internal void PauseSoundEffects()
        {
            _SoundEffectsSource.Pause();
        }

        internal void UnpauseSoundEffects()
        {
            _SoundEffectsSource.UnPause();
        }

        internal void PauseBackgroundMusic()
        {
            _BackgroundMusicSource.Pause();
        }

        internal void UnpauseBackgroundMusic()
        {
            _BackgroundMusicSource.UnPause();
        }

        internal void StopAllSound()
        {
            StopBackgroundMusic();
            StopSoundEffects();
        }

        internal void StopSoundEffects()
        {
            _SoundEffectsSource.Stop();
        }

        internal void StopBackgroundMusic()
        {
            _BackgroundMusicSource.Stop();
        }
    }

    internal enum SoundEffect
    {
        Pause,
        PlayerDies,
        DownFlagPole,
        Bump,
        BrickSmash,
        Kick,
        Stomp,
        PipeTravel,
        JumpSmall,
        JumpSuper,
        FirePower,
        StarPower,
        OneUpCollected,
        CoinCollected,
        PowerUpCollected,
        PowerUpAppears,
        Fireworks,
        VineGrowing,
        TimeWarning,
        BowserFalls,
        BowserFire
    }

    /// <summary></summary>
    /// <remarks></remarks>
    internal enum BackgroundMusic
    {
        Ground,
        GroundHurry,
        Underground,
        UndergroundHurry,
        WaterWorld,
        WaterWorldHurry,
        KoopaCastle,
        KoopaCastleHurry,
        Ending,
        StageClear,
        WorldClear,
        GameOver,
        GameOverAlternate,
    }
}