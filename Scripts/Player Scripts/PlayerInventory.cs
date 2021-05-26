using System;
using System.Collections;
using System.Diagnostics;
using Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Player_Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Inventory")]
        public ObjectInInventory inventory;
        [Space]
        [Header("Pop Up icons")]
        public TextMeshProUGUI tailsCollectedPopUp;
        public TextMeshProUGUI crystalsCollectedPopUp;
        [SerializeField] private GameObject tailsPopup;
        [SerializeField] private GameObject crystalsPopup;
        [Space] 
        [Header("Notification")] 
        [SerializeField] private GameObject notificationMinimized;
        [SerializeField] private GameObject notificationMaximized;
        [Header("Required Items - VALUE")]
        [SerializeField] private int requiredTailsNumber;
        [SerializeField] private int requiredCrystalsNumber;
        [Space]
        [Header("Collected Items - TEXT")]
        public TextMeshProUGUI tailsCollected;
        public TextMeshProUGUI crystalsCollected;
        [Header("Total Items- TEXT")]
        public TextMeshProUGUI totalTails;
        public TextMeshProUGUI totalCrystals;
        [Header("Required Items- TEXT")]
        public TextMeshProUGUI requiredTails;
        public TextMeshProUGUI requiredCrystals;
        [Header("Had items of required - TEXT")]
        [SerializeField] private TextMeshProUGUI hadTailsNumber;
        [SerializeField] private TextMeshProUGUI hadCrystalsNumber;
        [Space]
        [Header("Other Links")]
        public GameObject powerUp;
        [SerializeField] private Animator inventoryAnimator;
        [Space] 
        [Header("Effects")] 
        [SerializeField] private ParticleSystem healEffect;
        [SerializeField]private ParticleSystem crystalEffect;
        
        
        private Item[] _item;
        private int _totalTailsNumber;
        private int _totalCrystalsNumber;
        private int _tailNumber;
        private int _crystalNumber;
        private int _hadCrystals;
        private int _hadTails;
        private bool _isInvOpen;
        private bool _playerCanPowerup;
        private bool _hasSeal;
        private PlayerController _player;
        
        private static readonly int Open = Animator.StringToHash("open");
        private static readonly int Close = Animator.StringToHash("close");
        private static readonly int CrystalPop = Animator.StringToHash("crystalPop");
        private static readonly int TailPop = Animator.StringToHash("tailPop");

        private void Start()
        {
            powerUp.SetActive(false);
            _item = FindObjectsOfType<Item>();
            _player = GetComponent<PlayerController>();
            
            InitializeReqItems();
            InitializeItems();
            AssignItemsToInventory();
        }

        #region Initialize
        private void InitializeReqItems()
        {
            requiredTails.text = "X   " + requiredTailsNumber.ToString();
            requiredCrystals.text = "X   " + requiredCrystalsNumber.ToString();
            _hasSeal = false;
        }
        private void InitializeItems()
        {
            foreach (var itemFound in _item)
            {
                itemFound.GetItemType();

                if (itemFound.GetItemType() == "Crystal")
                {
                    _totalCrystalsNumber++;
                }

                if (itemFound.GetItemType() == "Tail")
                {
                    _totalTailsNumber++;
                }
            }
        }
        private void AssignItemsToInventory()
        {
            totalCrystals.text = "X    " +  _totalCrystalsNumber.ToString();
            totalTails.text = "X    " + _totalTailsNumber.ToString();
        }
        #endregion
        
      
        private void Update()
        {
            ShouldPowerUp();
            
            if (Input.GetKeyDown(KeyCode.I) && !_isInvOpen)
            {
                OpenInventory();
            }
            else if (Input.GetKeyDown(KeyCode.I) && _isInvOpen)
            {
                CloseInventory();
            }
            
            
            if (ShouldPowerUp() && Input.GetKeyDown(KeyCode.P))
            {
                _playerCanPowerup = true;
                _hadCrystals -= requiredCrystalsNumber;
                _hadTails -= requiredTailsNumber;
                powerUp.SetActive(false);
            }
            else
            {
                _playerCanPowerup = false;
            }

            HasSeal();
            
        }

        public bool PlayerCanPowerup() => _playerCanPowerup;
        
        public void OnTriggerEnter(Collider other)
        {
            var collidedItem = other.GetComponent<Item>();
            if (collidedItem)
            {
                inventory.AddItem(collidedItem.item, 1);
                Destroy(collidedItem.gameObject);

                switch (collidedItem.GetItemType())
                {
                    case "Crystal":
                        _crystalNumber++;
                        _hadCrystals++;
                        crystalsCollectedPopUp.text ="x " + _crystalNumber.ToString();
                        if (!_isInvOpen)
                        {
                            StartCoroutine(ShowCrystalPopup());
                        }
                        break;
                    case "Tail":
                        crystalEffect.Play(); 
                        _tailNumber++;
                        _hadTails++;
                        tailsCollectedPopUp.text ="x " + _tailNumber.ToString();
                        if (!_isInvOpen)
                        {
                            StartCoroutine(ShowTailPopup());
                        }
                        break;
                    case "MiniHeal":
                        healEffect.Play();
                        _player.playerHealthSystem.Heal(20); 
                        
                        break;
                    case "MaxHeal":
                        healEffect.Play();
                        _player.playerHealthSystem.Heal(100);
                        break;
                    
                    case "Seal":
                        _hasSeal = true;
                        break;
                }
            }
        }

        #region Pop Up Coroutines

        private IEnumerator ShowCrystalPopup()
        {
            crystalsPopup.SetActive(true);
            inventoryAnimator.SetTrigger(CrystalPop);    
            yield return new WaitForSeconds(1);
            crystalsPopup.SetActive(false);
            
        }

        private IEnumerator ShowTailPopup()
        {
            tailsPopup.SetActive(true);
            inventoryAnimator.SetTrigger(TailPop); 
            yield return new WaitForSeconds(1); 
            tailsPopup.SetActive(false);
                
        }
    
        #endregion
        
        #region Open - Close Inventory

        private bool OpenInventory()
        { 
            inventoryAnimator.SetTrigger(Open);
            return _isInvOpen = true;
        }

        private bool CloseInventory()
        {
            inventoryAnimator.SetTrigger(Close);
            return _isInvOpen = false;
        }

        #endregion
        
        #region Powerup Display

        private bool ShouldPowerUp()
        {
            if (requiredTailsNumber <= _hadTails && requiredCrystalsNumber <= _hadCrystals)
            {
                PowerActivated();
                return true;
            }
            PowerDeactivated();
            return false;
        }

        private void PowerActivated()
        {
            hadCrystalsNumber.color = Color.red;
            hadTailsNumber.color = Color.red;
            requiredTails.color = Color.red;
            requiredCrystals.color =Color.red;
            powerUp.SetActive(true);
            notificationMinimized.SetActive(true);
            notificationMaximized.SetActive(true);
        }

        private void PowerDeactivated()
        {
            notificationMinimized.SetActive(false);
            notificationMaximized.SetActive(false);
            hadTailsNumber.color = Color.white;
            hadCrystalsNumber.color = Color.white;
            requiredTails.color = Color.white;
            requiredCrystals.color =Color.white;
        }

        #endregion
        
        public void DisplayInventory()
        {
            tailsCollected.text = _tailNumber.ToString();
            crystalsCollected.text = _crystalNumber.ToString();

            hadCrystalsNumber.text = _hadCrystals.ToString();
            hadTailsNumber.text = _hadTails.ToString();
        }
    
        private void OnApplicationQuit()
        {
            inventory.inventoryContainer.Clear();
        }

        public bool HasSeal() => _hasSeal;
    }
}
