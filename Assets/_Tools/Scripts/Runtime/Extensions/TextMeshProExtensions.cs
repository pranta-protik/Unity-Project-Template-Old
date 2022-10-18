using TMPro;

namespace _Tools.Extensions
{
    public static class TextMeshProExtensions
    {
        public static void ChangeAlpha(this TextMeshPro textMesh, float alpha)
        {
            var color = textMesh.color;
            color.a = alpha;
            textMesh.color = color;
        }
        
        public static void ChangeAlpha(this TextMeshProUGUI textMesh, float alpha)
        {
            var color = textMesh.color;
            color.a = alpha;
            textMesh.color = color;
        }
    }
}