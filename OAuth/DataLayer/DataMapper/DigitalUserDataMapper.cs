using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.DataLayer.DataMapper
{
    /// <summary>
    /// A data mapper going to/from the domain model and the dto
    /// </summary>
    internal class DigitalUserDataMapper : DataMapBase<AMFUserLogin, DTO.DigitalUser>
    {        
        /// <summary>
        /// The static constructor sets up automapper
        /// </summary>
        static DigitalUserDataMapper()
        {
            var existingMap = Mapper.FindTypeMapFor<LoginAttempt, DTO.LoginAttempt>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<LoginAttempt, DTO.LoginAttempt>();
            }

            existingMap = Mapper.FindTypeMapFor<DTO.LoginAttempt, LoginAttempt>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.LoginAttempt, LoginAttempt>();
            }

            existingMap = Mapper.FindTypeMapFor<AMFUserLogin, DTO.DigitalUser>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<AMFUserLogin, DTO.DigitalUser>();

            }

            existingMap = Mapper.FindTypeMapFor<DTO.DigitalUser, AMFUserLogin>();
            if (existingMap == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.DigitalUser, AMFUserLogin>();
            }

        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override AMFUserLogin Map(DTO.DigitalUser source, AMFUserLogin destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Tell AutoMapper what you want to map
        /// </summary>
        /// <param name="source">the source of the data</param>
        /// <param name="destination">The destination instance of the data</param>
        /// <returns>The destination populated with the source</returns>
        public override DTO.DigitalUser Map(AMFUserLogin source, DTO.DigitalUser destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
