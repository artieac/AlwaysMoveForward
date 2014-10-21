using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "RequestTokenAuthorization")]
    public class RequestTokenAuthorization
    {
        /// <summary>
        /// A string to represent the field name for creating queries
        /// </summary>
        public const string IdFieldName = "Id";

        /// <summary>
        /// A string to represent the request token field name for creating queries
        /// </summary>
        public const string RequestTokenFieldName = "RequestToken";

        /// <summary>
        /// A string to represent the verifier code field name for creating queries
        /// </summary>
        public const string VerifierCodeFieldName = "VerifierCode";

        /// <summary>
        /// A string to represent the verifier code field name for creating queries
        /// </summary>
        public const string UserIdFieldName = "UserId";

        /// <summary>
        /// A string to represent the verifier code field name for creating queries
        /// </summary>
        public const string UserNameFieldName = "UserName";

        /// <summary>
        /// Default constructor for the class.
        /// </summary>
        public RequestTokenAuthorization()
        {
            this.Id = 0;
        }

        /// <summary>
        /// Gets or sets the database id.
        /// </summary>
        [NHibernate.Mapping.Attributes.Id(0, Name = IdFieldName, UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")] 
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the user id that authorized the request
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual long UserId { get; set; }

        /// <summary>
        /// Gets or sets the username that authorized the request.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the generated verifier code when authorized.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual string VerifierCode { get; set; }

        /// <summary>
        /// The date the token was authorized.
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateAuthorized { get; set; }
    }
}
