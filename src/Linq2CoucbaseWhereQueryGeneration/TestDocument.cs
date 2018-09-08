using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Couchbase.Linq.Filters;
using Linq2CoucbaseWhereQueryGeneration.Identity;
using Newtonsoft.Json;

namespace Linq2CoucbaseWhereQueryGeneration
{
    [JsonConverter( typeof( JsonIdentityConverter ) )]
    public class TestDocumentId : AbstractGuidIdentity<TestDocumentId>
    {
        public TestDocumentId( Guid id ) : base( id ) { }

        public static implicit operator TestDocumentId( string id )
        {
            return Parse( id );
        }
    }


    public class TestDocument
    {
        [Key]
        public TestDocumentId Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }


        public TestDocument()
        {
            Type = "TestDocument";
        }
    }


    public class TestDocumentFilter : IDocumentFilter<TestDocument>
    {
        public int Priority { get; set; }


        public IQueryable<TestDocument> ApplyFilter( IQueryable<TestDocument> source )
        {
            return source.Where( s => s.Type == "TestDocument" );
        }
    }
}
