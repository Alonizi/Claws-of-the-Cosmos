using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color High;
    public Vector3 offset;

    public void SetHealth(float health , float maxHealth){
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health ;
        slider.maxValue = maxHealth ;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low,High,slider.normalizedValue);
    }

    
}
