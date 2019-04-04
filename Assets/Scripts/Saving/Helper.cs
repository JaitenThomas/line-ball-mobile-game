using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Security.Cryptography;
using System;

public static class Helper 
{
	// Serialize
	public static string Serialize<T>(this T toSerialize)
	{
		XmlSerializer xml = new XmlSerializer (typeof(T));
		StringWriter writer = new StringWriter ();
		xml.Serialize (writer, toSerialize);
		return writer.ToString ();
	}


	//De-serialize
	public static T Deserialize<T>(this string toDeserialize)
	{
		XmlSerializer xml = new XmlSerializer (typeof(T));
		StringReader reader = new StringReader (toDeserialize);
		return (T)xml.Deserialize (reader);
	}

	private static string hash = "123986@abc";
	//Encrypt
	public static string Encrypt(string input)
	{

		byte[] data = UTF8Encoding.UTF8.GetBytes (input);
		using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
		{
			byte[] key = md5.ComputeHash (UTF8Encoding.UTF8.GetBytes(hash));
			using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider () {
				Key = key,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}) {
				ICryptoTransform tr = trip.CreateEncryptor ();
				byte[] results = tr.TransformFinalBlock (data, 0, data.Length);
				return Convert.ToBase64String (results, 0, results.Length);
			}
		}

	}

	//Decrypt
	public static string Decrypt(string input)
	{

		byte[] data = Convert.FromBase64String (input);
		using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider ()) {
			byte[] key = md5.ComputeHash (UTF8Encoding.UTF8.GetBytes (hash));
			using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider () {
				Key = key,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}) {
				ICryptoTransform tr = trip.CreateDecryptor ();
				byte[] results = tr.TransformFinalBlock (data, 0, data.Length);
				return UTF8Encoding.UTF8.GetString (results);
			}
		}
	}
}
