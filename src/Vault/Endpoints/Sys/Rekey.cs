using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public class RekeyStatusResponse
    {
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("started")]
        public bool Started { get; set; }

        [JsonProperty("t")]
        public int T { get; set; }

        [JsonProperty("n")]
        public int N { get; set; }

        [JsonProperty("progress")]
        public int Progress { get; set; }

        [JsonProperty("required")]
        public int Required { get; set; }

        [JsonProperty("pgp_fingerprints")]
        public List<string> PgpFingerprints { get; set; }

        [JsonProperty("backup")]
        public bool Backup { get; set; }
    }

    public class RekeyInitRequest
    {
        [JsonProperty("secret_shares")]
        public int SecretShares { get; set; }

        [JsonProperty("secret_threshold")]
        public int SecretThreshold { get; set; }

        [JsonProperty("pgp_keys")]
        public List<string> PgpKeys { get; set; }

        [JsonProperty("backup")]
        public bool Backup { get; set; }
    }

    public class RekeyUpdateResponse
    {
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("complete")]
        public bool Complete { get; set; }

        [JsonProperty("keys")]
        public List<string> Keys { get; set; }

        [JsonProperty("keys_base64")]
        public List<string> KeysBase64 { get; set; }

        [JsonProperty("pgp_fingerprints")]
        public List<string> PgpFingerprints { get; set; }

        [JsonProperty("backup")]
        public bool Backup { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<RekeyStatusResponse> RekeyStatus(CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<RekeyStatusResponse>($"{UriPathBase}/rekey/init", ct);
        }

        public Task<RekeyStatusResponse> RekeyRecoveryKeyStatus(CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<RekeyStatusResponse>($"{UriPathBase}/rekey-recovery-key/init", ct);
        }

        public Task<RekeyStatusResponse> RekeyInit(RekeyInitRequest config, CancellationToken ct = default(CancellationToken))
        {
            return _client.Put<RekeyInitRequest, RekeyStatusResponse>($"{UriPathBase}/rekey/init", config, ct);
        }

        public Task<RekeyStatusResponse> RekeyRecoveryKeyInit(RekeyInitRequest config, CancellationToken ct = default(CancellationToken))
        {
            return _client.Put<RekeyInitRequest, RekeyStatusResponse>($"{UriPathBase}/rekey-recovery-key/init", config, ct);
        }

        public Task RekeyCancel(CancellationToken ct = default(CancellationToken))
        {
            return _client.DeleteVoid($"{UriPathBase}/rekey/init", ct);
        }

        public Task RekeyRecoveryKeyCancel(CancellationToken ct = default(CancellationToken))
        {
            return _client.DeleteVoid($"{UriPathBase}/rekey-recovery-key/init", ct);
        }

        public Task<RekeyUpdateResponse> RekeyUpdate(string shard, string nonce, CancellationToken ct = default(CancellationToken))
        {
            var request = new RekeyUpdateRequest
            {
                Key = shard,
                Nonce = nonce
            };
            return _client.Put<RekeyUpdateRequest, RekeyUpdateResponse>($"{UriPathBase}/rekey/update", request, ct);
        }

        private class RekeyUpdateRequest
        {
            [JsonProperty("key")]
            public string Key { get; set; }

            [JsonProperty("nonce")]
            public string Nonce { get; set; }
        }
    }
}

