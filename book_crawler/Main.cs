using System;
using System.IO;
using Ionic.Zip;

namespace BookCrawler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try{
				using (var zip = ZipFile.Read(@"C:\projects\book_crawler\test.zip")) {
					foreach(var entry in zip.Entries){
						var bookInfoScraper = new BookInfoScraper ();
						var stream = new ReadStream(bookInfoScraper.Buffer);
						try{
							entry.Extract(stream);
						}
						catch(IndexOutOfRangeException ex){

						}

//						entry.O.Read(bookInfoScraper.Buffer,0, bookInfoScraper.Buffer.Length);
						bookInfoScraper.GetBookInfo();
					}
				}
			}
			catch(Exception e){
				Console.WriteLine(e);
			}

//			var bookInfoScraper = new BookInfoScraper ();
//			var bookInfo = bookInfoScraper.GetBookInfo(new StreamReader(@"C:\projects\book_crawler\eskov_pk.fb2"));
		}
	}
}
