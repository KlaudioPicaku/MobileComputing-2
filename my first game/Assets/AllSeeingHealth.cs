using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AllSeeingHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject energyPointsPrefab;
    [SerializeField] float previousHealth;
    [SerializeField] float timeUp = 0f;
    PlayerScript player;

    private Animator animator;
    private bool isDead = false;
    private void Awake()
    {
        string path2 = Application.persistentDataPath + "/TEMP" + "/" + this.gameObject.name + ".enemy";
        if (File.Exists(path2))
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        previousHealth = currentHealth;
    }
    private void Update()
    {

        if (currentHealth <= 0 && !isDead)
        {
            player.eyesKilled++;
            DieEye();
            isDead = true; //avoid animation loop
        }
        if (isDead)
        {
            timeUp += Time.deltaTime;
            if (timeUp >= 10)
            {
                this.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
    void hideElement()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }
    private void DieEye()
    {
        if (!isDead)
        {
            GameObject a = Instantiate(energyPointsPrefab, GetComponent<Transform>()) as GameObject;
            //Destroy object
            animator.Play("AllSeeing_die");
            animator.SetBool("IsDead", true);
            GetComponent<AllSeeingAI>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            //this.enabled = false;
            //Destroy(GameObject.FindWithTag("AllSeeingRoot"),10f);
            player.enemiesKilled.Add(this.gameObject.name);
            isDead = true;
        }

    }
    public void setHealth(float health)
    {
        currentHealth = currentHealth - health;
    }
}
