using System;
namespace ServeAPI.Paypal
{
    public class ClientMetadata
    {
        public string name { get; set; }
        public string display_name { get; set; }
        public string logo_uri { get; set; }
        public List<string> scopes { get; set; }
        public string ui_type { get; set; }

        public ClientMetadata(string name, string display_name, string logo_uri, List<string> scopes, string ui_type)
        {
            this.name = name;
            this.display_name = display_name;
            this.logo_uri = logo_uri;
            this.scopes = scopes;
            this.ui_type = ui_type;
        }
    }

    public class PaypalToken
    {
        public string scope { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string app_id { get; set; }
        public int expires_in { get; set; }
        public List<string> supported_authn_schemes { get; set; }
        public string nonce { get; set; }
        public ClientMetadata client_metadata { get; set; }
        public PaypalToken(string scope, string access_token, string token_type, string app_id, int expires_in, List<string> supported_authn_schemes, string nonce, ClientMetadata client_metadata = null)
        {
            this.scope = scope;
            this.access_token = access_token;
            this.token_type = token_type;
            this.app_id = app_id;
            this.expires_in = expires_in;
            this.supported_authn_schemes = supported_authn_schemes;
            this.nonce = nonce;
            this.client_metadata = client_metadata;
        }


    }
}

