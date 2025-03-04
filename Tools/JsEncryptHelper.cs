using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace JsEncrypt
{
    /// <summary>
    /// JsEncryptHelper 的摘要说明
    /// </summary>
    /// <summary>
    /// AJax提交参数加密帮助类
    /// </summary>
    public class JsEncryptHelper
    {
        private const string defaultprivateKey = @"MIIEogIBAAKCAQBtMw5aUdmgCw9lRpxytiOjW/j+HMzLiDxrfE2/39jn+Jxc7LJPcgdgy+R495KJuXHMJWypLwoexRbypXWQQPMnF7+BG/lkwFjnVY1gWCl8gpWwSrf5za2wxBxxjOApMUPIb/dcBwJ63JUr5oN4hJo+dvogAXrLUIxKN1o/bvCot15F3pNy96VUZT6WbPSKnAkDqWACtQE2fWMKhfrd9RBtmCV6m/69Gt0heHlUgHk/DmHaz/xtE7rnQrcwP9ulPL7va8GbHNPlJ+huHCzrYzKUhxxHsHoVxCt60RllbARuvb5GwLA8FU7gscD0fqJKMueCX+qhYUyLVut5dld5BmZzAgMBAAECggEAIvvsaTs3wXJJqGSK0qVPnZJlnuOTKNGoqbbVIdeRIiAf2BVsus8JVhV8SNTn+X+T2ZdTzI/pahoyU+J2W5SzUZ7mF3Li30hQMyzRckBMajtbwXLupfUi5DKv+iDr38aMtLZKRQ1p2fq5P6tGC2JQGBr93ysaL/DKQSyimRftud84+03OyjNtZFkaAVJGLcHwzKHFDHIebOS7/RGUHqgvmNd8wQHnL6zqVNZRkBsOx9L6RPATfB4pcAmxMKRIEnNVGV3AnCvm1a2VUCATTMcA36ai8PkcsAMK0R8t930cFj/GiDXfqVk2xXje3OvNnioifFtIwUrpFsWPiMmAIVEXIQKBgQDApQxYu5zk5fm9CMVS9Cahma3xeIe7GJ8grD3GqKQ9WlQWw8jRrlKGF8eCmOJlNfs5EpRBaf1seZySo7aK2MN2r7g0tpO7rmLOJqMyQBKS978xQQRd3TV8bUqcJThzw3YcoHQauopvNyngm5QLeCBXpaDNVyRI3Ms5fO6MirjIbwKBgQCRHKqBC0+7/vKGWiYmV7BJ08jJbBhdZH/HWGkPrP8a85NjhW+hsv5zmK40DrdOehrjbjjMkTwuv73Uoh/EG/pik6aNmc/TLvYhSAp4fQ1AFgg3NiBDUlxRQ4ocBGtQ1dvWa5LdZ3zsdNA/rFTUo3NEboCHBNdMIqM5P8X2/FWcPQKBgF+i0FFQul/sR6Hvs74t1OvO1kqVMpTQcVcQCw8Pc5G9wRcYFR77Mp69OV99NI9YUCKSNaWPz67FZNRrj9i4KblHHOSWaxr2RLjg942fv1jUw0bZZyiOA9qEi1CfLbpSo9UsVtdaGhWCRjjqI4HaHfxFDmJCS5IcgotEmUyOJZPjAoGAUZxnoC+2ZiqaR0lID2Rdtweu8ukiQtQUsQ5d9/z5dDTs/Zm8EJrUVDrYLlrgaPhvSt1ggFxmFnyrzHxplSePCAW2NAj/Quw0bL9RdDYQT6yUbki4mGQnm2R6tgseN9Yuz0as0Gw1a96+iDDpfLV4TFJBYq4sT5tjZP0i3ydHFpkCgYEAmRvE4QgA1K4nh/Zv7wAoEGYU9aipjuUJxUg6uF7ywgY6O/Vv2KMzs3R8okcsrQm6ZrxL75V/wr7fQcJ81++j2cqQcStlquTTDvHGcF35GXEtsxapNRboMg8TCWAxuSLmwBmCA+uviBfKPM3fMzDH1U7/BWv3JCyRVdVK+gMAwB0=";

        private const string defaultpublicKey = @"MIIBITANBgkqhkiG9w0BAQEFAAOCAQ4AMIIBCQKCAQBtMw5aUdmgCw9lRpxytiOjW/j+HMzLiDxrfE2/39jn+Jxc7LJPcgdgy+R495KJuXHMJWypLwoexRbypXWQQPMnF7+BG/lkwFjnVY1gWCl8gpWwSrf5za2wxBxxjOApMUPIb/dcBwJ63JUr5oN4hJo+dvogAXrLUIxKN1o/bvCot15F3pNy96VUZT6WbPSKnAkDqWACtQE2fWMKhfrd9RBtmCV6m/69Gt0heHlUgHk/DmHaz/xtE7rnQrcwP9ulPL7va8GbHNPlJ+huHCzrYzKUhxxHsHoVxCt60RllbARuvb5GwLA8FU7gscD0fqJKMueCX+qhYUyLVut5dld5BmZzAgMBAAE=";

        private RSACryptoServiceProvider _privateKeyRsaProvider;
        private RSACryptoServiceProvider _publicKeyRsaProvider;

        public JsEncryptHelper(string privateKey = "", string publicKey = "")
        {

            if (string.IsNullOrEmpty(privateKey))
            {
                privateKey = defaultprivateKey;
            }
            if (string.IsNullOrEmpty(publicKey))
            {
                publicKey = defaultpublicKey;
            }

            if (!string.IsNullOrEmpty(privateKey))
            {
                _privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(privateKey);
            }

            if (!string.IsNullOrEmpty(publicKey))
            {
                _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);
            }
        }

        public string Decrypt(string cipherText)
        {
            try
            {
                if (_privateKeyRsaProvider == null)
                {
                    throw new Exception("_privateKeyRsaProvider is null");
                }

                if (string.IsNullOrEmpty(cipherText))
                {
                    return "";
                }
                return Encoding.UTF8.GetString(_privateKeyRsaProvider.Decrypt(Convert.FromBase64String(cipherText), false));
            }
            catch (Exception ex)
            {
                ///记录错误日志
                return "cuo";
            }
        }

        public string Encrypt(string text)
        {
            if (_publicKeyRsaProvider == null)
            {
                throw new Exception("_publicKeyRsaProvider is null");
            }
            return Convert.ToBase64String(_publicKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(text), false));
        }


        private RSACryptoServiceProvider CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var privateKeyBits = System.Convert.FromBase64String(privateKey);

            var RSA = new RSACryptoServiceProvider();
            var RSAparams = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            RSA.ImportParameters(RSAparams);
            return RSA;
        }

        private int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
                if (bt == 0x82)
                {
                    highbyte = binr.ReadByte();
                    lowbyte = binr.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                }
                else
                {
                    count = bt;
                }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        private RSACryptoServiceProvider CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] x509key;
            byte[] seq = new byte[15];
            int x509size;

            x509key = Convert.FromBase64String(publicKeyString);
            x509size = x509key.Length;

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using (MemoryStream mem = new MemoryStream(x509key))
            {
                using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
                {
                    byte bt = 0;
                    ushort twobytes = 0;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    seq = binr.ReadBytes(15);       //read the Sequence OID
                    if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                    RSAParameters RSAKeyInfo = new RSAParameters();
                    RSAKeyInfo.Modulus = modulus;
                    RSAKeyInfo.Exponent = exponent;
                    RSA.ImportParameters(RSAKeyInfo);

                    return RSA;
                }

            }
        }

        private bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }
    }
}