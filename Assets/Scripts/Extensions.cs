using UnityEngine;

public static class Extensions {

    private static LayerMask layerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction) {
        if (rigidbody.isKinematic) {
            return false;
        }

        float radius = 0.35f;
        float distance = 0.5f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 direction) {
        Vector2 toOther = other.position - transform.position;
        return Vector2.Dot(toOther.normalized, direction.normalized) > 0.45f;
    }
}