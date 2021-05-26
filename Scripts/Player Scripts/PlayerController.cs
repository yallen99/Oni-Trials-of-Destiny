using System;
using Health_Scripts;
using System.Collections;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        // [Header("Day Night")] [SerializeField] private DayNightControl dayNightControl;
        
        [Header("Animation")] [SerializeField] private Animator foxAnimator;
        [SerializeField] private Animator kitsuneAnimator;
        [Space]
        [Header("Health")]
        [SerializeField] private HealthBarPlayer healthBarPlayer;
        [SerializeField] private GameObject transformIcon;
        [Space]
        [Header("Movement")]
        [SerializeField] float rotationSpeed = 0.3f;
        [SerializeField] float allowRotation = 0.1f;
        [SerializeField] float movementSpeed = 1f;
        [SerializeField] float gravityMultiplier;
        [SerializeField] float jumpForce = 10f;
        [Space]
        [Header("Timer")] [SerializeField] private float transformationTimer;

        [SerializeField] private float transformationScale;

        [SerializeField] private GameObject staminaBar;
        [Space]
        [Header("Effects")] [SerializeField] private ParticleSystem transformEffects;
       
        [HideInInspector] public HealthSystem playerHealthSystem;
        [HideInInspector] public bool poweredUp; 
        private float _inputX, _inputZ, _speed, _gravity;
        private Camera _cam;
        private CharacterController _characterController;
        private Vector3 _desiredMoveDirection;
        private float _verticalVelocity;
        private PlayerInventory _playerInventory;
        private Animator _animator;
        private float _sprintSpeed;
        private bool _playerIsDead;
        private bool _countdown;
        private float sprintTimer = 4;
        private bool isSprinting = false;
        private Vector3 _iconPosition;
        private static readonly int Walking = Animator.StringToHash("walking");
        private static readonly int Jumping = Animator.StringToHash("jumping");
        private static readonly int Transform = Animator.StringToHash("Transform");
       [HideInInspector] public bool grounded;

       




        void Start()
        {
            _iconPosition = transformIcon.transform.position;
            transformIcon.SetActive(false);
            LinkComponents();
            playerHealthSystem.Initialize(200);
            healthBarPlayer.Setup(playerHealthSystem);
            
            poweredUp = false;
            _countdown = false;

        }

        
        private void Update()
        {
            _playerInventory.DisplayInventory();
            _inputX = Input.GetAxis("Horizontal");
            _inputZ = Input.GetAxis("Vertical");
            
            InputDecider();
            
            MovementManager();

            #region Animation Check

            if (Input.GetKey(KeyCode.A) ||Input.GetKey(KeyCode.S) ||Input.GetKey(KeyCode.D) ||Input.GetKey(KeyCode.W))
            {
                foxAnimator.SetBool(Walking, true);
                kitsuneAnimator.SetBool(Walking, true);
                foxAnimator.SetBool(Jumping, false);
                kitsuneAnimator.SetBool(Jumping, false);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    foxAnimator.SetBool(Jumping, true);
                    kitsuneAnimator.SetBool(Jumping, true);
                    foxAnimator.SetBool(Walking, false);
                    kitsuneAnimator.SetBool(Walking, false);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
               foxAnimator.SetBool(Jumping, true);
               kitsuneAnimator.SetBool(Jumping, true);
            }
            else
            {
               foxAnimator.SetBool(Jumping, false);
               kitsuneAnimator.SetBool(Jumping, false);
              
               foxAnimator.SetBool(Walking, false);
                kitsuneAnimator.SetBool(Walking, false);
            }
            #endregion
            
            #region Sprint Check
            if (Input.GetButtonDown("Sprint"))
                {
                    isSprinting = true;
                    _sprintSpeed = 10;
                }
            if (Input.GetButtonUp("Sprint") || sprintTimer <=0)
            {
                isSprinting = false;
                _sprintSpeed = 0;
            }
            
            
            if (isSprinting)
            {
                sprintTimer -= Time.deltaTime;
            }
            if (sprintTimer < 4 && !isSprinting)
            {
                sprintTimer += Time.deltaTime;
            }
            UpdateStamiaBar();
            #endregion
            
     
            if (_playerInventory.PlayerCanPowerup() && !poweredUp)
            {
                _animator.SetBool(Transform, true);
                StartCoroutine(KitsuneForm());
                poweredUp = true;
                transformIcon.SetActive(true);
            }
           
            if(poweredUp){ IconUpdate(); }
        
        }

        private void FixedUpdate()
        {
            if (_countdown)
            {
                transformationTimer -= Time.deltaTime;
                
            }
        }
        
        private void LinkComponents()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _cam = Camera.main;
            _characterController = GetComponent<CharacterController>();
            playerHealthSystem = GetComponent<HealthSystem>();
            _animator = GetComponent<Animator>();
        }

        private void UpdateStamiaBar()
        {
            staminaBar.transform.localScale = new Vector3(sprintTimer/4, 1, 1);
        }

   
        
        private IEnumerator KitsuneForm()
        {
            transformEffects.Play();
            gravityMultiplier += 0.1f; 
            jumpForce += 6;
            _countdown = true;
            yield return new WaitForSeconds(transformationTimer);
         
            ResetHealthIcon();
            ResetTransformation();
            transformEffects.Play();
            gravityMultiplier -= 0.1f; 
            jumpForce -= 6;
        }

        #region  Timers
        private void ResetHealthIcon(){
            transformIcon.transform.localScale = new Vector3(1, 1, 1);
            _iconPosition = new Vector3(_iconPosition.x,_iconPosition.y, _iconPosition.z);
        }

        private void ResetTransformation()
        {
            _animator.SetBool(Transform, false);
            poweredUp = false;
            _countdown = false;
            transformIcon.SetActive(false);
            transformationTimer = transformationScale;

        }
        
        private void IconUpdate()
        {
            transformIcon.transform.localScale = new Vector3(1, GetSizeFraction(), 1);

        }

 
        private float GetSizeFraction()
        {
            return transformationTimer/transformationScale;
        }
        
        #endregion

        #region Movement

        void InputDecider()
        {
            _speed = new Vector2(_inputX, _inputZ).sqrMagnitude;

            if (_speed > allowRotation)
            {
                RotationManager();
            }
            else
            {
                _desiredMoveDirection = Vector3.zero;
            }
        }
        void RotationManager()
        {
            var transform1 = _cam.transform;
            var forward = transform1.forward;
            var right = transform1.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            _desiredMoveDirection = forward * _inputZ + right * _inputX;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_desiredMoveDirection), rotationSpeed);
        }
        void MovementManager()
        {
            _gravity -= 9.8f * Time.deltaTime;
            _gravity = _gravity * gravityMultiplier;

            Vector3 moveDirection = _desiredMoveDirection * ((movementSpeed + _sprintSpeed) * Time.deltaTime);
            moveDirection = new Vector3(moveDirection.x, _gravity, moveDirection.z);
            _characterController.Move(moveDirection);
            /*if (_characterController.isGrounded)
            {
                _verticalVelocity = -9.8f * Time.deltaTime;
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _verticalVelocity = jumpForce;
                }
            }
            else
            {
                _verticalVelocity -= 9.8f * Time.deltaTime;
            }*/

            
            Ray ray = new Ray(new Vector3(transform.position.x,transform.position.y - 0.5f, transform.position.z), -transform.up);
            RaycastHit hitObject;
            
            if (Physics.Raycast(ray, out hitObject, 0.5f) && hitObject.collider.gameObject.CompareTag("Ground"))
            {
                _verticalVelocity -= 9.8f * Time.deltaTime;
                Debug.DrawLine(ray.origin, hitObject.point, Color.green);
                grounded = true;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _verticalVelocity = jumpForce;
                }
            }
            else
            {
                Debug.DrawLine(ray.origin, hitObject.point, Color.red);
                _verticalVelocity -= 9.8f * Time.deltaTime;
                grounded = false;
            }

            Vector3 moveVector = new Vector3(0, _verticalVelocity, 0);
            _characterController.Move(moveVector * Time.deltaTime);
        }

        #endregion

        public bool ShouldRespawn() => playerHealthSystem.IsDead();
        public bool IsGrounded() => grounded;


    }
}
