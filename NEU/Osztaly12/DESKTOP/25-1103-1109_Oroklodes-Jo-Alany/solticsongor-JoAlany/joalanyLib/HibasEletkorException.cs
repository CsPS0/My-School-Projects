using System;

namespace joalanyLib
{
    public class HibasEletkorException : System.Exception
    {
        public HibasEletkorException() : base("Az életkor beállítása nem megfelelő!")
        {
            //Ha nem megy hát nem megy
        }
    }
}