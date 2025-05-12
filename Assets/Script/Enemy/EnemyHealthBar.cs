using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // 체력바로 사용할 Image 컴포넌트 (Inspector에서 연결)
    [SerializeField]
    private Image fillImage;

    private float maxHealth;

    /// <summary>
    /// 최대 체력을 설정하고 체력바를 초기화합니다.
    /// </summary>
    /// <param name="health">최대 체력 값</param>
    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        SetHealth(health);  // 초기 체력은 최대 체력으로 설정
    }

    /// <summary>
    /// 현재 체력에 맞게 체력바를 업데이트합니다.
    /// </summary>
    /// <param name="currentHealth">현재 체력 값</param>
    public void SetHealth(int currentHealth)
    {
        if (maxHealth <= 0)
        {
            Debug.LogWarning("maxHealth가 0 이하입니다. 올바른 값을 설정해주세요.");
            return;
        }
        // 체력에 따라 채워지는 양 계산 (0 ~ 1)
        float fillAmount = (float)currentHealth / maxHealth;
        fillImage.fillAmount = fillAmount;
    }
}
