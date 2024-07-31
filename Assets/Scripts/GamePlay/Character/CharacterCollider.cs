using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollider : MonoBehaviour
{
    [SerializeField] AddBlockScript addBlockScript;
    [SerializeField] Character character;



    private void OnTriggerEnter(Collider other)
    {
        if (character.IsOnGround && other.CompareTag("Character"))
        {
            if (other.gameObject.TryGetComponent<AddBlockScript>(out AddBlockScript otherAddBlock)
                && other.gameObject.TryGetComponent<Character>(out Character otherCharacter))
            {
                Character loser = (otherAddBlock.CurNumBlock > addBlockScript.CurNumBlock)
                    ? character : otherCharacter;
                if (loser.IsFalling) return;
                loser.Splash((- transform.position + other.transform.position) *
                    ((otherAddBlock.CurNumBlock > addBlockScript.CurNumBlock) ? -1 : 1));
            }

        }
        else if (other.CompareTag("Platform"))
        {
            if (other.transform.parent.gameObject.TryGetComponent<Platform>(out Platform platform))
            {
                if (platform != character.platform)
                {
                    character.ChangePlatform(platform);
                }
            }
            character.IsOnGround = true;
        }
        else if (other.CompareTag("Bridge"))
        {
            character.IsOnGround = false;
        }
        else if (other.CompareTag("WinningPlatform"))
        {
            EventManager.Instance.TriggerEvent("EndGame", character);
        }
    }
}
