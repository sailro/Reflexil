
#region " Imports "
using System;
using System.Reflection;
using System.Collections.Generic;
#endregion

namespace Reflexil.Wrappers
{
	class PropertyWrapper
    {

        #region " Fields "
        private PropertyInfo m_pinfo;
        private Dictionary<String, String> m_prefixes;
        #endregion

        #region " Properties "
        public PropertyInfo PropertyInfo
        {
            get
            {
                return m_pinfo;
            }
        }

        #endregion

        #region " Methods "
        public PropertyWrapper(PropertyInfo pinfo, Dictionary<String, String> prefixes)
        {
            m_pinfo = pinfo;
            m_prefixes = prefixes; 
        }

        public override string ToString()
        {
            string result = m_pinfo.Name;
            if (m_prefixes.ContainsKey(result))
            {
                result = String.Format("{0}: {1}", m_prefixes[result], result);
            }
            return result;
        }
        #endregion

	}
}
