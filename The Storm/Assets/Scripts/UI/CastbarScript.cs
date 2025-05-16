using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CastbarScript : MonoBehaviour
{

    [Header("Components")]
    private Casting _c;

    [Header("Variables")]
    [SerializeField] private GameObject castbar;
    [SerializeField] private Image castbarFG;
    [SerializeField] private Image spellImage;
    [SerializeField] private TMP_Text spellName;
    [SerializeField] private TMP_Text timerText; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _c = GetComponentInParent<Casting>();
    }

    // Update is called once per frame
    void Update()
    {
           if (_c.IsCasting)
        {
            UpdateCastbar();
        }
    }

    public void ToggleCastbar()
    {
        if (!castbar.activeSelf)
        {
            castbar.SetActive(true);
        }
        else
        {
            castbar.SetActive(false);
        }
    }

    void UpdateCastbar()
    {
        spellName.text = _c.CurrentSpell.spellName;
        spellImage.sprite = _c.CurrentSpell.spellImage;

        castbarFG.fillAmount = _c.CurrentCastingTime / _c.CastingTime;

    }



}
