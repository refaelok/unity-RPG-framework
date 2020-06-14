using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoint = 100f;

        Animator animator;
        ActionScheduler actionScheduler;
        NavMeshAgent navMeshAgent;
        bool isDead;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamge(float damage)
        {
            healthPoint = Mathf.Max(healthPoint - damage, 0);
            if (healthPoint == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (!isDead)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
                GetComponent<Animator>().SetTrigger("die");
                isDead = true;
                GetComponent<NavMeshAgent>().enabled = false;
            }
        }

        public object CaptureState()
        {
            return healthPoint;
        }

        public void RestoreState(object state)
        {
            healthPoint = (float)state;

            if (healthPoint == 0)
            {
                Die();
            }
        }
    }
}
