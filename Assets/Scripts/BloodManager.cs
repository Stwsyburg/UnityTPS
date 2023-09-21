using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodManager : MonoBehaviour
{
    private Image BloodFill;
    public static int CurrentBlood;
    public static int MaxBlood;
    // Start is called before the first frame update
    void Start()
    {
        BloodFill = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        BloodFill.fillAmount = (float)CurrentBlood / (float)MaxBlood;
    }
}
