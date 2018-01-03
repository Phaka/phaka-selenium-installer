// Copyright (c) Werner Strydom. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Phaka.Selenium.Installers.CustomActions
{
    using System;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.Deployment.WindowsInstaller;

    public class CustomActions
    {
        [CustomAction]
        public static ActionResult ValidateUsername(Session session)
        {
            session.Log("Begin " + nameof(ValidateUsername));
            var s = session["SELENIUM_SERVICE_USERNAME"];
            s = s.Trim();
            session["SELENIUM_SERVICE_USERNAME"] = s;

            bool isValid = true;

            if (string.IsNullOrEmpty(s))
                isValid = false;
            else if (s.Length > 20)
                isValid = false;

            session["SELENIUM_SERVICE_USERNAME_VALID"] = isValid ? "1" : "0";
            session.Log("Completed " + nameof(ValidateUsername));
            return ActionResult.Success;
        }


        [CustomAction]
        public static ActionResult ValidatePassword(Session session)
        {
            session.Log("Begin " + nameof(ValidatePassword));
            var s = session["SELENIUM_SERVICE_PASSWORD"];
            s = s.Trim();
            session["SELENIUM_SERVICE_PASSWORD"] = s;

            bool isValid = true;
          
            if (string.IsNullOrEmpty(s))
                isValid = false;
            else if (s.Length < 12 || s.Length > 128)
                isValid = false;

            session["SELENIUM_SERVICE_PASSWORD_VALID"] = isValid ? "1" : "0";
            session.Log("Completed " + nameof(ValidatePassword));
            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult ValidatePortNumber(Session session)
        {
            session.Log("Begin " + nameof(ValidatePortNumber));
            var s = session["SELENIUM_PORT"];
            s = s.Trim();
            session["SELENIUM_PORT"] = s;

            bool isValid = true;

            if (!ushort.TryParse(s, out var result))
                isValid = false;
            else if (result == 0)
                isValid = false; // can't use port 0

            session["SELENIUM_PORT_VALID"] = isValid ? "1" : "0";

            session.Log("Completed " + nameof(ValidatePortNumber));
            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult ValidateUrl(Session session)
        {
            session.Log("Begin " + nameof(ValidateUrl));
            var s = session["SELENIUM_HUB_BASEURL"];
            s = s.Trim();
            session["SELENIUM_HUB_BASEURL"] = s;

            bool isValid = true;
            if (!Uri.TryCreate(s, UriKind.Absolute, out var result))
                isValid = false;
            else if (result.Scheme != "http")
                isValid = false;

            session["SELENIUM_HUB_BASEURL_VALID"] = isValid ? "1" : "0";
            if (isValid)
            {
                // Append the 'grid/register' path
                if (result.PathAndQuery != "grid/register")
                {
                    result = new Uri(result, "grid/register");
                }
                session["SELENIUM_HUB_BASEURL"] = result.AbsoluteUri;
            }
            session.Log("Completed " + nameof(ValidateUrl));
            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult TestUrl(Session session)
        {
            session.Log("Begin " + nameof(TestUrl));

            var url = session["SELENIUM_HUB_BASEURL"];
            url = url.Trim();
            session["SELENIUM_HUB_BASEURL"] = url;

            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                HttpStatusCode statusCode;
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    statusCode = response.StatusCode;
                }

                session.Log("The connection to {0} returned {1}", url, statusCode);
                if (statusCode != HttpStatusCode.OK)
                    session["SELENIUM_HUB_BASEURL_TEST"] = "0";
                else
                    session["SELENIUM_HUB_BASEURL_TEST"] = "1";
            }
            catch (Exception e)
            {
                session.Log("Exception retrieving '{0}': {1}", url, e);
                session["SELENIUM_HUB_BASEURL_TEST"] = "0";
            }

            session.Log("Completed " + nameof(TestUrl));
            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult GeneratePassword(Session session)
        {
            session.Log("Begin " + nameof(GeneratePassword));

            var password = session["SELENIUM_SERVICE_PASSWORD"];
            password = password.Trim();
            if (string.IsNullOrEmpty(password))
            {
                var data = new byte[128];
                var rng = new RNGCryptoServiceProvider();
                rng.GetBytes(data);

                byte[] buffer = null;
                var alphabet = "abcdefghjkmnpqrstvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()[]{}\\/:'?<>'~`";
                var max = byte.MaxValue - (byte.MaxValue + 1) % alphabet.Length;
                var result = new char[24];

                for (var i = 0; i < 24; i++)
                {
                    var b = data[i];
                    while (b > max)
                    {
                        if (buffer == null)
                            buffer = new byte[1];

                        rng.GetBytes(buffer);
                        b = buffer[0];
                    }

                    result[i] = alphabet[b % alphabet.Length];
                }

                var s = new string(result);
                session["SELENIUM_SERVICE_PASSWORD"] = s;
            }
            else
            {
                session.Log("The 'SELENIUM_SERVICE_PASSWORD' property already has a value and will not be replaced.");
            }

            session.Log("Completed " + nameof(GeneratePassword));
            return ActionResult.Success;
        }
    }

   
}