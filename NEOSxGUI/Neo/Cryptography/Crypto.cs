using Neo.VM;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Buffers;


//using System.Numerics;


using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;




namespace Neo.Cryptography
{
    public class Crypto : ICrypto
    {

        static readonly X9ECParameters _curve = SecNamedCurves.GetByName("secp256r1");
        static readonly ECDomainParameters _domain = new ECDomainParameters(_curve.Curve, _curve.G, _curve.N, _curve.H, _curve.GetSeed());

        //public override byte[] mySha256(byte[] message, int offset, int count)
        //{
        //    var hash = new Sha256Digest();
        //    hash.BlockUpdate(message, offset, count);

        //    byte[] result = new byte[32];
        //    hash.DoFinal(result, 0);

        //    return result;
        //}






        public static readonly Crypto Default = new Crypto();

        public byte[] Hash160(byte[] message)
        {
            return message.Sha256().RIPEMD160();
        }

        public byte[] Hash256(byte[] message)
        {
            return message.Sha256().Sha256();
        }

        public byte[] Sign(byte[] message, byte[] prikey, byte[] pubkey)
        {
            using (var ecdsa = ECDsa.Create(new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                D = prikey,
                Q = new ECPoint
                {
                    X = pubkey.Take(32).ToArray(),
                    Y = pubkey.Skip(32).ToArray()
                }
            }))
            {
                return ecdsa.SignData(message, HashAlgorithmName.SHA256);
            }
        }




        public bool VerifySignature(byte[] message, byte[] signature, byte[] pubkey)
        {
            byte[] fullpubkey = DecodePublicKey(pubkey, false, out System.Numerics.BigInteger x, out System.Numerics.BigInteger y);

            Org.BouncyCastle.Math.EC.ECPoint point = _curve.Curve.DecodePoint(fullpubkey);
            var keyParameters = new ECPublicKeyParameters(point, _domain);

            var signer = SignerUtilities.GetSigner("SHA256withECDSA");
            signer.Init(false, keyParameters);
            signer.BlockUpdate(message, 0, message.Length);

            if (signature.Length == 64)
            {
                signature = new DerSequence(
                new DerInteger(new BigInteger(1, signature.Take(32).ToArray())),
                new DerInteger(new BigInteger(1, signature.Skip(32).ToArray())))
                .GetDerEncoded();
            }


            return signer.VerifySignature(signature);
        }





        public byte[] DecodePublicKey(byte[] pubkey, bool compress, out System.Numerics.BigInteger x, out System.Numerics.BigInteger y)
        {
            if (pubkey == null || pubkey.Length != 33 && pubkey.Length != 64 && pubkey.Length != 65)
            {
                throw new ArgumentException(nameof(pubkey));
            }

            if (pubkey.Length == 33 && pubkey[0] != 0x02 && pubkey[0] != 0x03) throw new ArgumentException(nameof(pubkey));
            if (pubkey.Length == 65 && pubkey[0] != 0x04) throw new ArgumentException(nameof(pubkey));

            byte[] fullpubkey;

            if (pubkey.Length == 64)
            {
                fullpubkey = new byte[65];
                fullpubkey[0] = 0x04;
                Array.Copy(pubkey, 0, fullpubkey, 1, pubkey.Length);
            }
            else
            {
                fullpubkey = pubkey;
            }

            var ret = new ECPublicKeyParameters("ECDSA", _curve.Curve.DecodePoint(fullpubkey), _domain).Q;
            var x0 = ret.XCoord.ToBigInteger();
            var y0 = ret.YCoord.ToBigInteger();

            x = System.Numerics.BigInteger.Parse(x0.ToString());
            y = System.Numerics.BigInteger.Parse(y0.ToString());

            return ret.GetEncoded(compress);
        }











        //    public bool VerifySignature(byte[] message, byte[] signature, byte[] pubkey)
        //    {

        //        if (pubkey.Length == 33 && (pubkey[0] == 0x02 || pubkey[0] == 0x03))
        //        {
        //            try
        //            {
        //                pubkey = Cryptography.ECC.ECPoint.DecodePoint(pubkey, Cryptography.ECC.ECCurve.Secp256r1).EncodePoint(false).Skip(1).ToArray();
        //            }
        //            catch
        //            {
        //                return false;
        //            }
        //        }
        //        else if (pubkey.Length == 65 && pubkey[0] == 0x04)
        //        {
        //            pubkey = pubkey.Skip(1).ToArray();
        //        }
        //        else if (pubkey.Length != 64)
        //        {
        //            throw new ArgumentException();
        //        }


        //        using (var ecdsa = ECDsa.Create(new ECParameters
        //        {
        //            Curve = ECCurve.NamedCurves.nistP256,//MASTER CULTPRIT RIGHT HERE
        //            Q = new ECPoint
        //            {
        //                X = pubkey.Take(32).ToArray(),
        //                Y = pubkey.Skip(32).ToArray()
        //            }}))
        //        {
        //            Console.WriteLine("end?");
        //            return ecdsa.VerifyData(message, signature, HashAlgorithmName.SHA256);
        //        }

        //    }

        //}

    }

}




//testing------------------------------------------------------------------------------------------------------------------

//Oid nistp256 = new Oid("1.2.840.10045.3.1.7");

//var qq = new ECPoint();
//qq.X = pubkey.Take(32).ToArray();
//qq.Y = pubkey.Skip(32).ToArray();

//Console.WriteLine("1");
//var paramss = new ECParameters();
//Console.WriteLine("2");
//paramss.Q = qq;
//Console.WriteLine("3");
//------------------------------------------
//try
//{var c = ECCurve.CreateFromOid(l);}
//catch(Exception e){Console.WriteLine(e);}
//------------------------------------------
//paramss.Curve = c;

//Console.WriteLine("4");
//var dafs = ECDsa.Create(paramss);
//Console.WriteLine(dafs.VerifyData(message, signature, HashAlgorithmName.SHA256));
//Console.WriteLine("befor444e closure ");
//-------------------------------------------------------------------------------------------------------------------------






//var ec = ECC.ECCurve.Secp256r1;
//var ecP = ECC.ECPoint.DecodePoint(pubkey, ec);

//var x = new ECC.ECFieldElement(new BigInteger(pubkey.Take(32).ToArray()),ec);
//var y = new ECC.ECFieldElement(new BigInteger(pubkey.Skip(32).ToArray()),ec);
//var ecpoint = new ECC.ECPoint(x, y, ec);

//var edsca = new ECC.ECDsa(ecpoint);



//var rr = new BigInteger(pubkey);
//var ss = new BigInteger(signature);

//var l = edsca.VerifySignature(message, ss, rr);
//Console.WriteLine(l);
//return l;