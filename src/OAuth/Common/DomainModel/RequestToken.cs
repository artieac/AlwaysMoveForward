using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Framework;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Common.Factories;

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
            this.DateAuthorized = DateTime.MaxValue;
        }

        public RequestToken(string consumerKey, Realm parsedRealm, string callbackUrl)
            : base()
        {
            this.ConsumerKey = consumerKey;
            this.Realm = parsedRealm;
            this.CallbackUrl = callbackUrl;
        }
        /// <summary>
        /// Gets or sets the Database id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Gets or sets the callback url
        /// </summary>
        public String CallbackUrl { get; private set; }

        /// <summary>
        /// Gets or sets the current access state
        /// </summary>
        public TokenState State { get; private set; }

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
        public Realm Realm { get; private set; }

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
        public DateTime ExpirationDate { get; private set; }

        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        public virtual DateTime DateCreated { get; private set; }

        /// <summary>
        /// Gets or sets the verifier code established during authorization
        /// </summary>
        public string VerifierCode { get; private set; }

        /// <summary>
        /// Gets or sets the user that authorized this token
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// Gets or sets the username of the user that authorized this token
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// The date that the request token was authorized
        /// </summary>
        public DateTime? DateAuthorized { get; private set; }

        /// <summary>
        /// Gets or sets the Access token associated with this request
        /// </summary>
        public AccessToken AccessToken { get; private set; }

        public void GenerateToken()
        {
            this.Token = Guid.NewGuid().ToString();
            this.Secret = Guid.NewGuid().ToString();
        }

        public void DenyAccess()
        {
            this.State = TokenState.AccessDenied;
        }

        public void Authorize(Realm realm)
        {
            this.Authorize(realm, RequestTokenAuthorizer.GenerateVerifierCode());
        }

        public void Authorize(Realm realm, string verifierCode)
        {
            if (realm != null)
            {
                this.DateAuthorized = DateTime.UtcNow;
                this.UserName = realm.DataName;
                this.UserId = long.Parse(realm.DataId);
                this.VerifierCode = verifierCode;
                this.State = TokenState.AccessGranted;
            }
        }

        public void Authorize(AMFUserLogin currentUser)
        {
            this.Authorize(currentUser, RequestTokenAuthorizer.GenerateVerifierCode());
        }

        public void Authorize(AMFUserLogin currentUser, string verifierCode)
        {
            if(currentUser != null)
            {
                this.DateAuthorized = DateTime.UtcNow;
                this.UserName = currentUser.Email;
                this.UserId = currentUser.Id;
                this.VerifierCode = verifierCode;
                this.State = TokenState.AccessGranted;
            }
        }

        public bool UsedUp
        {
            get
            {
                bool retVal = false;

                if ((this.IsAuthorized() == true && this.AccessToken != null) ||
                   this.ExpirationDate < DateTime.UtcNow)
                {
                    retVal = true;
                }

                return retVal;
            }
        }

        public bool IsAuthorized()
        {
            bool retVal = false;

            if(!string.IsNullOrEmpty(VerifierCode) &&
                DateAuthorized != DateTime.MaxValue)
            {
                retVal = true;
            }

            return retVal;
        }
        /// <summary>
        /// Generate the full callback url from the elements contained in the class
        /// </summary>
        /// <returns></returns>
        public string GenerateCallBackUrl()
        {
            string retVal = string.Empty;

            if (this.IsAuthorized() == true)
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
                    retVal += "&" + Constants.VerifierCodeParameter + "=" + UriUtility.UrlEncode(this.VerifierCode);
                }
            }

            return retVal;
        }

        public AccessToken GrantAccessToken(Consumer consumer)
        {
            if (this.IsAuthorized() == true && this.AccessToken == null)
            {
                AccessToken newAccessToken = TokenFactory.CreateAccessToken(this, consumer.AccessTokenLifetime);
                this.AccessToken = newAccessToken;
                this.State = TokenState.AccessGranted;               
            }

            return this.AccessToken;
        }
    }
}
