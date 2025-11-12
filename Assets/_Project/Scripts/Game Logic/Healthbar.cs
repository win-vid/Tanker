using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public float health, maxHealth, width, height;

    [SerializeField]
    RectTransform healthbar;

    public void setMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void setHealth(float health)
    {
        this.health = health;
        float newWidth = (this.health / maxHealth) * width;

        healthbar.sizeDelta = new Vector2(newWidth, height);
    }
}
