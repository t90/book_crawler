using System;
using System.IO;
using System.Text.RegularExpressions;

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

		public BookInfo GetBookInfo(){
//			var recievedBytes = bookReader.ReadBlock (_buffer, 0, _buffer.Length);
//			var operationalString = new String (_buffer, 0, recievedBytes);
//			var encodingMatch = Regex.Match(operationalString,"<?xml[^>]*encoding=\"([^\"]*)\"[^>]*>");

			return null;
		}

	}
}

