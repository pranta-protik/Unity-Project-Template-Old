using UnityEngine.UI;

namespace _Tools.Extensions
{
    public static class ImageExtensions
    {
        public static void ChangeAlpha(this Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}