using System;

namespace Fraction
{
    public class Fraction : IComparable<Fraction>, IEquatable<Fraction>
    {
        #region Plus grand déviseur commun [PGCD] 'القاسم المشترك الأكبر'

        public static long pgcd(long a, long b)
        {
            long r, t;
            if (b > a)
            {
                t = a;
                a = b;
                b = t;
            }

            do
            {
                r = a % b;
                a = b;
                b = r;
            } while (r != 0);

            return a;
        }

        #endregion

        //***

        #region Constructors

        //***
        public Fraction(long a, long b)
        {
            Nominator = a;
            if (b == 0)
                throw new ArgumentException($"Denominator cannot be zero. {nameof(b)}");
            denominator = b;

        }

        //***

        #endregion

        //***

        #region Propriétés

        //***

        #region Nominateur 'البسط'

        //***
        public long Nominator { get; set; }
        //***

        #endregion

        //***

        #region Denominator 'المقام'

        //***
        private long denominator;

        public long Denominator
        {
            get => denominator;
            set
            {
                if (value == 0)
                    throw new ArgumentException($"Denominator cannot be zero. {nameof(denominator)}");
            }
        }

        //***

        #endregion

        //***

        #endregion

        //***

        #region Surcharges

        //***

        #region get positive value

        //***
        public static Fraction operator +(Fraction a) => a;
        //***

        #endregion

        //***

        #region get negative value

        //***
        public static Fraction operator -(Fraction a) => new Fraction(-a.Nominator, a.Denominator);
        //***

        #endregion

        //***

        #region sum overloading (+)

        //***
        public static Fraction operator +(Fraction a, Fraction b)
        {
            var fraction = new Fraction(a.Nominator * b.Denominator + b.Nominator * a.Denominator,
                a.Denominator * b.Denominator);
            return --fraction;
        }

        //***

        #endregion

        //***

        #region substraction overloading (-)

        //***
        public static Fraction operator -(Fraction a, Fraction b)
        {
            var fraction = a + (-b);
            return --fraction;
        }

        //***

        #endregion

        //***

        #region multiplication overloading (*)

        //***
        public static Fraction operator *(Fraction a, Fraction b)
        {
            var fraction = new Fraction(a.Nominator * b.Nominator,
                a.Denominator * b.Denominator);
            return --fraction;
        }

        //***

        #endregion

        //***

        #region division overloading (/)

        //***
        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Nominator == 0)
                throw new DivideByZeroException();
            var fraction = new Fraction(a.Nominator * b.Denominator,
                a.Denominator * b.Nominator);
            return --fraction;
        }

        //***

        #endregion

        //***

        #region shorthand overloading : 'زيادة الإختزال'(+)

        //***
        public static Fraction operator --(Fraction fraction)
            => new Fraction(fraction.Nominator / pgcd(fraction.Nominator, fraction.Denominator),
                fraction.Denominator / pgcd(fraction.Nominator, fraction.Denominator));

        //***

        #endregion

        //***

        #region tostring overriding : 'إعادة وظيفة التنصيص'(+)

        //***
        public override string ToString() => $"{Nominator} / {Denominator}";
        //***

        #endregion

        //***

        #region equals : 'المساوات'
        //***
        public static bool operator ==(Fraction rf, Fraction lf)
        {
            if (rf is null)
            {
                if (lf is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lf.Equals(rf);
        }

        public static bool operator !=(Fraction lf, Fraction rf) => !(lf == rf);
        //***
        #endregion

        //***

        #endregion

        //***

        #region Interface implementation

        /// <summary>
        /// Compares this Pet to another Pet by
        /// summing each Pet's age and name length.
        /// </summary>
        /// <param name="other">The Pet to compare this Pet to.</param>
        /// <returns>-1 if this Pet is 'less' than the other Pet,
        /// 0 if they are equal,
        /// or 1 if this Pet is 'greater' than the other Pet.</returns>
        int IComparable<Fraction>.CompareTo(Fraction other)
        {
            var number = 0L;
            try
            {
                number = other.Nominator;
                var sumOther = other.Nominator / other.Denominator;
                number = this.Nominator;
                var sumThis = this.Nominator / this.Denominator;

                if (sumOther > sumThis)
                    return -1;
                else if (sumOther == sumThis)
                    return 0;
                else
                    return 1;
            }
            catch (DivideByZeroException)
            {
                throw new ArgumentException($"Division by zero.");
            }
        }

        public bool Equals(Fraction other)
        {
            if (other is null) return false;
            return (Nominator * other.Denominator == Denominator * other.Nominator);
        }

        #endregion

        //***
    }

    //***
}