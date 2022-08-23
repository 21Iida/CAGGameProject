using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class AudioEsc : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> audioSources;

        private void OnEnable()
        {
            foreach (var source in audioSources)
            {
                if(source == null) return;
                source.Pause();
            }
        }

        private void OnDisable()
        {
            foreach (var source in audioSources)
            {
                if(source == null) return;
                source.Play();
            }
        }
    }
}
