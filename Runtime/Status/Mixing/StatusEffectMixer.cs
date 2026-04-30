// Dependancies : 
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// Static singleton accessor for the <b>StatusEffectMixingDatabase</b>. <br/>
    /// Loads the database from project resources and exposes it globally for mixing logic. <br/><br/>
    /// Contains : <br/>
    /// <b>- instance :</b> A lazily loaded reference to the mixing database asset. <br/><br/>
    /// Responsible for : <br/>
    /// <b>- Database Access :</b> Ensures the mixing database is loaded and available at runtime. <br/>
    /// </summary>
    public static class StatusEffectMixer
    {
        // Data Members :
        private static StatusEffectMixingDatabase _underlyingDatabaseInstance;

        /// <summary>
        /// Singleton accessor for the StatusEffectMixingDatabase. Loads the asset from Resources if needed.
        /// </summary>
        public static StatusEffectMixingDatabase instance
        {
            get
            {
                if (_underlyingDatabaseInstance == null)
                {
                    _underlyingDatabaseInstance = Resources.Load<StatusEffectMixingDatabase>("StatusEffectMixingDatabase");

                    if (_underlyingDatabaseInstance == null)
                    {
                        Debug.LogError("Ability System : Status Effect Mixer Singleton : StatusEffectMixingDatabase resource not found.");
                    }
                }

                return _underlyingDatabaseInstance;
            }
        }
    }
}
