using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.Core.Common.DataLayer;

namespace AlwaysMoveForward.OAuth2.DataLayer.DataMapper
{
    /// <summary>
    /// A data mapper going to/from the domain model and the dto
    /// </summary>
    internal class LoginAttemptDataMapper : DataMapBase<LoginAttempt, Models.LoginAttempts>
    {
        /// <summary>
        /// The static constructor sets up automapper
        /// </summary>
        static LoginAttemptDataMapper()
        {
            DataMapConfiguration.Configure();
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override LoginAttempt Map(Models.LoginAttempts source, LoginAttempt destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override Models.LoginAttempts Map(LoginAttempt source, Models.LoginAttempts destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
