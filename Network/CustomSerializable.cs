using UnityEngine;

namespace IngameShop.Network
{
    internal static class CustomSerializable
    {
        public static string Vector3ToString(Vector3 vector)
        {
            return $"{vector.x},{vector.y},{vector.z}";
        }

        public static Vector3 Vector3FromString(string vectorString)
        {
            string[] parts = vectorString.Split(',');
            if (parts.Length != 3) throw new System.FormatException("String is not a valid Vector3 format");

            return new Vector3(
                float.Parse(parts[0]),
                float.Parse(parts[1]),
                float.Parse(parts[2])
            );
        }

        public static string QuaternionToString(Quaternion quaternion)
        {
            return $"{quaternion.x},{quaternion.y},{quaternion.z},{quaternion.w}";
        }

        public static Quaternion QuaternionFromString(string quaternionString)
        {
            string[] parts = quaternionString.Split(',');
            if (parts.Length != 4) throw new System.FormatException("String is not a valid Quaternion format");

            return new Quaternion(
                float.Parse(parts[0]),
                float.Parse(parts[1]),
                float.Parse(parts[2]),
                float.Parse(parts[3])
            );
        }
    }
}
