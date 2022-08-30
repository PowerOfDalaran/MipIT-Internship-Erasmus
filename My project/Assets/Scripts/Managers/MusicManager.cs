using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MusicManager : MonoBehaviour
    {
        [SerializeField] AudioSource standardAudioSource;
        [SerializeField] public AudioClip menuBackgroundClip;
        [SerializeField] public AudioClip gameBackgroundClip;
        bool stillFading = false;
        [SerializeField] float singleEffectsVolume = 1;
        public static bool alreadyExisting = false;

        private void Awake()
        {
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
            if (standardAudioSource.clip != null)
            {
                standardAudioSource.Play();
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            //Code for pausing music if game got paused
            /*
            if (InGameMenuManager.gameIsPaused)
            {
                standardAudioSource.Pause();
            }
            else
            {
                standardAudioSource.UnPause();
            }
            */
        }

        // Method playing single sound
        public void PlaySingleSound(AudioClip singleSound)
        {
            standardAudioSource.PlayOneShot(singleSound, singleEffectsVolume);
        }

        // Method changing background clip with enumerator
        public void ChangeBackgroundMusic(AudioClip backgroundClip)
        {
            StartCoroutine(FadeMusic(2.5f, 0, 0, backgroundClip));
            StartCoroutine(FadeMusic(2.5f, 1, 2.5f, null));
        }

        //Enumerator muting and turning up the music and optionally changing the background clip, if fourth parameter isn't null
        public IEnumerator FadeMusic(float duration, float targetVolume, float waitForSeconds, AudioClip clipToChange)
        {
            // Checking if previous enumerator finished working
            if (stillFading)
            {
                yield return new WaitForSeconds(waitForSeconds);
            }

            stillFading = true;
            float currentTime = 0;
            float start = standardAudioSource.volume;

            //Gradual change of music
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                standardAudioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }

            //Optional change of music clip
            if (clipToChange != null)
            {
                standardAudioSource.clip = clipToChange;
                standardAudioSource.Play();
            }

            stillFading = false;
            yield break;
        }
    }
