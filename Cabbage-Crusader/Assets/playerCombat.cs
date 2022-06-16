using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDMG = 10;

    public float attackRate = 1f;
    float nextAttackTime = 0f;

    //health code
    public int playerMaxHealth = 100;
    public int playerCurrentHealth;
    
    public HealthBar healthBar;

    private bool isBlocking;
    private Rigidbody2D rb;

    private bool alreadyDead = false;

    //audio sounds
    public AudioSource swordSound;
    public AudioSource shieldBlock;
    public AudioSource backgroundMusic;

    public string SceneName;

    //player is an enemy for bandits
    public static List<playerCombat> enemyList = new List<playerCombat>();


    public static List<playerCombat> GetEnemyList()
    {
        return enemyList;
    }

   void Start()
    {
        backgroundMusic.Play();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        enemyList.Add(this);
 
        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
            {
                PlayerAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.X)) //block
        {
            animator.SetTrigger("Block");
            shieldBlock.Play();
            isBlocking = true;
        }
    }

 

    void PlayerAttack()
    {
        //Play animation
        animator.SetTrigger("Attack");

        swordSound.Play();

        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDMG(attackDMG);
        }
    }


    public void TakeDamage(int attackDamage)
    {


        if (!isBlocking)
        {
            playerCurrentHealth -= attackDamage;
            healthBar.SetHealth(playerCurrentHealth);

            //Play hurt animation
            animator.SetTrigger("PlayerHurt");
            if (transform.rotation.y == 0)
            {
                transform.Translate(-0.2f, 0, 0);
                transform.Translate(-0.2f, 0, 0);
                transform.Translate(-0.2f, 0, 0);
                transform.Translate(-0.2f, 0, 0);
                transform.Translate(-0.2f, 0, 0);
            }
            else
            {
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
                transform.Translate(0.2f, 0, 0);
            }

        }
        else
        {
            isBlocking = false;
        }
        if (playerCurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (alreadyDead == false)
        {
            enemyList.Remove(this);

            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;

            animator.SetTrigger("Death");
            Debug.Log("Player Dead");

            alreadyDead = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneName);
    }

    void OnDestroy()
    {
        enemyList.Remove(this);
    }
}

