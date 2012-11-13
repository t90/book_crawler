using System;
using System.IO;

namespace BookCrawler
{
	public class ReadStream : Stream
	{
		#region implemented abstract members of Stream

		public override void Flush ()
		{
		}

		public override long Seek (long offset, SeekOrigin origin)
		{
			throw new NotImplementedException ();
		}

		public override void SetLength (long value)
		{
			throw new NotImplementedException ();
		}

		public override int Read (byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException ();
		}



		public override void Write (byte[] buffer, int offset, int count)
		{
			if(_storageLeft <= 0){
				throw new IndexOutOfRangeException("No more space left in the ReadStream");
			}
			var copyBytes = Math.Min(count,_storageLeft);
			Array.Copy(buffer,offset,_buffer,_index,copyBytes);
			_storageLeft -= copyBytes;
			_index += copyBytes;
		}

		public override bool CanRead {
			get {
				return false;
			}
		}

		public override bool CanSeek {
			get {
				return false;
			}
		}

		public override bool CanWrite {
			get {
				return true;
			}
		}

		public override long Length {
			get {
				return _length;
			}
		}

		public override long Position {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		#endregion

		private int _length = 0;
		private byte[] _buffer;
		private int _index = 0;
		private int _storageLeft;

		public ReadStream (byte[] buffer)
		{
			_buffer = buffer;
			_storageLeft = buffer.Length;
		}
	}
}

