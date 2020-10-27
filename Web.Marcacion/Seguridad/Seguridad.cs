using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Web.Marcacion.Seguridad
{
    public class Seguridad
    {
        

        public static string Encriptar(string Valor, string Tiempo = "")
        {
           
            try
            {
              
                string key = "AssisteKey";
                byte[] KeyArray;
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(Valor);

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                KeyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider ides = new TripleDESCryptoServiceProvider();
                ides.Key = KeyArray;
                ides.Mode = CipherMode.ECB;
                ides.Padding = PaddingMode.PKCS7;
                ICryptoTransform ctransform = ides.CreateEncryptor();
                byte[] ArrayResultado = ctransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
                ides.Clear();
                Valor = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            }
            catch (Exception EX)
            {

                Valor = EX.Message.ToString();
            }
            return Valor;
        }

        public static string Desencriptar(string Valor, string Tiempo = "")
        {
            
            try
            {
                
                string Key = "AssisteKey";
                byte[] KeyArray;
                byte[] Array_a_Decifrar = Convert.FromBase64String(Valor);
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                KeyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = KeyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform ICT = tdes.CreateDecryptor();
                byte[] ResultArray = ICT.TransformFinalBlock(Array_a_Decifrar, 0, Array_a_Decifrar.Length);
                tdes.Clear();
                Valor = UTF8Encoding.UTF8.GetString(ResultArray);
            }
            catch (Exception EX)
            {

                Valor = EX.Message.ToString();
            }
            return Valor;

        }

     

    }
}