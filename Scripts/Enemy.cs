using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Health_Scripts;
using Player_Scripts;
using Scriptable_Objects;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
        private PlayerController playerController;
        [Header("Animation")]
        [SerializeField] private GameObject projectile;
        [SerializeField] private float projectileAnimTime;
        [SerializeField] private float animAttackDelay;
        [SerializeField] private Animator enemyAnimator;
        
        [Space]
        [Header("Enemy Config")]
        [SerializeField] private float attackRange= 0.5f;
        [SerializeField] private PathConfig pathConfig;
        [SerializeField] private HealthBarEnemy healthBarEnemy;
        [SerializeField] private int enemyHealth;

        [Space] [Header("Effects")] [SerializeField]
        private ParticleSystem particles;
        
        private HealthSystem _healthSystem;
        private List<Transform> _wayPoints;
        private int _wayPointIndex;
       
        private static readonly int Attack = Animator.StringToHash("attack");

        private enum States
        {
            PATROL,
            ATTACK
        }

        private States activeState; 
        
        private void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
            _wayPoints = pathConfig.GetWayPoints();
            transform.position = _wayPoints[_wayPointIndex].transform.position;
            _healthSystem = GetComponent<HealthSystem>();
            _healthSystem.Initialize(enemyHealth);
            healthBarEnemy.Setup(_healthSystem);

            shouldAttack = true;
            activeState = States.PATROL;
        }

        private void Update()
        {
            switch (activeState)
            {
                case States.PATROL:
                    Move();
                    SearchForTarget();
                    break;
                case States.ATTACK:
                    AttackPlayer();
                    break;
            }
            
            if (_healthSystem.IsDead())
            {
                Instantiate(particles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        private void SearchForTarget()
        {
            if (IsPlayerInAttackRange())
            {
                activeState = States.ATTACK;
            }
        }


        private bool shouldAttack;
        private void AttackPlayer()
        {
            this.transform.LookAt(playerController.transform);
            enemyAnimator.SetBool(Attack, true);
            if (shouldAttack)
            {
                StartCoroutine(ShootPlayer());
            }

            if (!IsPlayerInAttackRange())
            {
                activeState = States.PATROL;
            }
        }

        private IEnumerator ShootPlayer()
        {
            shouldAttack = false;
            yield return new WaitForSeconds(animAttackDelay);
            projectile.SetActive(true);
            yield return new WaitForSeconds(projectileAnimTime);
            projectile.SetActive(false);
            shouldAttack = true;
        }
        
        private void Move()
        {
            enemyAnimator.SetBool(Attack, false);
            
            if (_wayPointIndex <= _wayPoints.Count - 1)
            {
                var targetPosition = _wayPoints[_wayPointIndex].transform.position;
                var moveThisFrame = pathConfig.GetSpeed() * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveThisFrame);
                this.transform.LookAt(targetPosition);

                if (transform.position == targetPosition)
                {
                    _wayPointIndex++;
                }
            }
            else
            {
                _wayPointIndex = 0;
            }
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        private bool IsPlayerInAttackRange() => Vector3.Distance(playerController.transform.position, this.transform.position) <= attackRange;
        


}
