using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Couchbase.Linq.QueryGeneration;
using Couchbase.Linq.Serialization;
using Couchbase.Linq.Serialization.Converters;

namespace Linq2CoucbaseWhereQueryGeneration
{
    public class TestDocumentIdSerializationConverter :
        SerializationConverterBase,
        ISerializationConverter<TestDocumentId>
    {
        private static readonly IDictionary<Type, MethodInfo> ConvertFromMethodsStatic =
            GetConvertFromMethods<TestDocumentIdSerializationConverter>();

        private static readonly IDictionary<Type, MethodInfo> ConvertToMethodsStatic =
            GetConvertToMethods<TestDocumentIdSerializationConverter>();


        protected override IDictionary<Type, MethodInfo> ConvertFromMethods => ConvertFromMethodsStatic;
        protected override IDictionary<Type, MethodInfo> ConvertToMethods => ConvertToMethodsStatic;


        public TestDocumentId ConvertFrom( TestDocumentId value )
        {
            return value;
        }


        public TestDocumentId ConvertTo( TestDocumentId value )
        {
            return value;
        }

        protected override void RenderConvertedConstant( ConstantExpression constantExpression, IN1QlExpressionTreeVisitor expressionTreeVisitor )
        {
            expressionTreeVisitor.Expression.Append( "'" );
            expressionTreeVisitor.Visit( constantExpression );
            expressionTreeVisitor.Expression.Append( "'" );
        }

        protected override void RenderConvertFromMethod( Expression innerExpression, IN1QlExpressionTreeVisitor expressionTreeVisitor )
        {
            expressionTreeVisitor.Visit( innerExpression );
        }

        protected override void RenderConvertToMethod( Expression innerExpression, IN1QlExpressionTreeVisitor expressionTreeVisitor )
        {
            expressionTreeVisitor.Visit( innerExpression );
        }
    }
}
