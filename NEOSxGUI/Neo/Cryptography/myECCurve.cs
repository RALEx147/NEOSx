using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace NEOSxGUI.Neo.Cryptography
{
    
    public partial struct myECCurve
    {
        
        public byte[] A;
        public byte[] B;
        public ECPoint G;
        public byte[] Order;
        public byte[] Cofactor;
        public byte[] Seed;
        public ECCurve.ECCurveType CurveType;
        public HashAlgorithmName? Hash;
        public byte[] Polynomial;
        public byte[] Prime;

        private Oid _oid;
        public Oid Oid
        {
            get
            {
                return new Oid(_oid.Value, _oid.FriendlyName);
            }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Oid));

                if (string.IsNullOrEmpty(value.Value) && string.IsNullOrEmpty(value.FriendlyName))
                    throw new ArgumentException("string.IsNullOrEmpty(value.Value) && string.IsNullOrEmpty(value.FriendlyName)");

                _oid = value;
            }
        }

        public static myECCurve Create(Oid oid)
        {
            myECCurve curve = new myECCurve();
            curve.CurveType = ECCurve.ECCurveType.Named;
            curve.Oid = oid;
            return curve;
        }


        public bool IsPrime
        {
            get
            {
                return CurveType == ECCurve.ECCurveType.PrimeShortWeierstrass ||
                    CurveType == ECCurve.ECCurveType.PrimeMontgomery ||
                    CurveType == ECCurve.ECCurveType.PrimeTwistedEdwards;
            }
        }

        public bool IsCharacteristic2
        {
            get
            {
                return CurveType == ECCurve.ECCurveType.Characteristic2;
            }
        }

        public bool IsExplicit
        {
            get
            {
                return IsPrime || IsCharacteristic2;
            }
        }

        public bool IsNamed
        {
            get
            {
                return CurveType == ECCurve.ECCurveType.Named;
            }
        }

       
        public void Validate()
        {
            if (IsNamed)
            {
                if (HasAnyExplicitParameters())
                {
                    throw new CryptographicException("SR.Cryptography_InvalidECNamedCurve");
                }

                if (Oid == null ||
                    (string.IsNullOrEmpty(Oid.FriendlyName) && string.IsNullOrEmpty(Oid.Value)))
                {
                    throw new CryptographicException("SR.Cryptography_InvalidCurveOid");
                }
            }
            else if (IsExplicit)
            {
                bool hasErrors = false;

                if (A == null ||
                    B == null || B.Length != A.Length ||
                    G.X == null || G.X.Length != A.Length ||
                    G.Y == null || G.Y.Length != A.Length ||
                    Order == null || Order.Length == 0 ||
                    Cofactor == null || Cofactor.Length == 0)
                {
                    hasErrors = true;
                }

                if (IsPrime)
                {
                    if (!hasErrors)
                    {
                        if (Prime == null || Prime.Length != A.Length)
                        {
                            hasErrors = true;
                        }
                    }

                    if (hasErrors)
                        throw new CryptographicException("SR.Cryptography_InvalidECPrimeCurve");
                }
                else if (IsCharacteristic2)
                {
                    if (!hasErrors)
                    {
                        if (Polynomial == null || Polynomial.Length == 0)
                        {
                            hasErrors = true;
                        }
                    }

                    if (hasErrors)
                        throw new CryptographicException("SR.Cryptography_InvalidECCharacteristic2Curve");
                }
            }
            else
            {
                // Implicit; if there are any values, throw
                if (HasAnyExplicitParameters() || Oid != null)
                {
                    throw new CryptographicException("SR.Cryptography_CurveNotSupported");
                }
            }
        }

        private bool HasAnyExplicitParameters()
        {
            return (A != null ||
                B != null ||
                G.X != null ||
                G.Y != null ||
                Order != null ||
                Cofactor != null ||
                Prime != null ||
                Polynomial != null ||
                Seed != null ||
                Hash != null);
        }

        public static implicit operator ECCurve(myECCurve v)
        {
            throw new NotImplementedException();
        }
    }
}