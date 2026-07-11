using TMPro;
using UnityEngine;

[System.Serializable]
public class AttackController : MonoBehaviour
{
    private AttackData runtimeAttackData;
    public AttackData RuntimeAttackData { get { return runtimeAttackData; } set { if (value != null) { runtimeAttackData = value; } } }

    private MovementDriver movementDriver;

    private GameObject targetObject;
    private Vector3 targetLocation;
    private string targetTag;
    private Vector2 targetDirection;
    
    [SerializeField] private DamageData damageDataTemplate;
    private DamageData runtimeDamageData;

    private Vector3 spawnPosition;
    private float distanceTraveled = 0f;

    [SerializeField] protected GameObject tempDisplayTextPrefab;
    protected TextMeshPro tempDisplayText;

    private void Awake()
    {
        if (damageDataTemplate != null)
        {
            runtimeDamageData = Instantiate(damageDataTemplate);
        }

        movementDriver = GetComponent<MovementDriver>();

        tempDisplayText = Instantiate(tempDisplayTextPrefab).GetComponent<TextMeshPro>();

        spawnPosition = transform.position;
    }

    private void Update()
    {
        tempDisplayText.gameObject.transform.position = transform.position;
        string textToDisplay = runtimeAttackData.GetAttackValuesString();
        textToDisplay += $"Target Object: {targetObject}\n";
        textToDisplay += $"Target Location: {targetLocation}\n";
        textToDisplay += $"Target Tag: {targetTag}\n";
        textToDisplay += $"Target Direction: {targetDirection}\n";
        tempDisplayText.SetText(textToDisplay);

        distanceTraveled = (spawnPosition - transform.position).sqrMagnitude;
        if (distanceTraveled >= runtimeAttackData.AttackRangeMaximum)
        {
            Destroy(tempDisplayText.gameObject);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (runtimeAttackData.HomesOntoTarget && targetObject != null)
        {
            targetDirection = (targetObject.transform.position - transform.position).normalized;
        }

        movementDriver.Move(targetDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            DamageProcessor.ProcessDamageToTarget(collision.gameObject, runtimeDamageData);
            Destroy(tempDisplayText.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject targetsObject)
    {
        if (targetsObject == null)
        {
            return;
        }

        targetObject = targetsObject;
        targetLocation = targetsObject.transform.position;
        targetTag = targetsObject.tag;
        targetDirection = (targetLocation - transform.position).normalized;
    }
}
