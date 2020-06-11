using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier { A, B };
        [SerializeField] int SceneToLoad = -1;
        [SerializeField] Transform spawwnPoint;
        [SerializeField] DestinationIdentifier destination;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);

            yield return SceneManager.LoadSceneAsync(SceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawwnPoint.transform.position);
            player.transform.rotation = otherPortal.spawwnPoint.transform.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal != this && portal.destination == destination)
                {
                    return portal;
                }
            }

            return null;
        }
    }
}

