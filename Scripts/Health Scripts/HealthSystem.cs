using System;
using UnityEngine;

namespace Health_Scripts
{
    public class HealthSystem : MonoBehaviour
    {

        public event EventHandler OnHealthChanged;
        public event EventHandler OnDead;
        private bool isDead = false;
  
        private int _health;
        private int _healthMax;
        public int _lives = 3;

        public void Initialize(int healthMax)
        {
            this._healthMax = healthMax;
            _health = healthMax;
        }
        
        public int GetHealth()
        {
            return _health;
        }

        public float GetHealthPercent()
        {
            return (float) _health / _healthMax;
        }

        public void Damage(int amount)
        {
            _health -= amount;
            if (_health < 0)
            {
                _health = 0;
            }
            if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);

            if (_health <= 0)
            {
                isDead = true;
            }
        }

        public void Heal(int healAmount)
        {
            _health += healAmount;
            if (_health > _healthMax) _health = _healthMax;
            if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        }

        public bool IsDead() => isDead;

        public void Reset()
        {
            _health = _healthMax;
            isDead = false;
            DecreaseLife();
        }

        private void DecreaseLife()
        {
            _lives --;
            var livesImages = GameObject.FindGameObjectsWithTag("life");
            if (livesImages.Length > 0)
            {
                Destroy(livesImages[livesImages.Length-1]);
            }
          
        }
    }
}
