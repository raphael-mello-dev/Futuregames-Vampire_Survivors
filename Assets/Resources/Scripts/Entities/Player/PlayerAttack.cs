using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform weaponTransform;

    [SerializeField] private float distanceFromPlayer = 1.0f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float offset;

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = 0;

        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 desiredPosition = transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * distanceFromPlayer;

        weaponTransform.position = Vector3.Lerp(weaponTransform.position, desiredPosition, rotationSpeed * Time.deltaTime);

        PointWeaponAtCursor(mousePosition);
    }

    private void PointWeaponAtCursor(Vector3 mousePosition)
    {
        Vector3 direction = mousePosition - weaponTransform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        weaponTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
    }
}