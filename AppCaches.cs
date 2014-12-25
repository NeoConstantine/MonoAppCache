/*
* 说明：定义移动端cache的高级设置。用xml去模拟cache
* 创建人：田琳元
* 创建时间：2014-01-14
*/
using System;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace MonoServices.Caches
{
	[Serializable]
	public class AppCaches 
	{
		public AppCaches(){}

		/// <summary>
		/// 设置cache相关属性
		/// </summary>
		/// <returns>The cookies.</returns>
		/// <param name="key">Key.</param>
		public object SetCaches(string key,object value)
		{
			XmlDocument doc = new XmlDocument ();
			doc.Load (FileName);
			XmlNode root = doc.SelectSingleNode("root");
			XmlNodeList child = root.ChildNodes;
			foreach (XmlNode c in child) {
				XmlElement keyNode = (XmlElement)c;
				if (keyNode.Name == key) {
					keyNode.ParentNode.RemoveChild (c);
				}

			}
			XmlElement xmlele = doc.CreateElement (key);
			xmlele.InnerText = JsonConvert.SerializeObject(value);
			root.AppendChild (xmlele);

			doc.Save (FileName);
			return value;
		}

		/// <summary>
		/// 获取cookie相关属性
		/// </summary>
		/// <returns>The cookies.</returns>
		/// <param name="key">Key.</param>
		public object GetCache(string key)
		{
			XmlDocument doc = new XmlDocument ();
			doc.Load (FileName);
			object obj = null;
			XmlNode root = doc.SelectSingleNode("root");
			XmlNodeList child = root.ChildNodes;
			foreach (XmlNode c in child) {
				XmlElement keyNode = (XmlElement)c;
				if (keyNode.Name == key) {
					obj = JsonConvert.DeserializeObject (keyNode.InnerText);
				}

			}
			return obj;
		}




		/// <summary>
		/// 获取xml完整路径+文件名称
		/// </summary>
		/// <returns>The filename.</returns>
		public static string GetFilename()
		{
			//获取移动端文件路径方法
			var documents =	Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			//构建文件路径＋文件名称
			var filename = Path.Combine (documents, "SeaCache.xml");
			if (!File.Exists (filename)) {
				File.WriteAllText (filename, @"<?xml version=""1.0"" encoding=""utf-8"" ?><root></root>");
			}
			return filename;
		}


		/// <summary>
		/// 设置或获取cookie属性，
		/// </summary>
		/// <value>The app cookie.</value>
		public static AppCaches AppCache {
			get{
				return (AppCaches) new AppCaches();
			}
			set{ 

			}
		}

		/// <summary>
		/// 设置cookie索引器
		/// </summary>
		/// <param name="key">Key.</param>
		public object this[string key]{
			get{ 
				return GetCache (key);
			}
			set{ 
				this.SetCaches (key, value);
			}
		}

		/// <summary>
		/// 获取xml（完整路径+名称）
		/// </summary>
		/// <value>The name of the file.</value>
		public static string FileName{
			get{ 
				return GetFilename ();
			}
		}
	}
}

