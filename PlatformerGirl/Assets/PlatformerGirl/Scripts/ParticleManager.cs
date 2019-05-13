/// Platformer Girl
/// CS 4332.501 - Intro to Programming Video Games
/// Assignment 2
/// 
/// Author: Jimmy Nguyen
/// Email: Jimmy@Jimmyworks.net

using UnityEngine;

/// <summary>
/// Particle manager controller component for handling all particle effects
/// </summary>
public class ParticleManager : MonoBehaviour {
    // Class singleton
    private static ParticleManager singleton;

    /// <summary>
    /// Particle types
    /// </summary>
    public enum ParticleType
    {
        COIN_PARTICLE
    }

	/// <summary>
    /// Start
    /// </summary>
	void Start () {
        if (singleton != null)
            return;
        singleton = this;
    }

    /// <summary>
    /// Create particle effects
    /// </summary>
    /// <param name="position"> Position of the particle effects </param>
    /// <param name="type"> Type of particle effect </param>
    public static void createParticle(Vector3 position, ParticleType type)
    {
        // If the type of particle effect is for collected coin...
        if(type == ParticleType.COIN_PARTICLE)
        {
            GameObject particles = (GameObject)Instantiate(Resources.Load("CoinDestroyed"));
            particles.transform.parent = singleton.transform;
            particles.transform.position = position;
        }
    }
}
