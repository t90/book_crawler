using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

namespace BookCrawler
{
	public class BookInfoScraper
	{
		public BookInfoScraper ()
		{
		}

		byte[] _buffer = new byte[0x2000];

		public byte[] Buffer{
			get{
				return _buffer;
			}
		}

		IEnumerable<BookInfo.Author> GetAuthors (XmlNodeList xmlNodeList)
		{
			foreach(XmlNode node in xmlNodeList){
				yield return new BookInfo.Author{
					First = node.SelectSingleNode("//first-name").Value,
					Middle = node.SelectSingleNode("//middle-name").Value,
					Last = node.SelectSingleNode("//last-name").Value,
				};

			}
		}

		public BookInfo GetBookInfo(){
			var memory = new MemoryStream(_buffer,0, _buffer.Length);
			var reader = new StreamReader(memory);
			var operationalString = reader.ReadToEnd();
			var encodingMatch = Regex.Match(operationalString,"<?xml[^>]*encoding=\"([^\"]*)\"[^>]*>");
			if(encodingMatch.Success){
				memory.Position = 0;
				reader = new StreamReader(memory,Encoding.GetEncoding(encodingMatch.Groups[1].Value));
				operationalString = reader.ReadToEnd();
			}

			var fbMatch = Regex.Match(operationalString, "<([\\w:]*)FictionBook [^>]*xmlns([\\w:]*)=\"http://www.gribuser.ru/xml/fictionbook/2.0\"[^>]*>");
			if(!fbMatch.Success){
				throw new FormatException("invalid fb2 file. Can not find default namespace");
			}

			var prefix = fbMatch.Groups[2].Value;

			operationalString = Regex.Replace(operationalString,"[\\r\\n]+","");
//			operationalString = operationalString.Replace("\r","").Replace("\n","");

			var descriptionMatch = Regex.Match(operationalString,string.Format("<{0}description[^>]*>.*</{0}description>",prefix));
			if(!descriptionMatch.Success){
				throw new FormatException("invalid fb2 file. Can not find description section");
			}

			var doc = new XmlDocument();
			doc.LoadXml(descriptionMatch.Groups[0].Value);
			var nav = doc.CreateNavigator();


			return new BookInfo(){
				Genre = doc.GetElementsByTagName("genre")[0].InnerText,
				Title = doc.GetElementsByTagName("book-title")[0].InnerText,
				Authors = GetAuthors(doc.GetElementsByTagName("author")).ToArray()
			};
		}

	}
}

