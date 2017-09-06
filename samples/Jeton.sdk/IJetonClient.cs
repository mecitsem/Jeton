using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Sdk
{
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.6.1.0")]
    public partial interface IJetonClient
    {
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<JetonToken> GenerateAsync(string appId, JetonIdentity identity);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<JetonToken> GenerateAsync(string appId, JetonIdentity identity, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<JetonIdentity> CheckAsync(string appId, JetonToken token);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<JetonIdentity> CheckAsync(string appId, JetonToken token, System.Threading.CancellationToken cancellationToken);

    }
}
