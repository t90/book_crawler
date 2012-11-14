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
				var archiveName = @"C:\projects\book_crawler\test.zip";
				using (var zip = ZipFile.Read(archiveName)) {
					foreach(var entry in zip.Entries){
						var bookInfoScraper = new BookInfoScraper ();
						var stream = new ReadStream(bookInfoScraper.Buffer);
						try{
							entry.Extract(stream);
						}
						catch(FormatException fe){
							throw new ApplicationException(string.Format("Error processing file {0} from archive {1}",entry.FileName, archiveName),fe);
						}
						catch(IndexOutOfRangeException){
							;
						}
						var book = bookInfoScraper.GetBookInfo();
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
