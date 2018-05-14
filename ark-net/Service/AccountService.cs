// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountService.cs" company="Ark">
//   MIT License
//   // 
//   // Copyright (c) 2017 Kristjan Košič
//   // 
//   // Permission is hereby granted, free of charge, to any person obtaining a copy
//   // of this software and associated documentation files (the "Software"), to deal
//   // in the Software without restriction, including without limitation the rights
//   // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   // copies of the Software, and to permit persons to whom the Software is
//   // furnished to do so, subject to the following conditions:
//   // 
//   // The above copyright notice and this permission notice shall be included in all
//   // copies or substantial portions of the Software.
//   // 
//   // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   // SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Model.Account;
using ArkNet.Model.Delegate;
using ArkNet.Utils;
using NBitcoin;
using Newtonsoft.Json;

namespace ArkNet.Service
{
    #region Classes

    /// <summary>
    /// Provides functionality for requesting account information from a peer.
    /// </summary>
    /// 
    public class AccountService : BaseService
    {
        public AccountService(NetworkApi networkApi, LoggingApi logger)
            :base(networkApi, logger)
        {
        }

        #region Methods

        /// <summary>
        /// Gets the account information of an address.
        /// </summary>
        /// 
        /// <param name="address">A valid ARK as a <see cref="string"/>.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="ArkAccountResponse"/> type.</returns>
        /// 
        public ArkAccountResponse GetByAddress(string address)
        {
            return GetByAddressAsync(address).Result;
        }

        /// <summary>
        /// Asynchronously gets the account information of an address.
        /// </summary>
        /// 
        /// <param name="address">A valid ARK address as a <see cref="string"/>.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="ArkAccountResponse"/> type.</returns>
        /// 
        public async Task<ArkAccountResponse> GetByAddressAsync(string address)
        {
            try
            {
                var response = await _networkApi.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_ACCOUNT, address)).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<ArkAccountResponse>(response);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Gets the balance of an account.
        /// </summary>
        /// 
        /// <param name="address">A valid ARK address as a <see cref="string"/>.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="ArkAccountBalance"/> type.</returns>
        /// 
        public ArkAccountBalance GetBalance(string address)
        {
            return GetBalanceAsync(address).Result;
        }

        /// <summary>
        /// Asynchronously gets the balance of an account.
        /// </summary>
        /// 
        /// <param name="address">A valid ARK address as a <see cref="string"/>.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="ArkAccountBalance"/> type.</returns>
        /// 
        public async Task<ArkAccountBalance> GetBalanceAsync(string address)
        {
            try
            {
                var response = await _networkApi.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_BALANCE, address)).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<ArkAccountBalance>(response);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Gets the delegates of an account.
        /// </summary>
        /// 
        /// <param name="address">A valid ARK address as a <see cref="string"/>.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="ArkDelegateList"/> type.</returns>
        /// 
        public ArkDelegateList GetDelegates(string address)
        {
            return GetDelegatesAsync(address).Result;
        }

        /// <summary>
        /// Asynchronously gets the delegates of an account by the account address.
        /// </summary>
        /// 
        /// <param name="address">A valid ARK address as a <see cref="string"/>.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="Task{ArkDelegateList}"/> type.</returns>
        /// 
        public async Task<ArkDelegateList> GetDelegatesAsync(string address)
        {
            try
            {
                var response = await _networkApi.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_DELEGATES, address)).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<ArkDelegateList>(response);
            }
            catch(Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
}

        /// <summary>
        /// Gets a list of top accounts.
        /// </summary>
        /// 
        /// <param name="limit">An unsigned integer that specifies the maximum number of records.</param>
        /// 
        /// <param name="recordsToSkip">An unsigned integer that specified the number of records to skip.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="ArkAccountTopList"/> type.</returns>
        /// 
        public ArkAccountTopList GetTop(int? limit, int? recordsToSkip)
        {
            return GetTopAsync(limit, recordsToSkip).Result;
        }

        /// <summary>
        /// Asynchronously gets a list of top accounts.
        /// </summary>
        /// 
        /// <param name="limit">An unsigned integer that specifies the maximum number of records.</param>
        /// 
        /// <param name="recordsToSkip">An unsigned integer that specified the number of records to skip.</param>
        /// 
        /// <returns>Returns an instance of the <see cref="Task{ArkAccountTopList}"/> type.</returns>
        /// 
        public async Task<ArkAccountTopList> GetTopAsync(int? limit, int? recordsToSkip)
        {
            try
            {
                var response =
                    await _networkApi.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_TOP_ACCOUNTS, limit.HasValue ? limit : 100, recordsToSkip.HasValue ? recordsToSkip : 0)).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<ArkAccountTopList>(response);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Generates a passphrase
        /// </summary>
        /// <returns>String containinig the passphrase</returns>
        public string GeneratePassphrase()
        {
            try
            {
                return new Mnemonic(Wordlist.English, WordCount.Twelve).ToString();
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }
    }
    #endregion
}
#endregion