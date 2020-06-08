using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        PlayableDirector playableDirector;
        bool alreadyTriggered = false;

        private void Start()
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyTriggered && other.gameObject.tag == "Player")
            {
                playableDirector.Play();
                alreadyTriggered = true;
            }
        }
    }
}

