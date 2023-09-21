using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    private Image ExpFill;
    public static float CurrentExp;
    public static float MaxExp;
    // Start is called before the first frame update
    void Start()
    {
        ExpFill = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ExpFill.fillAmount = (float)CurrentExp / (float)MaxExp;
    }
}
