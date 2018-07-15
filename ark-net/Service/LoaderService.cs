﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoaderService.cs" company="Ark">
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
using ArkNet.Model.Loader;
using ArkNet.Utils;
using Newtonsoft.Json;

namespace ArkNet.Service
{
    /// <summary>
    /// Provides functionality for requesting loader information.
    /// </summary>
    /// 
    public class LoaderService : BaseService
    {
        public LoaderService(NetworkApi networkApi, LoggingApi logger)
            : base(networkApi, logger)
        {
        }

        #region Methods

        /// <summary>
        /// Gets the loader status.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkLoaderStatus"/> type.</returns>
        /// 
        public ArkLoaderStatus GetStatus()
        {
            return GetStatusAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the loader status.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkLoaderStatus}"/> type.</returns>
        /// 
        public async Task<ArkLoaderStatus> GetStatusAsync()
        {
            try
            {
                _logger.Info("Getting loader status");

                var response = await _networkApi.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_STATUS).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<ArkLoaderStatus>(response);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Gets the loader sync status.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkLoaderStatusSync"/> type.</returns>
        /// 
        public ArkLoaderStatusSync GetSyncStatus()
        {
            return GetSyncStatusAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the loader sync status.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkLoaderStatusSync}"/> type.</returns>
        /// 
        public async Task<ArkLoaderStatusSync> GetSyncStatusAsync()
        {
            try
            {
                _logger.Info("Getting sync status");

                var response = await _networkApi.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_SYNC_STATUS).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<ArkLoaderStatusSync>(response);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// Gets the loader auto configure parameters.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="ArkLoaderNetworkResponse"/> type.</returns>
        /// 
        public ArkLoaderNetworkResponse GetAutoConfigureParameters()
        {
            return GetAutoConfigureParametersAsync().Result;
        }

        /// <summary>
        /// Asynchronously gets the loader auto configure parameters.
        /// </summary>
        /// 
        /// <returns>Returns an <see cref="Task{ArkLoaderNetworkResponse}"/> type.</returns>
        /// 
        public async Task<ArkLoaderNetworkResponse> GetAutoConfigureParametersAsync()
        {
            try
            {
                _logger.Info("Getting autoconfig parameters");

                var response = await _networkApi.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_AUTO_CONFIGURE).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<ArkLoaderNetworkResponse>(response);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
                throw e;
            }
        }

        #endregion
    }
}