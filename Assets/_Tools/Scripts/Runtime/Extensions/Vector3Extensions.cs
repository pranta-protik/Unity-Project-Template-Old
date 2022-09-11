using UnityEngine;

namespace _Tools.Extensions
{
    public static class Vector3Extensions
    {
        #region Extension Methods

        /// <summary>
        /// Updates the Vector3 with given parameter values.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>Returns a Vector3 with given parameter values.</returns>
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }

        /// <summary>
        /// Changes the y position of a Vector3 to zero.
        /// </summary>
        /// <param name="original"></param>
        /// <returns>Returns a Vector3 with y = 0f.</returns>
        public static Vector3 Flat(this Vector3 original)
        {
            return new Vector3(original.x, 0f, original.z);
        }

        #endregion
    }
}