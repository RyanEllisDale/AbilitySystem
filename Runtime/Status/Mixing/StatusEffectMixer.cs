using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public static class StatusEffectMixer
    {
        private static StatusEffectMixingDatabase underlyingDatabaseInstance;
        public static StatusEffectMixingDatabase instance
        {
            get
            {
                if (underlyingDatabaseInstance == null)
                {
                    underlyingDatabaseInstance = Resources.Load<StatusEffectMixingDatabase>("StatusEffectMixingDatabase");
                    if (underlyingDatabaseInstance == null)
                    {
                        Debug.LogError("Ability System : Status Effect Mixer Singleton : StatusEffectMixingDatabaseResource not found");
                    }
                }

                return underlyingDatabaseInstance;
            }
        }
    }
}
