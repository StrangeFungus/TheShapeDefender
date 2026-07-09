using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerController : BaseEntityController
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private InputAction movementAction;

    private void Start()
    {
        
    }
    /*
     // HEALTH RELATED


    // DEFENSE RELATED
    [SerializeField] private float parryChance = 0;
    [SerializeField] private float parryCooldown = 0;
    [SerializeField] private float counterAttackChance = 0;

    [SerializeField] private float blockChance = 0;
    [SerializeField] private float blockCooldown = 0;
    [SerializeField] private float blockAmount = 0;
    [SerializeField] private float reflectAttackChance = 0;
    [SerializeField] private float reflectAttackAngle = 0;

    [SerializeField] private float dodgeChance = 0;
    [SerializeField] private float dodgeCooldown = 0;

    // RESISTANCE RELATED
    [SerializeField] private float armorValue = 0;
    [SerializeField] private float thornsAmount = 0;

    [SerializeField] private float criticalHitResist = 0;
    [SerializeField] private float criticalHitDamageResist = 0;

    [SerializeField] private float statusEffectResist = 0;
    [SerializeField] private float physicalDamageResist = 0;
    [SerializeField] private float magicalDamageResist = 0;
    [SerializeField] private float energyDamageResist = 0;

    // MOVEMENT RELATED
    [SerializeField] private float movementSpeed = 0;

    [SerializeField] private float dashSpeed = 0;
    [SerializeField] private float dashDistance = 0;
    [SerializeField] private float currentDashCharges = 0;
    [SerializeField] private float maxDashCharges = 0;
    [SerializeField] private float dashCooldown = 0;

    // UTILITY RELATED
    [SerializeField] private float experienceGainMultiplier = 0;
    [SerializeField] private float constructionPointsMultiplier = 0;
     */
}
