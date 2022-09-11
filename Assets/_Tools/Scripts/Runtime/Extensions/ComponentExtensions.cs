using _Tools.Helpers;
using JetBrains.Annotations;
using UnityEngine;

namespace _Tools.Extensions
{
    public static class ComponentExtensions
    {
        #region Extension Methods

        /// <summary>
        /// Checks if a component is null.
        /// </summary>
        /// <param name="originalComponent"></param>
        /// <param name="refComponentName"></param>
        /// <param name="refTransform"></param>
        /// <returns>Returns true if not null, otherwise returns false.</returns>
        public static bool IsNotNull(this Component originalComponent, string refComponentName, [CanBeNull] Transform refTransform = null)
        {
            if (originalComponent) return true;

            if (refTransform)
            {
                DebugUtils.LogError($"No reference of {refComponentName} found in {refTransform}!");
                return false;
            }
            
            DebugUtils.LogError($"No instance of {refComponentName} found!");
            return false;
        }

        #endregion
    }
}