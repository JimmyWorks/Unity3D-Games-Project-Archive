/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

///<summary>
/// Coin class which implements abstract Collectible class
///</summary>
public class Coin : Collectible {

    /// <summary>
    /// Public dispatch method for when this object has been collected
    /// </summary>
    /// <param name="input"> The owner which collected this collectible </param>
    public override void postCollection(PlatformerGirl.PlayerCharacter input)
    {
        input.gainCoin();
        ParticleManager.createParticle(transform.position, ParticleManager.ParticleType.COIN_PARTICLE);
        Destroy(transform.gameObject);
    }
}
