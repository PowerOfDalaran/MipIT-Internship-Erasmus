using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MusicManager : MonoBehaviour
    {
        public static bool alreadyExisting = false;
        bool stillFading = false;

        [SerializeField] float soundEffectsVolume = 1;

        AudioSource audioSource;

        [SerializeField] public AudioClip menuBackgroundClip;
        [SerializeField] public AudioClip gameBackgroundClip;

        public float GeneralVolume
        {
            get 
            {
                return audioSource.volume; 
            }

            set 
            {
                audioSource.volume = value; 
            }
        }

        public float SoundEffectsVolume
        {
            get 
            {
                return soundEffectsVolume; 
            }

            set 
            {
                soundEffectsVolume = value; 
            }
        }

        private void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();

            //Deleting this gameObject if there exist its copy
            if (alreadyExisting)
            {
                Destroy(gameObject);
            }
            else
            {
                alreadyExisting = true;
            }

            //Playing current audio clip and adding object to DontDestroyOnLoad list
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            //Pausing music if game got paused
            if (PauseMenu.GameIsPaused)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.UnPause();
            }
        }

        //Method playing single sound if game isn't paused
        public void PlaySingleSound(AudioClip singleSound)
        {
            if(!PauseMenu.GameIsPaused)
            {
                audioSource.PlayOneShot(singleSound, soundEffectsVolume);                
            }
        }

        //Changing background clip with enumerator
        public void ChangeBackgroundMusic(AudioClip backgroundClip)
        {
            StartCoroutine(FadeMusic(2.5f, 0, 0, backgroundClip));
            StartCoroutine(FadeMusic(2.5f, audioSource.volume, 2.5f, null));
        }

        //Enumerator making the music gradually mute or turn up the volume and switching the audio clip
        public IEnumerator FadeMusic(float duration, float targetVolume, float waitForSeconds, AudioClip clipToChange)
        {
            // Checking if previous enumerator finished working
            if (stillFading)
            {
                yield return new WaitForSeconds(waitForSeconds);
            }

            //Assigment of variables
            stillFading = true;
            float currentTime = 0;
            float start = audioSource.volume;

            //Gradual change of music
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }

            //Optional change of music clip
            if (clipToChange != null)
            {
                audioSource.clip = clipToChange;
                audioSource.Play();
            }

            stillFading = false;
            yield break;
        }
    }
