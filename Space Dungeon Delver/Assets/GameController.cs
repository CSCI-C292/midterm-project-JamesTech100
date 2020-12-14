using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static int health = 5;
    private static int maxHealth = 5;

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }

    [SerializeField] public Text healthText;
    //private bool GameOver = false;
    public RectTransform gameOverPanel;
    public Text gameOverText;
    public event EventHandler GameOverEvent;

    public Player thePlayer;

    private void OnGameOver()
    {
        if (GameOverEvent != null)
            GameOverEvent(this, EventArgs.Empty);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start() 
    {
        GameController.instance.GameOverEvent += OnGameOverEvent;
    }

    private void OnGameOverEvent(object sender, System.EventArgs e)
    {
        gameOverText.gameObject.SetActive(true);
        gameOverPanel.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;

        if(Health <= 0)
        {
            health = 0;
            KillPlayer();
        }

    }

    public static void HealPlayer(int healAmount)
    {
        Health = Mathf.Min(maxHealth, health + healAmount);
    }

    private static void KillPlayer()
    {
        instance.gameOverText.gameObject.SetActive(true);
        instance.gameOverPanel.gameObject.SetActive(true);
        instance.thePlayer.gameObject.SetActive(false);
    }

    public bool IsWon
    {
        get
        {
            if(health <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
