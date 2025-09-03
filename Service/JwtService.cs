using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Microsoft.IdentityModel.Tokens;
using StudentRegistration.Models.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentRegistration.Service
{
    public class JwtService
    {
        private readonly IConfiguration _config;
        private readonly IAmazonKeyManagementService _kms;

        public JwtService(IConfiguration config, IAmazonKeyManagementService kms)
        {
            _config = config;
            _kms = kms;
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            // 1. Create unsigned token header and payload
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var header = new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder")), SecurityAlgorithms.HmacSha256));
            var payload = new JwtPayload(claims);

            var token = new JwtSecurityToken(header, payload);
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Generate unsigned token
            var unsignedToken = tokenHandler.WriteToken(token);

            // 3. Sign it with AWS KMS
            var kmsKeyId = _config["AwsKmsKeyId"]; // e.g., arn:aws:kms:region:account-id:key/key-id
            var messageBytes = Encoding.UTF8.GetBytes(unsignedToken);

            var signRequest = new SignRequest
            {
                KeyId = kmsKeyId,
                Message = new MemoryStream(messageBytes),
                MessageType = MessageType.RAW,
                SigningAlgorithm = SigningAlgorithmSpec.RSASSA_PKCS1_V1_5_SHA_256
            };

            var response = await _kms.SignAsync(signRequest);
            var signature = response.Signature.ToArray();
            var signatureBase64 = Convert.ToBase64String(signature);

            // 4. Append the KMS signature manually
            return $"{unsignedToken}.{Base64UrlEncoder.Encode(signature)}";
        }
    }
}
    

