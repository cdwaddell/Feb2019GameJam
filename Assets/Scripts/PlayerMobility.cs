using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMobility : MonoBehaviour
    {
        public float speed = 100;
        public GameObject ShopFloor;

        Rigidbody2D rb;

        Vector2 destination;
        Vector2 lastLocation;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            lastLocation = rb.transform.position;
        }

        void Update()
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var collider = ShopFloor.GetComponent<PolygonCollider2D>();
            if (Input.GetMouseButtonDown(0) && IsInPolygon(point, collider))
            {
                destination = point;

                var travelPath = destination - lastLocation;
                rb.velocity = travelPath.normalized * speed * Time.deltaTime;
            }
        }

        void LateUpdate()
        {
            lastLocation = rb.transform.position;
            var gap = new Vector2(
                Mathf.Abs(destination.x - lastLocation.x),
                Mathf.Abs(destination.y - lastLocation.y)
            );

            var xVelocity = rb.velocity.x;
            var yVelocity = rb.velocity.y;
            if (.05 > Mathf.Abs(gap.x))
                xVelocity = 0;
            if (.05 > Mathf.Abs(gap.y))
                yVelocity = 0;

            rb.velocity = new Vector2(xVelocity, yVelocity);
        }

        private bool IsInPolygon(Vector2 pointToCheck, PolygonCollider2D polygon)
        {
            bool result = false;
            var previousPoint = polygon.points[0];
            for (int i = 1; i < polygon.points.Length; i++)
            {
                var currentPoint = polygon.points[i];
                if (CheckCollision(pointToCheck, previousPoint, currentPoint, ref result))
                    return true;
                previousPoint = currentPoint;
            }
            //reconnection the polygon to the first point and check
            if (CheckCollision(pointToCheck, previousPoint, polygon.points[0], ref result))
                return true;
            return result;
        }

        private bool CheckCollision(Vector2 pointToCheck, Vector2 pointA, Vector2 pointB, ref bool result)
        {
            if ( //if the point to check is the point that you are checking
                (pointB.x == pointToCheck.x) && (pointB.y == pointToCheck.y) ||
                (
                    //the point you are checking is on the same y axis as the other two points
                    (pointB.y == pointA.y) && (pointToCheck.y == pointA.y) && 
                    //and the point is constrained by the x axisS
                    (pointA.x <= pointToCheck.x) && (pointToCheck.x <= pointB.x)
                )
            ) return true; //immediate success
            
            if ((pointB.y < pointToCheck.y) && (pointA.y >= pointToCheck.y) || (pointA.y < pointToCheck.y) && (pointB.y >= pointToCheck.y))
            {
                if (pointB.x + (pointToCheck.y - pointB.y) / (pointA.y - pointB.y) * (pointA.x - pointB.x) <= pointToCheck.x)
                    result = !result;
            }
            return false;
        }
    }
}
