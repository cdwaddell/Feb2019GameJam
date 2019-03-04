using System;
using UnityEngine;

public class OnPersonEnter : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(CheckPlayer(collider))
        {
            OnPlayerTransitioned(new PlayerTransitionedEventArgs
            {
                Player = collider.gameObject,
                Type = PlayerTransitionType.Enter
            });

            var glow = GetComponent<SpriteGlow.SpriteGlowEffect>();
            if (glow != null)
                glow.OutlineWidth = 3;
        }
    }

    //public void OnCollisionStay2D(Collision2D collision)
    //{
    //    var collider = collision.collider;
    //    if (CheckPlayer(collider))
    //    {
    //        OnPlayerTransitioned(new PlayerTransitionedEventArgs
    //        {
    //            Player = collider.gameObject,
    //            Type = PlayerTransitionType.Stay
    //        });
    //    }
    //}

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (CheckPlayer(collider))
        {
            OnPlayerTransitioned(new PlayerTransitionedEventArgs {
                Player = collider.gameObject,
                Type = PlayerTransitionType.Exit
            });

            var glow = GetComponent<SpriteGlow.SpriteGlowEffect>();
            if (glow != null)
                glow.OutlineWidth = 0;
        }
    }
    
    private bool CheckPlayer(Collider2D collision)
    {
        return collision.gameObject.name == "Player";
    }

    protected virtual void OnPlayerTransitioned(PlayerTransitionedEventArgs e)
    {
        PlayerTransitioned?.Invoke(gameObject, e);
    }

    public event EventHandler<PlayerTransitionedEventArgs> PlayerTransitioned;
}
