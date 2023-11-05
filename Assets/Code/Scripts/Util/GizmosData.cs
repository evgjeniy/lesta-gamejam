namespace Code.Scripts.Util
{
    [System.Serializable]
    public class GizmosData
    {
        public DrawType drawType = DrawType.Wire;
        public UnityEngine.Color gizmosColor = UnityEngine.Color.white;

        [System.Flags]
        public enum DrawType : byte { None, Wire, Solid }
    }
}