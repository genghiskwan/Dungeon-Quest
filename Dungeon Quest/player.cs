using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Quest
{
    internal class PlayerCharacter
    {
        private int m_health = 17;
        private int m_maxHealth = 17;
        private int m_mana = 10;
        private int m_block = 0;
        private int m_attack = 3;
        private int m_attackBouns = 0;
        private int m_blizzard = 5;
        private int m_blizzardBonus = 0;
        private bool m_defendBonus = false;
        private string m_characterName;
        private bool m_hasShield = false;
        private int m_healthPotion = 1;
        private int m_manaPotion = 1;

        public PlayerCharacter(string characterName)
        {
            m_characterName = characterName;
        }
        public string GetCharacterName()
        {
            return m_characterName;
        }
        
        public void HealthLvlUp()
        {
            m_maxHealth += 2;
        }

        public void IncreaseHealth()
        {
            m_health += 4;
        }
        public void HealthPotion()
        {
            if ((m_health + 8) < m_maxHealth)
            {
                m_health = m_health + 8;
                m_healthPotion--;
            }
            else if ((m_health + 8) > m_maxHealth)
            {
                m_health = m_maxHealth;
                m_healthPotion--;
            }
        }
        public void DungeonHeal()
        {
            m_health = m_maxHealth;
        }
        public void ManaPotion()
        {
            if (m_mana < 7)
            {
                m_mana = m_mana + 3;
                m_manaPotion--;
            }
            else if (m_health >= 7)
            {
                m_mana = 10;
                m_manaPotion--;
            }
        }
        public void AddHealtPotion()
        {
            m_healthPotion++;
        }

        public void AddManaPotion()
        {
            m_manaPotion++;
        }
        public void Health(int healthStatus)
        {
            m_health += healthStatus;
        }
        public void Mana(int manaStatus)
        {
            m_mana += manaStatus;
        }
        public void Block(int value)
        {
            m_block = value;
        }
        public void BonusAttack(int attkBonus) 
        {
            m_attackBouns = attkBonus;
            m_attack += m_attackBouns;
        }
        public void BlizzardBonus(int blzBonus)
        {
            m_blizzardBonus = blzBonus;
            m_blizzard += m_blizzardBonus;
        }

        public void ShieldEquipped()
        {
            m_hasShield = true;
        }
        public int GetHealth()
        {
            return m_health;
        }
        public int GetMana()
        {
            return m_mana;
        }
        public int GetBlock()
        {
            return m_block;
        }        
        public int PlayerAttack()
        {
            
            return m_attack;
        }
        public int GetBlizzard()
        {
            
            return m_blizzard;
        }

        public int GetHealthPotion()
        {
            return m_healthPotion;
        }

        public int GetManaPotion()
        {
            return m_manaPotion;
        }
        public bool PlayerDefenseBonus()
        {
            return m_defendBonus;
        }

        public bool HasShield()
        {
            return m_hasShield;
        }
    }
}
