using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public Button speedCardButton;
    public Button speedBallCardButton;
    public Button healthCardButton;

    public GameObject cardPanel;

    private TopDownCharacterMover character;
    private StickController stickController;
    private PlayerStats playerStats;

    private void Start()
    {
        
        speedCardButton.onClick.AddListener(() => CardSelected(Card.speed));
        speedBallCardButton.onClick.AddListener(() => CardSelected(Card.speedBall));
        healthCardButton.onClick.AddListener(() => CardSelected(Card.health));

        
        cardPanel.SetActive(false);

        character = FindObjectOfType<TopDownCharacterMover>();
        stickController = FindObjectOfType<StickController>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void CardSelected(Card card)
    {
        
        switch (card)
        {
            case Card.speed:
                character.IncreaseSpeed();
                break;
            case Card.speedBall:
                stickController.TopSpeedIncrease();
                break;
            case Card.health:
                playerStats.TakeDamagePercentage(-20f);
                break;
            default:
                break;
        }

        // Kart seçildiðinde paneli gizle
        gameObject.SetActive(false);
    }
}

public enum Card
{
    speed,
    speedBall,
    health
}

