// ***********************************************************************
// Assembly         : SampleApp
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="UserParameterModel.cs" company="SampleApp">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SampleApp.ViewModels
{
    /// <summary>
    /// Class UserParameterModel.
    /// </summary>
    public class UserParameterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserParameterModel"/> class.
        /// </summary>
        public UserParameterModel()
        {
            Page = 1;
            PageSize = 10;
        }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public int Page { get; set; }
        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }
        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>The order by.</value>
        public string OrderBy { get; set; }
        /// <summary>
        /// Gets or sets the column sort.
        /// </summary>
        /// <value>The column sort.</value>
        public string ColumnSort { get; set; }
        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>The search.</value>
        public string Search { get; set; }
        
    }
}
