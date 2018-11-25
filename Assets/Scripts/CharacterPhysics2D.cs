using System.Collections;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class CharacterPhysics2D : MonoBehaviour {

	[SerializeField] private LayerMask collisionMask;
	[SerializeField] private float skinWidth = .015f;
	[SerializeField] private int horizontalRayCount = 4;
	[SerializeField] private int verticalRayCount = 4;

	private float maxClimbAngle = 60;
	private float maxDescendAngle = 60;

	private float horizontalRaySpacing;
	private float verticalRaySpacing;

	private new BoxCollider2D collider;
	private RaycastOrigins raycastOrigins;
	private CollisionInfo collisions;
	private Vector2 velocity;

	public int HorizontalRayCount {
		get { return horizontalRayCount; }
	}

	public int VerticalRayCount {
		get { return verticalRayCount; }
	}

	public CollisionInfo Collisions {
		get { return collisions; }
	}

	public bool IsGrounded {
		get { return collisions.below; }
	}

    public Vector2 Velocity
    {
        get
        {
            return velocity;
        }

        set
        {
            velocity = value;
        }
    }

	private void OnValidate() {
		skinWidth = Mathf.Clamp(skinWidth, 0 , float.MaxValue);
		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2 , int.MaxValue);
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2 , int.MaxValue);
	}

    private void Start () {
		collider = GetComponent<BoxCollider2D> ();
		CalculateRaySpacing ();
	}

	private void Update () {
		Move(velocity * Time.deltaTime);
	}

	private void Move (Vector3 ds) {
		UpdateRaycastOrigins ();
		collisions.Reset ();
		collisions.velocityOld = ds;

		if (ds.y < 0) {
			DescendSlope (ref ds);
		}
		if (ds.x != 0) {
			HorizontalCollisions (ref ds);
		}
		if (ds.y != 0) {
			VerticalCollisions (ref ds);
		}

		transform.Translate (ds);
	}

	

	private void HorizontalCollisions (ref Vector3 ds) {
		float dirX = Mathf.Sign (ds.x);
		float rayLength = Mathf.Abs (ds.x) + skinWidth;

		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (dirX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * dirX, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.right * dirX * rayLength, Color.red);

			if (hit) {

				float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);

				if (i == 0 && slopeAngle <= maxClimbAngle) {
					if (collisions.descendingSlope) {
						collisions.descendingSlope = false;
						ds = collisions.velocityOld;
					}
					float distanceToSlopeStart = 0;
					if (slopeAngle != collisions.slopeAngleOld) {
						distanceToSlopeStart = hit.distance - skinWidth;
						ds.x -= distanceToSlopeStart * dirX;
					}
					ClimbSlope (ref ds, slopeAngle);
					ds.x += distanceToSlopeStart * dirX;
				}

				if (!collisions.climbingSlope || slopeAngle > maxClimbAngle) {
					ds.x = (hit.distance - skinWidth) * dirX;
					rayLength = hit.distance;

					if (collisions.climbingSlope) {
						ds.y = Mathf.Tan (collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs (ds.x);
					}

					collisions.left = dirX == -1;
					collisions.right = dirX == 1;
				}
			}
		}
	}

	private void VerticalCollisions (ref Vector3 ds) {
		float dirY = Mathf.Sign (ds.y);
		float rayLength = Mathf.Abs (ds.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (dirY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + ds.x);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * dirY, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.up * dirY * rayLength, Color.red);

			if (hit) {
				ds.y = (hit.distance - skinWidth) * dirY;
				rayLength = hit.distance;

				if (collisions.climbingSlope) {
					ds.x = ds.y / Mathf.Tan (collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign (ds.x);
				}

				collisions.below = dirY == -1;
				collisions.above = dirY == 1;
			}
		}

		if (collisions.climbingSlope) {
			float directionX = Mathf.Sign (ds.x);
			rayLength = Mathf.Abs (ds.x) + skinWidth;
			Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * ds.y;
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			if (hit) {
				float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
				if (slopeAngle != collisions.slopeAngle) {
					ds.x = (hit.distance - skinWidth) * directionX;
					collisions.slopeAngle = slopeAngle;
				}
			}
		}
	}

	private void ClimbSlope (ref Vector3 ds, float slopeAngle) {
		float moveDistance = Mathf.Abs (ds.x);
		float climbVelocityY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance;

		if (ds.y <= climbVelocityY) {
			ds.y = climbVelocityY;
			ds.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (ds.x);
			collisions.below = true;
			collisions.climbingSlope = true;
			collisions.slopeAngle = slopeAngle;
		}
	}

	private void DescendSlope (ref Vector3 ds) {
		float directionX = Mathf.Sign (ds.x);
		Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

		if (hit) {
			float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
			if (slopeAngle != 0 && slopeAngle <= maxDescendAngle) {
				if (Mathf.Sign (hit.normal.x) == directionX) {
					if (hit.distance - skinWidth <= Mathf.Tan (slopeAngle * Mathf.Deg2Rad) * Mathf.Abs (ds.x)) {
						float moveDistance = Mathf.Abs (ds.x);
						float descendVelocityY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance;
						ds.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (ds.x);
						ds.y -= descendVelocityY;

						collisions.slopeAngle = slopeAngle;
						collisions.descendingSlope = true;
						collisions.below = true;
					}
				}
			}
		}
	}

	private void UpdateRaycastOrigins () {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}

	private void CalculateRaySpacing () {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	private struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;

		public bool climbingSlope;
		public bool descendingSlope;
		public float slopeAngle, slopeAngleOld;
		public Vector3 velocityOld;

		public void Reset () {
			above = below = false;
			left = right = false;
			climbingSlope = false;
			descendingSlope = false;

			slopeAngleOld = slopeAngle;
			slopeAngle = 0;
		}
	}

}