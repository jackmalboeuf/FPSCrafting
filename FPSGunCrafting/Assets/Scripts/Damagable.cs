using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Damagable : MonoBehaviour
{
    [SerializeField]
    GameObject damageTextPrefab;
    [SerializeField]
    float maxHealth;

    float currentHealth;
    Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = healthSlider.maxValue;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        Transform damageNumberSpawn = transform.Find("DamageNumberSpawn");
        GameObject damageText = Instantiate(damageTextPrefab, damageNumberSpawn.position, damageNumberSpawn.rotation, damageNumberSpawn) as GameObject;
        damageText.GetComponent<TextMesh>().text = damage.ToString();
        damageText.GetComponent<Rigidbody>().AddRelativeForce(Random.Range(-150, 150), 300, 0);
    }
}
