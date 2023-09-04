using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UHD : MonoBehaviour
{
    private static UHD instance;
    public static UHD GetInsatance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    public Image weaponIcon;
    public Image playerIcon;
    public Text hpNum;
    public void UpdateWeaponUI(Sprite icon)
    {
        weaponIcon.sprite = icon;
    }
    public void UpdateHpNum(int hp_num)
    {
        hpNum.text = hp_num.ToString();
    }
    // Start is called before the first frame update
    
}
