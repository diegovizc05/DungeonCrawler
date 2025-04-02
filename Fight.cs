using UnityEngine;
 using UnityEngine.InputSystem.Interactions;
 
 public class Fight
 {
     private Inhabitant attacker;
     private Inhabitant defender;
     private Player defender2;
     private Player attacker2;

 
     public Fight()
     {
            
     }
     private Monster theMonster;
 
     private bool fightOver = false;
 
     public Fight(Monster m)
     {
        this.theMonster = m;
        //initially determine who goes first
        int roll = Random.Range(0, 20) + 1;
        if (roll <= 10)
        {
            Debug.Log("Monster goes first");
            this.attacker =  m;
            this.defender2 = Core.thePlayer;
        }
        else
        {
            Debug.Log("Player goes first");
            this.attacker2 = Core.thePlayer;
            this.defender = m;
        }
 
     }
 
     public void startFight()
     {

     }
     public bool isFightOver()
     {
        return this.fightOver;
     }

         public void takeTurn(string action)
    {
        if (this.fightOver)
        {
            Debug.Log("The fight is already over!");
            return;
        }

        switch (action.ToLower())
        {
            case "power attack":
                performPowerAttack();
                break;

            case "normal attack":
                performNormalAttack();
                break;

            case "drink potion":
                drinkPotion();
                break;

            default:
                Debug.Log("Sorry, wrong input!");
                    break;
            }
        }
    
        private void performPowerAttack()
    {
        Debug.Log("Performing a power attack!");

        int attackRoll = Random.Range(0, 20) + 1;
        attackRoll = Mathf.FloorToInt(attackRoll * 0.75f); // Reduce attack roll by 25%
        Debug.Log("Power Attack Roll: " + attackRoll);
        Debug.Log("Defender AC: " + this.defender.getAC());

        if (attackRoll >= this.defender.getAC())
        {
            int damage = Mathf.FloorToInt(Random.Range(1, 6) * 1.5f); // 50% more damage
            Debug.Log("Power Attack hits! Damage: " + damage);
            this.defender.takeDamage(damage);
        }
        else
        {
            Debug.Log("Power Attack missed!");
        }
    }

    private void performNormalAttack()
    {
        Debug.Log("Performing a normal attack!");

        int attackRoll = Random.Range(0, 20) + 1;
        Debug.Log("Normal Attack Roll: " + attackRoll);
        Debug.Log("Defender AC: " + this.defender.getAC());

        if (attackRoll >= this.defender.getAC())
        {
            int damage = Random.Range(1, 6); // Normal damage
            Debug.Log("Normal Attack hits! Damage: " + damage);
            this.defender.takeDamage(damage);
        }
        else
        {
            Debug.Log("Normal Attack missed!");
        }
    }

    private void drinkPotion()
    {
        Debug.Log(Core.thePlayer.getName() + " drank a potion!");

        int maxHp = Core.thePlayer.getMaxHp();
        int healAmount = Mathf.FloorToInt(maxHp * 0.25f); // Heal 25% of max HP
        int newHp = Mathf.Min(Core.thePlayer.getCurrHp() + healAmount, maxHp); // Ensure HP does not exceed max HP

        Debug.Log(Core.thePlayer.getName() + " healed for " + healAmount + " HP. New HP: " + newHp);
        Core.thePlayer.currHp = newHp; // Update the player's current HP
    }
 
     public void takeASwing(GameObject playerGameObject, GameObject monsterGameObject)
     {
        int attackRoll = Random.Range(0, 20) + 1;
        Debug.Log("Attack Roll: " + attackRoll);
        Debug.Log("Defender AC: " + this.defender.getAC());
         
        if(attackRoll >= this.defender.getAC())
        {
            //attacker hits the defender
            int damage = Random.Range(1, 6); //1 to 5 damage
            this.defender.takeDamage(damage);
            
            if(this.defender.isDead())
            {
                this.fightOver = true;
                Debug.Log(this.attacker.getName() + " killed " + this.defender.getName());
                if(this.defender != null && this.defender.GetType() == typeof(Player))
                 {
                    //player died
                    Debug.Log("Player died");
                    //end the game
                    playerGameObject.SetActive(false); //hide the player
                }
                else
                {
                    //monster died
                    Debug.Log("Monster died");
                    //remove the monster from the scene
                    GameObject.Destroy(monsterGameObject); //remove the monster from the scene
                }
             }
         }
         else
         {
            Debug.Log(this.attacker.getName() + " missed " + this.defender.getName());
         }
 
        Inhabitant temp = this.attacker;
        this.attacker = this.defender;
        this.defender = temp;
     }
 
     public void startFight(GameObject playerGameObject, GameObject monsterGameObject)
     {
        //should have the attacker and defender fight each until one of them dies.
        //the attacker and defender should alternate between each fight round and
        //the one who goes first was determined in the constructor.
        while(true)
        {
            int attackRoll = Random.Range(0, 20) + 1;
            //int healthPotion = 
            if(attackRoll >= this.defender.getAC())
            {
                //attacker hits the defender
                int damage = Random.Range(1, 6); //1 to 5 damage
                this.defender.takeDamage(damage);

                if(this.defender.isDead())
                {
                    Debug.Log(this.attacker.getName() + " killed " + this.defender.getName());
                    if(this.defender != null && this.defender.GetType() == typeof(Player))
                    {
                        //player died
                        Debug.Log("Player died");
                        //end the game
                        playerGameObject.SetActive(false); //hide the player
                    }
                    else
                    {
                        //monster died
                        Debug.Log("Monster died");
                        //remove the monster from the scene
                        GameObject.Destroy(monsterGameObject); //remove the monster from the scene
                    }
                    break; //fight is over
                }
            }
            else
            {
                Debug.Log(this.attacker.getName() + " missed " + this.defender.getName());
             }
             
            Inhabitant temp = this.attacker;
            this.attacker = this.defender;
            this.defender = temp;
        }
     }
 }