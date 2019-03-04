using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMobility : MonoBehaviour
    {
        public GameObject ShopFloor;

        Rigidbody2D rb;
        CharacterController cc;
        List<Animator> animators = new List<Animator>();

        GameObject DownLeft;
        GameObject DownRight;
        GameObject UpLeft;
        GameObject UpRight;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            cc = GetComponent<CharacterController>();

            foreach (Transform child in transform)
            {
                try
                {
                    var anim = child.GetComponent<Animator>();
                    if (anim != null) animators.Add(anim);
                }
                catch { }
                switch (child.name)
                {
                    case "DownLeft":
                        DownLeft = child.gameObject;
                        break;
                    case "DownRight":
                        DownRight = child.gameObject;
                        break;
                    case "UpLeft":
                        UpLeft = child.gameObject;
                        break;
                    case "UpRight":
                        UpRight = child.gameObject;
                        break;
                }
            }
        }

        void Update()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            var movement = new Vector3(moveHorizontal, moveVertical, 0f);

            movement = movement * 10 * Time.deltaTime;

            transform.Translate(movement);

            var movingRight = movement.x > 0;
            var movingDown = movement.y < 0;
            var movingUp = movement.y > 0;
            var movingLeft = movement.x < 0;

            if(movingLeft && movingUp)
                SetActiveMovement(true, false, false, false);
            else if(movingLeft)
                SetActiveMovement(false, true, false, false);
            else if(movingRight && movingDown)
                SetActiveMovement(false, false, false, true);
            else if(movingRight)
                SetActiveMovement(false, false, true, false);
            else if(movingUp)
                SetActiveMovement(true, false, false, false);
            else if(movingDown)
                SetActiveMovement(false, false, false, true);

            var moving = Math.Abs(movement.x) > .001 || Math.Abs(movement.y) > .001;
            foreach (var anim in animators)
            {
                anim.SetBool("Moving", moving);
            }
        }

        private void SetActiveMovement (bool upLeft, bool downLeft, bool upRight, bool downRight)
        {
            UpLeft.SetActive(upLeft);
            DownLeft.SetActive(downLeft);
            UpRight.SetActive(upRight);
            DownRight.SetActive(downRight);
        }

        private Order HeldOrder = null;
        public void HoldLaundry(Order order)
        {
            HeldOrder = order;
            foreach(Transform child in transform)
            {
                foreach(Transform grandChild in child.transform)
                {
                    if(grandChild.gameObject.name == "basket")
                    {
                        if(HeldOrder != null)
                            grandChild.gameObject.SetActive(true);
                    }
                }
            }
        }

        public void DropLaundry()
        {
            HeldOrder = null;
            foreach (Transform child in transform)
            {
                foreach (Transform grandChild in child.transform)
                {
                    if (grandChild.gameObject.name == "basket")
                    {
                        if(HeldOrder == null)
                            grandChild.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
