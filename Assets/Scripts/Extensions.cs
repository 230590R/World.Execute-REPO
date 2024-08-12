using System.Linq;
using UnityEngine;

namespace ExtensionMethods {
  public static class VectorExtensions {
    /// <summary>
    /// Returns if the Vector is approximately zero. Implemented using Mathf.Approximately.
    /// </summary>
    public static bool IsZero(this Vector2 vec2) {
      return vec2.x.IsZero() && vec2.y.IsZero();
    }

    /// <summary>
    /// Returns if the float is approximately zero. Implemented using Mathf.Approximately.
    /// </summary>
    public static bool IsZero(this float f) {
      return Mathf.Approximately(f, 0f);
    }

    /// <summary>
    /// Clamps the vector within the provided range.
    /// </summary>
    public static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max) {
      v.x = Mathf.Clamp(v.x, min.x, max.x);
      v.y = Mathf.Clamp(v.y, min.y, max.y);
      return v;
    }
  }

  public static class GameObjectExtensions {
    public static void DestroyChildren(this GameObject t) {
      t.transform.Cast<Transform>().ToList().ForEach(c => Object.Destroy(c.gameObject));
    }

    public static void DestroyChildrenImmediate(this GameObject t) {
      t.transform.Cast<Transform>().ToList().ForEach(c => Object.DestroyImmediate(c.gameObject));
    }
  }


}
