using Main.Gear.Weapon;

namespace Main.Gear.Weapon
{
    internal class Sword : Weapon
    {
        private const int AccessibilityConst = 1;

        private int _accessibility;

        public override int Accessibility
        {
            get { return _accessibility; }
            set { _accessibility = AccessibilityConst; }
        }
    }
}
