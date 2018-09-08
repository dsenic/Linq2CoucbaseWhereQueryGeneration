using System;
using System.Collections.Generic;
using System.Text;

namespace Linq2CoucbaseWhereQueryGeneration.Identity
{
    public static class IdConverter
    {
        public static TIdentity Convert<TIdentity>( string value ) where TIdentity : class, IIdentity
        {
            return ( Convert( value, typeof( TIdentity ) ) as TIdentity );
        }


        public static IIdentity Convert( string id, Type targetType )
        {
            if ( id == null )
            {
                throw new ArgumentNullException( "id is null" );
            }

            string [ ] parts = id.Split( new char [ ] { '-' }, 2, StringSplitOptions.None );

            if ( parts.Length != 2 )
            {
                throw new InvalidOperationException();
            }

            string tag = parts [ 0 ];
            string value = parts [ 1 ];

            return Activator.CreateInstance( targetType, Guid.Parse( value ) ) as IIdentity;
        }
    }
}
