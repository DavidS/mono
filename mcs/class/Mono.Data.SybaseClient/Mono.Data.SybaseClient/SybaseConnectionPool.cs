//
// Mono.Data.SybaseClient.SybaseConnectionPool.cs
//
// Author:
//   Tim Coleman (tim@timcoleman.com)
//
// Copyright (C) 2002 Tim Coleman
//

using Mono.Data.Tds.Protocol;
using System;
using System.Collections;
using System.Threading;

namespace Mono.Data.SybaseClient {
        internal class SybaseConnectionPool : MarshalByRefObject, IList, ICollection, IEnumerable
	{
		#region Fields

		ArrayList list = new ArrayList ();

		int maxSize;
		int minSize;
		int packetSize;
		int port;
		int timeout;

		string dataSource;

		#endregion // Fields

		#region Constructors

		public SybaseConnectionPool (string dataSource, int port, int packetSize, int timeout, int minSize, int maxSize)
		{
			this.dataSource = dataSource;
			this.port = port;
			this.packetSize = packetSize;
			this.timeout = timeout;
			this.minSize = minSize;
			this.maxSize = maxSize;
		}

		#endregion // Constructors

		#region Properties

		public ITds this[int index] {
			get { return (ITds) list[index]; }
		}

		object IList.this[int index] {
			get { return this[index]; }
			set { throw new InvalidOperationException (); }
		}

		public int Count {
			get { return list.Count; }
		}

		public bool IsFixedSize {
			get { return false; }
		}

		public bool IsReadOnly {
			get { return true; }
		}

		public bool IsSynchronized {
			get { return false; }
		}

		public int MaxSize {
			get { return maxSize; }
		}

		public int MinSize {
			get { return minSize; }
		}

		public object SyncRoot {
			get { throw new InvalidOperationException (); }
		}

		#endregion // Properties

		#region Methods

		public int Add (object o)
		{
			return list.Add ((ITds) o);
		}

		public void Clear ()
		{
			list.Clear ();
		}

		public bool Contains (object o)
		{
			return list.Contains ((ITds) o);
		}

		public void CopyTo (Array array, int index)
		{
			list.CopyTo (array, index);
		}

		public IEnumerator GetEnumerator ()
		{
			return list.GetEnumerator ();
		}

		[MonoTODO]
		public ITds AllocateConnection ()
		{
			// make sure we have the minimum count (really only useful the first time)
			lock (list) {
				for (int i = Count; i < minSize; i += 1)
					Add (new Tds50 (dataSource, port, packetSize, timeout));
			}

			// Try to obtain a lock
			foreach (object o in list)
				if (Monitor.TryEnter (o))
					return (ITds) o;

			if (Count < maxSize) {
				ITds tds = new Tds50 (dataSource, port, packetSize, timeout);
				Monitor.Enter (tds);
				Add (tds);
				return tds;
			}

			// else we have to wait for one to be available
			
			return null;
		}

		public void ReleaseConnection (ITds tds)
		{
			Monitor.Exit (tds);
		}

		public int IndexOf (object o)
		{
			return list.IndexOf ((ITds) o);
		}

		public void Insert (int index, object o)
		{
			list.Insert (index, (ITds) o);
		}

		public void Remove (object o)
		{
			list.Remove ((ITds) o);
		}

		public void RemoveAt (int index)
		{
			list.RemoveAt (index);
		}

		#endregion // Methods
	}
}
