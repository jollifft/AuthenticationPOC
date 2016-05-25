using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialTest
{
    /// <summary>
    ///     The MAD.Plugin.BaaS.Core.ITableData provides an abstraction indicating
    ///     how the system properties for a given table data model are to be serialized when
    ///     communicating with the server. 
    /// </summary>
    public interface ITableData
    {
        /// <summary>
        /// Gets or sets the unique ID for this entity.
        /// </summary>
        string Id { get; set; }
    }
}
