using System;

namespace Linq2CoucbaseWhereQueryGeneration.Identity
{
    public abstract class AbstractGuidIdentity<TIdentity> : IIdentity where TIdentity : class, IIdentity
    {
        private readonly Guid _id;
        public readonly static string TagValue;


        static AbstractGuidIdentity()
        {
             TagValue = typeof( TIdentity ).Name.Replace( "Identity", "" ).Replace( "Id", "" ).ToLowerInvariant();
        }


        public string GetId()
        {
            return string.Format( "{0}-{1}", GetTag(), _id ).ToLowerInvariant();
        }


        public string GetTag()
        {
            return TagValue;
        }


        protected AbstractGuidIdentity( Guid id )
        {
            if ( id == Guid.Empty )
            {
                throw new ArgumentException( nameof( id ) );
            }

            _id = id;
        }


        public override string ToString()
        {
            return GetId();
        }


        public static TIdentity NewId()
        {
            Type type = typeof( TIdentity );
            object [ ] objArray = new object [ ] { Guid.NewGuid() };
            return ( TIdentity ) Activator.CreateInstance( type, objArray );

        }


        public override bool Equals( object obj )
        {
            if ( object.ReferenceEquals( null, obj ) )
            {
                return false;
            }

            if ( object.ReferenceEquals( this, obj ) )
            {
                return true;
            }

            IIdentity identity = obj as IIdentity;

            if ( object.ReferenceEquals( null, identity ) )
            {
                return false;
            }

            return Equals( identity );
        }


        public bool Equals( IIdentity other )
        {
            if ( object.ReferenceEquals( null, other ) )
            {
                return false;
            }

            return other.GetId().Equals( GetId(), StringComparison.OrdinalIgnoreCase );
        }


        public static bool operator == ( AbstractGuidIdentity<TIdentity> left, AbstractGuidIdentity<TIdentity> right )
        {
            if ( object.ReferenceEquals( left, null ) )
            {
                return object.ReferenceEquals( right, null );
            }

            return left.Equals( right );
        }


        public static bool operator != ( AbstractGuidIdentity<TIdentity> left, AbstractGuidIdentity<TIdentity> right )
        {
            return !( left == right );
        }


        public static implicit operator String( AbstractGuidIdentity<TIdentity> id )
        {
            if ( id == null )
            {
                return null;
            }

            return id.GetId();
        }


        public override int GetHashCode()
        {
            return GetId().GetHashCode();
        }


        public static TIdentity Parse( string id )
        {
            return IdConverter.Convert<TIdentity>( id );
        }
    }
}
