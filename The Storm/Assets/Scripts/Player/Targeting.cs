using UnityEngine;

public class Targeting : MonoBehaviour
{

    private HealthbarScript healthbarScript;

    [SerializeField] private Targeting previousTarget;
    [SerializeField] private Targeting currentTarget; public Targeting CurrentTarget { get => currentTarget; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthbarScript = GetComponentInChildren<HealthbarScript>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Untarget()
    {
        if (currentTarget != null)
        {
            currentTarget.OnUntargeted();
            currentTarget = null;
        }
    }

    public void Target(Targeting target)
    {
        if (currentTarget != null)
        {
            currentTarget.OnUntargeted();
            currentTarget = null;
        }
        currentTarget = target;
        target.OnTargeted();
    }

    public void OnTargeted()
    {

        this.healthbarScript.ChangeHealthbarColor(true);
    }

    public void OnUntargeted()
    {
        this.healthbarScript.ChangeHealthbarColor(false);
    }
}
