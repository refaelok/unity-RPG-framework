﻿using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 1f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float attackSpeed = 1f;

        Mover mover;
        Animator animator;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {

            timeSinceLastAttack += Time.deltaTime;

            if (target && !target.IsDead())
            {
                if (!GetIsRange())
                {
                    Vector3 destination = GetDistanceFromTarget();
                    mover.MoveTo(destination);
                }
                else
                {
                    mover.Cancel();
                    AttackBehaviour();
                }
            }
        }

        private Vector3 GetDistanceFromTarget()
        {
            Collider targetCollider = target.GetComponent<Collider>();
            return targetCollider ? targetCollider.ClosestPoint(transform.position) : target.transform.position;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > attackSpeed)
            {
                // This will trigger the Hit() event.

                animator.ResetTrigger("stopAttack");
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }


        // Animation Event
        void Hit()
        {
            if (target)
            {
                target.TakeDamge(weaponDamage);
            }
        }

        private bool GetIsRange()
        {
            return Vector3.Distance(transform.position, GetDistanceFromTarget()) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            Health targetToTtest = combatTarget ? combatTarget.GetComponent<Health>() : null;
            return targetToTtest && !targetToTtest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
            target = null;
            mover.Cancel();
        }
    }
}
