using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SliceableV2 : MonoBehaviour {
  public SliceableV2 Prefab;

  // the slice force applied after a slice
  [SerializeField] private float sliceforceParallel = 5f;
  [SerializeField] private float sliceforcePerpendicular = 2f;

  // references to other components
  public SpriteRenderer m_SpriteRenderer;
  private Sprite m_Sprite;
  private PolygonCollider2D m_Collider;
  private Rigidbody2D m_Rigidbody2D;

  // variables for fade out
  [SerializeField] private float _cullAreaThreshold = 0.01f;
  private float area;
  [SerializeField] private float _lifetime = 995f;
  private float _life;

  private float originalMass;

  private void Awake() {
    _life = _lifetime;
    m_Collider = GetComponent<PolygonCollider2D>();
    m_Rigidbody2D = GetComponent<Rigidbody2D>();

    // create a new sprite
    m_SpriteRenderer = (m_SpriteRenderer == null) ? GetComponent<SpriteRenderer>() : m_SpriteRenderer;
    Sprite originalSprite = m_SpriteRenderer.sprite;
    Texture2D originalTex = originalSprite.texture; // original


    var test = originalTex.height;
    var test2 = originalSprite.pixelsPerUnit;

    //m_SpriteRenderer.sprite = Sprite.Create(originalTex, new Rect(0, 0, originalTex.width, originalTex.height), new Vector2(0.5f, 0.5f), originalTex.height, 0, SpriteMeshType.Tight);
    
    m_SpriteRenderer.sprite = Sprite.Create(originalTex, originalSprite.rect, new Vector2(0.5f, 0.5f), originalSprite.pixelsPerUnit, 0, SpriteMeshType.Tight);

    m_Sprite = m_SpriteRenderer.sprite;



  }

  private void Start() {
    originalMass = m_Rigidbody2D.mass;
    Vector2[] originalVerts = m_Sprite.vertices;
    for (int i = 0; i < originalVerts.Length; i++) {
      originalVerts[i] += new Vector2(0.5f, 0.5f);
    }
    ClockwiseSort.SortVerts(originalVerts);

    Vector2[] colliderverts = new Vector2[originalVerts.Length];
    for (int i = 0; i < originalVerts.Length; i++) {
      colliderverts[i] = originalVerts[i] - new Vector2(0.5f, 0.5f);
    }
    m_Collider.points = colliderverts;
    area = GetArea();
  }

  private void Update() {

    // despawn object
    if (_life > 0 && area <= _cullAreaThreshold) {
      float opacity = _life / _lifetime;
      _life -= Time.deltaTime;

      Color oldcolour = m_SpriteRenderer.material.color;
      m_SpriteRenderer.material.color = new Color(oldcolour.r, oldcolour.g, oldcolour.b, opacity);
      m_SpriteRenderer.color = new Color(oldcolour.r, oldcolour.g, oldcolour.b, opacity);
    }

    if (_life <= 0) {
      Destroy(gameObject);
    }
  }

  private void OnEnable() {
    area = GetArea();
  }

  private float GetArea() {
    Vector2[] verts = m_Sprite.vertices;
    ClockwiseSort.SortVerts(verts);

    float accumulator = 0;
    float up_acc = 0;
    float down_acc = 0;

    for (int i = verts.Length - 1; i >= 0; i--) {
      int next = (i != 0) ? (i - 1) : verts.Length - 1;

      up_acc += verts[i].y * verts[next].x;
      down_acc += verts[i].x * verts[next].y;

      accumulator += verts[i].x * verts[next].y;
      accumulator += verts[next].x * verts[i].y;
    }

    return (0.5f * Mathf.Abs(up_acc - down_acc));
  }


  public void Slice(Ray2D slice) {
    Debug.Log("SLICE");
    if (area < _cullAreaThreshold) return;

    // convert the ray into local space
    slice.origin = (Vector2)transform.InverseTransformPoint(slice.origin) + new Vector2(0.5f, 0.5f);

    // rotate the ray 
    float angle = transform.rotation.eulerAngles.z;
    slice.direction = Quaternion.Euler(0, 0, -angle) * slice.direction;

    // slice and create the sliced half
    Vector2[] positive, negative = { };
    if (!InternalSlice(slice, out positive, out negative)) return;

    CreateSlicedObject(positive);
    CreateSlicedObject(negative);

    //// edit self
    //Vector2 normal = new Vector2(slice.direction.y, -slice.direction.x);
    //GenerateSprite(positive);
    //m_Rigidbody2D.mass = area * originalMass;
    //m_Rigidbody2D.AddForce((-normal * sliceforcePerpendicular * m_Rigidbody2D.mass) + (slice.direction * sliceforceParallel * m_Rigidbody2D.mass), ForceMode2D.Impulse);


    ////// create new child
    //GameObject child = Instantiate(Prefab.gameObject, transform.position, transform.rotation);

    //SliceableV2 childSliceable = child.GetComponent<SliceableV2>();
    //Rigidbody2D childRB = child.GetComponent<Rigidbody2D>();
    //childSliceable.GenerateSprite(negative);
    //childRB.mass = childSliceable.area * originalMass;
    //childRB.AddForce((normal * sliceforcePerpendicular * childRB.mass) + (-slice.direction * sliceforceParallel * 0.5f * childRB.mass), ForceMode2D.Impulse);
  }

  private void CreateSlicedObject(Vector2[] verts) {
    GameObject newObj = new GameObject();
    var sprRenderer = newObj.AddComponent<SpriteRenderer>();
    sprRenderer.sprite = m_Sprite;
    newObj.AddComponent<Rigidbody2D>();
    var objSliceable = newObj.AddComponent<SliceableV2>();
    objSliceable.GenerateSprite(verts);
    Destroy(objSliceable);
  }

  public void NoPrefabSlice(Ray2D slice) {
    if (area < _cullAreaThreshold) return;

    // convert the ray into local space
    slice.origin = (Vector2)transform.InverseTransformPoint(slice.origin) + new Vector2(0.5f, 0.5f);

    // rotate the ray 
    float angle = transform.rotation.eulerAngles.z;
    slice.direction = Quaternion.Euler(0, 0, -angle) * slice.direction;

    // slice and create the sliced half
    Vector2[] positive, negative = { };
    if (InternalSlice(slice, out positive, out negative)) {
      // edit self
      Vector2 normal = new Vector2(slice.direction.y, -slice.direction.x);
      GenerateSprite(positive);
      m_Rigidbody2D.mass = area * originalMass;
      m_Rigidbody2D.AddForce((-normal * sliceforcePerpendicular * m_Rigidbody2D.mass) + (slice.direction * sliceforceParallel * m_Rigidbody2D.mass), ForceMode2D.Impulse);




      GameObject child = new GameObject();
      SliceableV2 childSliceable = child.AddComponent<SliceableV2>();
      Rigidbody2D childRB = child.AddComponent<Rigidbody2D>();
      SpriteRenderer childSpriteRenderer = child.AddComponent<SpriteRenderer>();
      childSpriteRenderer.sprite = m_SpriteRenderer.sprite;
      childSliceable.m_SpriteRenderer = childSpriteRenderer;

      childSliceable.GenerateSprite(negative);
      childRB.mass = childSliceable.area * originalMass;
      childRB.AddForce((normal * sliceforcePerpendicular * childRB.mass) + (-slice.direction * sliceforceParallel * 0.5f * childRB.mass), ForceMode2D.Impulse);

      //GameObject child = Instantiate(Prefab.gameObject, transform.position, transform.rotation);

      //Sliceable childSliceable = child.GetComponent<Sliceable>();
      //Rigidbody2D childRB = child.GetComponent<Rigidbody2D>();
      //childSliceable.GenerateSprite(negative);
      //childRB.mass = childSliceable.area * originalMass;
      //childRB.AddForce((normal * sliceforcePerpendicular * childRB.mass) + (-slice.direction * sliceforceParallel * 0.5f * childRB.mass), ForceMode2D.Impulse);
    }

  }


  /// <summary>
  /// Slices the mesh, generating an instantiation and changing the parent.
  /// </summary>
  /// <param name="ray">Ray to define the 2D slice. Must be in local space (0-1)</param>
  /// <param name="Vector2[]">Outputs the two new sets of vertices.</param>
  /// <returns>Returns if there is an intersection</returns>
  private bool InternalSlice(Ray2D ray, out Vector2[] out_pos, out Vector2[] out_neg) {
    // create a plane from the ray
    Vector2 raynormal = new Vector2(-ray.direction.y, ray.direction.x);
    Plane slice = new Plane(raynormal, ray.origin);
    Vector2 posPoint = ray.origin + raynormal;
    List<Vector2> positive = new List<Vector2>();
    List<Vector2> negative = new List<Vector2>();

    // Sprite.verticies are stored from -0.5 to 0.5; convert to 0-1, then sort for good measure (optional?)
    Vector2[] originalVerts = m_Sprite.vertices;
    float PPU = m_Sprite.pixelsPerUnit;
    var scaleX = PPU / m_Sprite.rect.width;
    var scaleY = PPU / m_Sprite.rect.height;
    for (int i = 0; i < originalVerts.Length; i++) {
      originalVerts[i].x *= scaleX;
      originalVerts[i].y *= scaleY;
      originalVerts[i] += new Vector2(0.5f, 0.5f);
    }
    ClockwiseSort.SortVerts(originalVerts);

    // find the points of intersection, add to both the positive and negative
    bool hasIntersection = false;
    Vector2 slicestart = ray.origin - (ray.direction * 99f);
    Vector2 sliceend = ray.origin + (ray.direction * 99f);
    for (int i = 0; i < originalVerts.Length; i++) {
      int next = (i < originalVerts.Length - 1) ? (i + 1) : 0;

      Vector2 half = new Vector2(0.5f, 0.5f);
      Vector2 intersect = LineIntersection(originalVerts[i], originalVerts[next], slicestart, sliceend);
      if (intersect.x == -99) continue; // skip if no intersection


      hasIntersection = true;
      positive.Add(intersect);
      negative.Add(intersect);
    }

    // early return in the case of no intersection; redundancy for safety
    if (!hasIntersection) {
      out_pos = originalVerts;
      out_neg = originalVerts;
      return false;
    }

    // sort the vertices into positive and negative sides
    foreach (Vector2 v in originalVerts) {
      if (slice.SameSide(posPoint, v))
        positive.Add(v);
      else
        negative.Add(v);
    }

    // convert the lists into arrays, sorted
    out_pos = positive.ToArray();
    out_neg = negative.ToArray();
    return true;
  }

  protected static Vector2 LineIntersection(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2) {
    // rename other line's parameters for ease of use
    float x1 = start1.x; float y1 = start1.y;
    float x2 = end1.x; float y2 = end1.y;
    float x3 = start2.x; float y3 = start2.y;
    float x4 = end2.x; float y4 = end2.y;

    // get the denominator of the equation
    float denominator = ((x1 - x2) * (y3 - y4)) - ((y1 - y2) * (x3 - x4));
    // early return if 0; the two lines are parallel
    if (denominator == 0)
      return new Vector2(-99, -99);

    // calculate t and u of the equation
    float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denominator;
    float u = ((x1 - x3) * (y1 - y2) - (y1 - y3) * (x1 - x2)) / denominator;

    // return x2 and y2 if no intersection
    if (!(t < 1 && t > 0 && u < 1 && u > 0))
      return new Vector2(-99, -99);

    // return the point of intersection
    float x = x1 + t * (x2 - x1);
    float y = y1 + t * (y2 - y1);
    return new Vector2(x, y);
  }

  /// <summary>
  /// Copied code online for debugging purposes. Slightly edited to shift origin to bottom left.
  /// Credit: https://discussions.unity.com/t/how-to-debug-drawing-plane/72450/3
  /// </summary>
  public void DrawPlane(Vector3 position, Vector3 normal) {
    position -= new Vector3(0.5f, 0.5f, 0); // start from bottomleft
    Vector3 v3;

    if (normal.normalized != Vector3.forward)
      v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
    else
      v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude;

    var corner0 = position + v3;
    var corner2 = position - v3;
    var q = Quaternion.AngleAxis(90.0f, normal);
    v3 = q * v3;
    var corner1 = position + v3;
    var corner3 = position - v3;

    Debug.DrawLine(corner0, corner2, Color.green, 99);
    Debug.DrawLine(corner1, corner3, Color.green, 99);
    Debug.DrawLine(corner0, corner1, Color.green, 99);
    Debug.DrawLine(corner1, corner2, Color.green, 99);
    Debug.DrawLine(corner2, corner3, Color.green, 99);
    Debug.DrawLine(corner3, corner0, Color.green, 99);
    Debug.DrawRay(position, normal, Color.red, 99);
  }


  /// <summary>
  /// Generates a sprite using the input vertices. Affects the sprite and collider params.
  /// </summary>
  /// <returns></returns>
  public void GenerateSprite(Vector2[] verts, Sprite sprite = null, PolygonCollider2D collider = null) {
    // by default, uses this object's sprite and collider
    if (sprite == null) sprite = m_Sprite;
    if (collider == null) collider = m_Collider;

    ClockwiseSort.SortVerts(verts);

    // generate tris in correct winding order
    int triangles = (verts.Length - 2);
    ushort next = 1;
    ushort[] tris = new ushort[triangles * 3]; // triangles in a n-sided gon is n-2, 3 per vert
    for (int i = 0; i < triangles; i++) {
      tris[(i * 3)] = 0;
      tris[(i * 3) + 1] = next;
      tris[(i * 3) + 2] = (ushort)(next + 1);
      next++;
    }

    // convert local verts into sprite space, override the sprite geometry
    Vector2[] spriteverts = new Vector2[verts.Length];
    for (int i = 0; i < verts.Length; i++) {
      spriteverts[i] = new Vector2(
        verts[i].x * sprite.textureRect.width,
        verts[i].y * sprite.textureRect.height); // change
    }
    sprite.OverrideGeometry(spriteverts, tris);

    // convert local verts to polygon space
    Vector2[] colliderverts = new Vector2[verts.Length];
    for (int i = 0; i < verts.Length; i++) {
      colliderverts[i] = verts[i] - new Vector2(0.5f, 0.5f);
    }
    collider.points = colliderverts;

    area = GetArea();
  }

  protected static class ClockwiseSort {
    /// <summary>
    /// Uses atan2 to determine vertices' relative position. Quicksort algo.
    /// </summary>
    /// <returns></returns>
    public static void SortVerts(Vector2[] verts) {
      float[] angle = new float[verts.Length];
      Vector2 center = GetCenter(verts);

      for (int i = 0; i < verts.Length; i++) {
        angle[i] = Mathf.Atan2(
          verts[i].y - center.y,
          verts[i].x - center.x
        );
      }

      QuickSort(verts, angle, 0, verts.Length - 1);
    }

    private static void QuickSort(Vector2[] verts, float[] angles, int low, int high) {
      if (low >= high) return;
      int index = Partition(verts, angles, low, high);
      // sort the left and right sides of the partition
      QuickSort(verts, angles, low, (index - 1));
      QuickSort(verts, angles, (index + 1), high);
    }

    private static void Swap(Vector2[] verts, float[] angles, int i, int j) {
      Vector2 tempvert = verts[i];
      float tempangle = angles[i];
      verts[i] = verts[j];
      angles[i] = angles[j];
      verts[j] = tempvert;
      angles[j] = tempangle;
    }

    private static int Partition(Vector2[] verts, float[] angles, int low, int high) {
      // choose the pivot to be the last element
      float pivot = angles[high];
      int i = (low - 1);

      // sort the array
      for (int j = low; j < high; j++) {
        if (angles[j] >= pivot) continue;
        i++;
        Swap(verts, angles, i, j);
      }

      // move the pivot, then return the pivot index
      Swap(verts, angles, (i + 1), high);
      return (i + 1);
    }

    /// <summary>
    /// Redundant function. Uses bubble sort.
    /// </summary>
    /// <returns></returns>
    private static Vector2[] SortVertClockwise(Vector2[] verts) {

      float[] angle = new float[verts.Length];
      Vector2 center = GetCenter(verts);

      for (int i = 0; i < verts.Length; i++) {
        angle[i] = Mathf.Atan2(
          verts[i].y - center.y,
          verts[i].x - center.x
          );
      }

      Vector2[] out_verts = verts;

      for (int i = 0; i < out_verts.Length - 1; i++) {
        bool swapped = false;
        for (int j = 0; j < out_verts.Length - i - 1; j++) {
          if (angle[j] > angle[j + 1]) {

            float tempangle = angle[j];
            Vector2 tempvert = out_verts[j];

            angle[j] = angle[j + 1];
            out_verts[j] = out_verts[j + 1];

            angle[j + 1] = tempangle;
            out_verts[j + 1] = tempvert;

            swapped = true;
          }
        }

        if (!swapped) break;
      }

      return out_verts;
    }
  }

  /// <summary>
  /// </summary>
  /// <returns>Returns the centre point.</returns>
  private static Vector2 GetCenter(Vector2[] in_verts) {
    Vector2 center = Vector2.zero;
    for (int i = 0; i < in_verts.Length; i++)
      center += in_verts[i];

    center /= in_verts.Length;
    return center;
  }

}
