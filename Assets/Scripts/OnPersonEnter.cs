using UnityEngine;

public class OnPersonEnter : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(CheckPlayer(collision))
        {
            Hit(collision.gameObject);

            var glow = GetComponent<SpriteGlow.SpriteGlowEffect>();
            if (glow != null)
                glow.OutlineWidth = 3;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (CheckPlayer(collision))
        {
            Exit(collision.gameObject);

            var glow = GetComponent<SpriteGlow.SpriteGlowEffect>();
            if (glow != null)
                glow.OutlineWidth = 0;
        }
    }

    private bool CheckPlayer(Collider2D collision)
    {
        return collision.gameObject.name == "Player";
    }

    public virtual void Hit(GameObject player)
    {

    }

    public virtual void Exit(GameObject player)
    {

    }
}
