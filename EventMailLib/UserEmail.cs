using System;

namespace EventMailLib
{
    public class UserEmail
    {
        /// <summary>
        /// EmailAddress used for registration and should be kept unique so that
        /// each registered user has its own EmailAddress.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// UserName might be used...
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Todo: use one way encryption for password 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// UserValidated tells us if the emailaddress was validated, either
        /// via the console application or via the website or maybe even
        /// because we sent out a link to the emailaddress that triggers
        /// the appropriate website action for validation.
        /// </summary>
        public bool UserValidated { get; set; }
    }
}
