using TMPro;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    [SerializeField] private HealthData healthDataTemplate;
    protected HealthData runtimeHealthData;
    public HealthData RuntimeHealthData { get { return runtimeHealthData; } }

    [SerializeField] private DefenseData defenseDataTemplate;
    protected DefenseData runtimeDefenseData;
    public DefenseData RuntimeDefenseData { get { return defenseDataTemplate; } }

    protected MovementDriver movementDriver;
    protected AttackContainer attackContainer;


    [SerializeField] protected GameObject tempDisplayTextPrefab;
    protected TextMeshPro tempDisplayText;

    protected void Awake()
    {
        if (healthDataTemplate != null)
        {
            runtimeHealthData = Instantiate(healthDataTemplate);
        }

        if (defenseDataTemplate != null)
        {
            runtimeDefenseData = Instantiate(defenseDataTemplate);
        }

        movementDriver = GetComponent<MovementDriver>();
        attackContainer = GetComponent<AttackContainer>();

        tempDisplayText = Instantiate(tempDisplayTextPrefab).GetComponent<TextMeshPro>();
    }

    protected void Update()
    {
        tempDisplayText.gameObject.transform.position = transform.position;
        tempDisplayText.SetText(runtimeHealthData.GetHealthValuesString());
        attackContainer.AttemptToUseAttacks();

        if (runtimeHealthData != null)
        {
            runtimeHealthData?.UpdateHealthBarLocation(transform.position + new Vector3(0.0f, 4.0f, 0.0f));
        }

        if (runtimeHealthData.IsDead)
        {
            Die();
        }
    }

    protected void Die()
    {
        gameObject.SetActive(false);
    }
}
