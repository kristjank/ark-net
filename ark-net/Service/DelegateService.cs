// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateService.cs" company="Ark">
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
using ArkNet.Messages.BaseMessages;
using ArkNet.Model.Delegate;
using ArkNet.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet.Service
{
    /// <summary>
    /// Provides functionality for requesting delegate information.
    /// </summary>
    public static class DelegateService
    {
        #region Methods

        /// <summary>
        /// Gets all delgates.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkDelegateList"/> type.</returns>
        /// 
        public static ArkDelegateList GetAll()
        {
            return GetAllAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets all delgates.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkDelegateList}"/> type.</returns>
        /// 
        public async static Task<ArkDelegateList> GetAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Delegate.GET_ALL);

            return JsonConvert.DeserializeObject<ArkDelegateList>(response);
        }

        /// <summary>
        /// Gets delgates by range.
        /// </summary>
        /// 
        /// <param name="req">The range parameters.</param>
        /// 
        /// <returns>Returns an <see cref="ArkDelegateList"/> type.</returns>
        /// 
        public static ArkDelegateList GetDelegates(ArkBaseRequest req)
        {
            return GetDelegatesAsync(req).Result;
        }

        /// <summary>
        /// Asynchronously gets delgates by range.
        /// </summary>
        /// 
        /// <param name="req">The range parameters.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkDelegateList}"/> type.</returns>
        /// 
        public async static Task<ArkDelegateList> GetDelegatesAsync(ArkBaseRequest req)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_ALL + "{0}", req.ToQuery()));

            return JsonConvert.DeserializeObject<ArkDelegateList>(response);
        }

        /// <summary>
        /// Gets a delgate by username.
        /// </summary>
        /// 
        /// <param name="username">The delegate username.</param>
        /// 
        /// <returns>Returns an <see cref="ArkDelegateResponse"/> type.</returns>
        /// 
        public static ArkDelegateResponse GetByUsername(string username)
        {
            return GetByUsernameAsync(username).Result;
        }

        /// <summary>
        /// Asynchronously gets a delgate by username.
        /// </summary>
        /// 
        /// <param name="username">The delegate username.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkDelegateResponse}"/> type.</returns>
        /// 
        public async static Task<ArkDelegateResponse> GetByUsernameAsync(string username)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_BY_USERNAME, username));

            return JsonConvert.DeserializeObject<ArkDelegateResponse>(response);
        }

        /// <summary>
        /// Gets a delegate by public key.
        /// </summary>
        /// 
        /// <param name="pubKey">The delegate public key.</param>
        /// 
        /// <returns>Returns an <see cref="ArkDelegateResponse"/> type.</returns>
        /// 
        public static ArkDelegateResponse GetByPubKey(string pubKey)
        {
            return GetByPubKeyAsync(pubKey).Result;
        }

        /// <summary>
        /// Asynchronously gets a delegate by public key.
        /// </summary>
        /// 
        /// <param name="pubKey">The delegate public key.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkDelegateResponse}"/> type.</returns>
        /// 
        public async static Task<ArkDelegateResponse> GetByPubKeyAsync(string pubKey)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_BY_PUBLIC_KEY, pubKey));

            return JsonConvert.DeserializeObject<ArkDelegateResponse>(response);
        }

        /// <summary>
        /// Gets voters of a delegate by public key.
        /// </summary>
        /// 
        /// <param name="pubKey">The delgate public key</param>
        /// 
        /// <returns>Returns an <see cref="ArkDelegateVoterList"/> type.</returns>
        /// 
        public static ArkDelegateVoterList GetVoters(string pubKey)
        {
            return GetVotersAsync(pubKey).Result;
        }

        /// <summary>
        /// Asynchronously Gets voters of a delegate by public key.
        /// </summary>
        /// 
        /// <param name="pubKey">The delgate public key</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkDelegateVoterList}"/> type.</returns>
        /// 
        public async static Task<ArkDelegateVoterList> GetVotersAsync(string pubKey)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_VOTERS, pubKey));

            return JsonConvert.DeserializeObject<ArkDelegateVoterList>(response);
        }

        /// <summary>
        /// Gets the current delgate fee.
        /// </summary>
        /// 
        /// <returns>Return an <see cref="long"/>.</returns>
        /// 
        public static long GetFee()
        {
            return GetFeeAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the current delgate fee.
        /// </summary>
        /// 
        /// <returns>Return an <see cref="Task{long}"/>.</returns>
        /// 
        public async static Task<long> GetFeeAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Delegate.GET_FEE);
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["fee"].ToString());
        }

        /// <summary>
        /// Gets amount forged by a delegate by public key.
        /// </summary>
        /// 
        /// <param name="pubKey">The delegate public key.</param>
        /// 
        /// <returns>Returns an <see cref="ArkDelegateForgedBalance"/> type.</returns>
        /// 
        public static ArkDelegateForgedBalance GetForgedByAccount(string pubKey)
        {
            return GetForgedByAccountAsync(pubKey).Result;
        }

        /// <summary>
        /// Asynchronously gets amount forged by a delegate by public key.
        /// </summary>
        /// 
        /// <param name="pubKey">The delegate public key.</param>
        /// 
        /// <returns>Returns an <see cref="Task{ArkDelegateForgedBalance}"/> type.</returns>
        /// 
        public async static Task<ArkDelegateForgedBalance> GetForgedByAccountAsync(string pubKey)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_FORGED, pubKey));

            return JsonConvert.DeserializeObject<ArkDelegateForgedBalance>(response);
        }

        /// <summary>
        /// Gets a list of the next forger delgates.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkDelegateNextForgers"/> type.</returns>
        /// 
        public static ArkDelegateNextForgers GetNextForgers()
        {
            return GetNextForgersAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets a list of the next forger delgates.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkDelegateNextForgers}"/> type.</returns>
        /// 
        public async static Task<ArkDelegateNextForgers> GetNextForgersAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Delegate.GET_NEXT_FORGERS);

            return JsonConvert.DeserializeObject<ArkDelegateNextForgers>(response);
        }

        /// <summary>
        /// Gets the total votes of a delegate by public key.
        /// </summary>
        /// 
        /// <param name="pubKey">The delegate public key.</param>
        /// 
        /// <returns>Returns an <see cref="long"/> type.</returns>
        /// 
        public static long GetTotalVoteArk(string pubKey)
        {
            var arkDelegate = GetByPubKey(pubKey);

            if (arkDelegate.Success && arkDelegate.Delegate != null)
            {
                return arkDelegate.Delegate.Vote;
            }

            return 0;
        }

        /// <summary>
        /// Asynchronously gets the total votes of a delegate by public key.
        /// </summary>
        /// 
        /// <param name="pubKey">The delegate public key.</param>
        /// 
        /// <returns>Returns an <see cref="Task{long}"/> type.</returns>
        /// 
        public async static Task<long> GetTotalVoteArkAsync(string pubKey)
        {
            var arkDelegate = await GetByPubKeyAsync(pubKey);

            if (arkDelegate.Success && arkDelegate.Delegate != null)
            {
                return arkDelegate.Delegate.Vote;
            }

            return 0;
        }

        #endregion
    }
}