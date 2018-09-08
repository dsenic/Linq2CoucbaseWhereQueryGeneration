using System;
using System.Collections.Generic;
using System.Linq;

using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Couchbase.Linq;
using Couchbase.Linq.Filters;
using Couchbase.Linq.Serialization;
using Linq2CoucbaseWhereQueryGeneration.Identity;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Linq2CoucbaseWhereQueryGeneration
{
    class Program
    {
        private static Cluster _cluster;
        private static IBucket _bucket;
        private static BucketContext _bucketContext;

        private static TestDocumentId TestId = TestDocumentId.Parse( "testdocument-f1514cc6-dc70-4e1d-9b53-3c506e58c881" );


        static void Main(string[] args)
        {
            InitializeCouchbase();

            //Write();

            Read();

            Console.ReadLine();
        }


        public static void Write()
        {
            var test = new TestDocument();
            test.Id = TestId;
            test.Name = "Test document";

            _bucketContext.Save( test );
        }


        public static void Read()
        {
            var document = _bucketContext.Query<TestDocument>().Where( x => x.Id == TestId ).FirstOrDefault();

            // This is working
            //var document = _bucketContext.Query<TestDocument>().Where( x => x.Id == TestId.ToString() ).FirstOrDefault();

            if ( document != null )
            {
                Console.WriteLine( document.Id );
                Console.WriteLine( document.Name );
            }
            else
            {
                Console.WriteLine( "Not found" );
            }
        }


        private static void InitializeCouchbase()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddNLog( new NLogProviderOptions
            {
                CaptureMessageTemplates = true,
                CaptureMessageProperties = true
            } );
            NLog.LogManager.LoadConfiguration( "nlog.config" );

            _cluster = new Cluster
            (
                new ClientConfiguration
                {
                    LoggerFactory = loggerFactory,
                    Servers = new List<Uri> { new Uri( "http://localhost:8091/" ) },
                    Serializer = () => new CustomIdEnabledSerializer()
                }
            );

            var authenticator = new PasswordAuthenticator( "Administrator", "trustno1" );
            _cluster.Authenticate( authenticator );

            _bucket = _cluster.OpenBucket( "test" );

            _bucketContext = new BucketContext( _bucket );

            DocumentFilterManager.SetFilter( new TestDocumentFilter() );

            TypeBasedSerializationConverterRegistry.Global.Add( typeof( JsonIdentityConverter ), typeof( TestDocumentIdSerializationConverter ) );
        }
    }
}
