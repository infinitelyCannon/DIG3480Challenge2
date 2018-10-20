using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBox : MonoBehaviour {

    public enum BlockTypes
    {
        CoinBlock,
        GiftBlock
    };

    [SerializeField]
    public BlockTypes type;
    public GameObject coinPrefab;
    public int initialCoins;
    public GameObject giftObject;

    private Animator anim;
    private int coinCount = 0;
    private PlayerController playerScriptRef;
    private bool isInfinite = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        
        if(type == BlockTypes.CoinBlock)
        {
            if(initialCoins == -1)
            {
                initialCoins = int.MaxValue;
                isInfinite = true;
            }
            anim.SetBool("isCoinBlock", true);
            anim.SetInteger("Coins", initialCoins);
            coinCount = initialCoins;
        }
        else
        {
            anim.SetBool("isGiftBlock", true);
        }
        playerScriptRef = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Bump");
            if(type == BlockTypes.CoinBlock && !isInfinite)
            {
                anim.SetInteger("Coins", --coinCount);
            }
        }
    }

    public void NewCoinTrigger()
    {
        GameObject.Instantiate(coinPrefab, transform.position, Quaternion.identity, transform);
        playerScriptRef.AddToScore(100);
    }

    public void NewGiftTrigger()
    {
        GameObject.Instantiate(giftObject, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
    }
}
