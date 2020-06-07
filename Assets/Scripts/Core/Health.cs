using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoint = 100f;

        Animator animator;
        ActionScheduler actionScheduler;
        NavMeshAgent navMeshAgent;
        bool isDead;

        private void Start()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

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
                actionScheduler.CancelCurrentAction();
                animator.SetTrigger("die");
                isDead = true;
                navMeshAgent.enabled = false;
            }
        }
    }
}
