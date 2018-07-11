using Neo.VM;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Numerics;

using System.Buffers;

namespace Neo.Cryptography
{
    public class Crypto : ICrypto
    {
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

            if (pubkey.Length == 33 && (pubkey[0] == 0x02 || pubkey[0] == 0x03))
            {
                try
                {
                    pubkey = Cryptography.ECC.ECPoint.DecodePoint(pubkey, Cryptography.ECC.ECCurve.Secp256r1).EncodePoint(false).Skip(1).ToArray();
                }
                catch
                {
                    return false;
                }
            }
            else if (pubkey.Length == 65 && pubkey[0] == 0x04)
            {
                pubkey = pubkey.Skip(1).ToArray();
            }
            else if (pubkey.Length != 64)
            {
                throw new ArgumentException();
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






            var ec = ECC.ECCurve.Secp256r1;
            var ecP = ECC.ECPoint.DecodePoint(pubkey, ec);

            var x = new ECC.ECFieldElement(new BigInteger(pubkey.Take(32).ToArray()),ec);
            var y = new ECC.ECFieldElement(new BigInteger(pubkey.Skip(32).ToArray()),ec);
            var ecpoint = new ECC.ECPoint(x, y, ec);

            var edsca = new ECC.ECDsa(ecpoint);



            var rr = new BigInteger(pubkey);
            var ss = new BigInteger(signature);

            var l = edsca.VerifySignature(message, ss, rr);
            Console.WriteLine(l);
            return l;



















            using (var ecdsa = ECDsa.Create(new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,//MASTER CULTPRIT RIGHT HERE
                Q = new ECPoint
                {
                    X = pubkey.Take(32).ToArray(),
                    Y = pubkey.Skip(32).ToArray()
                }}))
            {
                Console.WriteLine("end?");
                return ecdsa.VerifyData(message, signature, HashAlgorithmName.SHA256);
            }

        }

    }



}
