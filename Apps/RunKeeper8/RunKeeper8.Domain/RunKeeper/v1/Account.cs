﻿using RunKeeper8.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPhone.Contracts.Storage;

namespace RunKeeper8.Domain.RunKeeper.v1
{
    public class Account : IAccount
    {

        private IItem _storageFile;

        public Account(IItem storageFile):this()
        {

            _storageFile = storageFile;

            if (string.IsNullOrEmpty(_storageFile.Name)) _storageFile.Name = "RunKeeperAccessToken";
            
        }
        public void Load()
        {
            _storageFile.Load().Wait();
            if (_storageFile.Contents != null)
            {
                _AccessToken = System.Text.Encoding.UTF8.GetString(_storageFile.Contents, 0, _storageFile.Contents.Length);
            }
        }
        public void Save()
        {

            _storageFile.Contents = System.Text.Encoding.UTF8.GetBytes(_AccessToken);
            _storageFile.Save().Wait();

        }


        public Account()
        {
            ClientID = "67778de52c0442e296e3ed149fec3af3";
            ClientSecret = "0a0cfce7c10c4a5b9e7d2eb9342083a9";
            AuthorizationURL = "https://runkeeper.com/apps/authorize";
            AccessTokenURL = "https://runkeeper.com/apps/token";
            DeAuthorizationURL = "https://runkeeper.com/apps/de-authorize";
            RedirectURL = "http://robchartier.ca";
            RecordActivityUrl = "http://api.runkeeper.com/fitnessActivities";

        }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string AuthorizationURL { get; set; }
        public string AccessTokenURL { get; set; }
        public string DeAuthorizationURL { get; set; }
        public string RedirectURL { get; set; }
        public string Code { get; set; }
        private string _AccessToken;

        public string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_AccessToken)) this.Load();
                return _AccessToken;
            }
            set
            {
                _AccessToken = value;
                this.Save();
            }
        }

        public string RecordActivityUrl { get; set; }

        public string RecordActivityEndPoint()
        {
            return RecordActivityUrl;
        }

        public string AuthorizationEndPoint()
        {
            string url = AuthorizationURL;
            if (!url.Contains("?"))
            {
                url = url + "?";
            }
            else
            {
                if (!url.EndsWith("&")) url = url + "&";
            }
            
            url = url + "client_id=" + System.Uri.EscapeUriString(ClientID);
            url = url + "&response_type=code";
            url = url + "&redirect_uri=" + System.Uri.EscapeUriString(RedirectURL);
            return url;

        }

        public string TokenEndPoint()
        {
            return AccessTokenURL;
        }


        public string TokenParameters()
        {
            var url = "";
            url = url + "client_id=" + System.Uri.EscapeUriString(ClientID);
            url = url + "&response_type=code";
            url = url + "&code=" + System.Uri.EscapeUriString(Code);
            url = url + "&client_secret=" + System.Uri.EscapeUriString(ClientSecret);
            url = url + "&redirect_uri=" + System.Uri.EscapeUriString(RedirectURL);
            url = url + "&grant_type=authorization_code";
            
            return url;
        }
    }
}
