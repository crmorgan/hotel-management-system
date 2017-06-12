using System;
using Raven.Client;
using Raven.Client.Document;

namespace Reservations.Data
{
	public class DocumentStoreHolder
	{
		private static readonly Lazy<IDocumentStore> InternalStore = new Lazy<IDocumentStore>(CreateStore);

		public static IDocumentStore Store => InternalStore.Value;

		private static IDocumentStore CreateStore()
		{
			var store = new DocumentStore
			{
				Url = "http://localhost:8080",
				DefaultDatabase = "Reservations"
			}.Initialize();

			return store;
		}
	}
}