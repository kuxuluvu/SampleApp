// ***********************************************************************
// Assembly         : SampleApp
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="ResponseUploadImageModel.cs" company="SampleApp">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Net;

namespace SampleApp.ViewModels
{
    /// <summary>
    /// Class ResponseUploadImageModel.
    /// </summary>
    public class ResponseUploadImageModel
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public DataReponse Data { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public HttpStatusCode Status { get; set; }
    }
    /// <summary>
    /// Class DataReponse.
    /// </summary>
    public class DataReponse
    {
        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        public string Link { get; set; }
    }
}
