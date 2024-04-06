using System;

namespace Sources.HeroBase
{
    [Serializable]
    public class HeroData
    {
        public int ID;
        public string Name;
        public int Damage;
        public float ReloadSpeed;
        public int Price;
        public bool IsInitial;
    }
}