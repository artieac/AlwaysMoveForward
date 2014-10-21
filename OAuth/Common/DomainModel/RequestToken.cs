﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Framework;
using AlwaysMoveForward.OAuth.Contracts;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// A class representing a Request Token
    /// </summary>
    public class RequestToken : IToken, IOAuthToken
    {       
        /// <summary>
        /// The default lifetime for RequestTokens
        /// </summary>
        private const int DefaultLifetimeInMinutes = 1440;

        /// <summary>
        /// A default constructor for the class
        /// </summary>
        public RequestToken()
        {
            this.Id = 0;
            this.DateCreated = DateTime.UtcNow;
            this.ExpirationDate = DateTime.UtcNow.AddMinutes(RequestToken.DefaultLifetimeInMinutes);
        }

        /// <summary>
        /// Gets or sets the Database id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the callback url
        /// </summary>
        public String CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets if the token has been used yet
        /// </summary>
        public bool UsedUp { get; set; }

        /// <summary>
        /// Gets or sets the current access state
        /// </summary>
        public TokenState State { get; set; }

        /// <summary>
        /// Gets or sets the session handle (used by DevDefined)
        /// </summary>
        public string SessionHandle { get; set; }

        /// <summary>
        /// Gets or sets the token string
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the secret
        /// </summary>
        string IToken.TokenSecret 
        {
            get { return this.Secret; }
            set { this.Secret = value; }
        }

        /// <summary>
        /// Gets or sets the key of the Consumer
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the realm.
        /// </summary>
        public Realm Realm { get; set; }

        /// <summary>
        /// An explicit implementation to satisfy the IConsumer interface
        /// </summary>
        string IConsumer.Realm
        {
            get
            {
                string retVal = string.Empty;

                if (this.Realm != null)
                {
                    retVal = this.Realm.ToString();
                }

                return retVal;
            }
            set { this.Realm = Realm.Parse(value); }
        }

        /// <summary>
        /// Gets or sets the realm.
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the authorization information for this request
        /// </summary>
        public RequestTokenAuthorization RequestTokenAuthorization { get; set; }

        /// <summary>
        /// Gets or sets the Access token associated with this request
        /// </summary>
        public AccessToken AccessToken { get; set; }

        /// <summary>
        /// Generate the full callback url from the elements contained in the class
        /// </summary>
        /// <returns></returns>
        public string GenerateCallBackUrl()
        {
            string retVal = string.Empty;

            if (this.RequestTokenAuthorization != null)
            {
                if (!string.IsNullOrEmpty(this.CallbackUrl))
                {
                    retVal = this.CallbackUrl;

                    if (!retVal.Contains("?"))
                    {
                        retVal += "?";
                    }
                    else
                    {
                        retVal += "&";
                    }

                    retVal += Constants.TokenParameter + "=" + UriUtility.UrlEncode(this.Token);
                    retVal += "&" + Constants.VerifierCodeParameter + "=" + UriUtility.UrlEncode(this.RequestTokenAuthorization.VerifierCode);
                }
            }

            return retVal;
        }
    }
}