﻿using Gy.HrswAuto.DataMold;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Gy.HrswAuto.Utilities
{
    public class PartConfigManager
    {
        List<PartConfig> _partConfigs;

       // bool IsInitialed = false; // 

        private PartConfigManager()
        {
            _partConfigs = new List<PartConfig>();
        }

        #region 公共方法
        public void InitPartConfigManager(string partFile)
        {
            // 从XML读出文件配置
            if (File.Exists(partFile))
            {
                LoadPartConfigFromXml(partFile);
            }
 //         IsInitialed = true;
        }

        /// <summary>
        /// 判断是否存在工件标识
        /// </summary>
        /// <param name="partId"></param>
        /// <returns></returns>
        public bool Exists(string partId)
        {
            var list = _partConfigs.FindAll(part => part.PartID.Equals(partId, StringComparison.CurrentCultureIgnoreCase));
            return list.Count != 0;
        }

        public PartConfig GetPartConfig(string partId)
        {
            PartConfig pc = _partConfigs.Find(part => string.Compare(part.PartID, partId, true) == 0);
            return pc;
        }

        public void AddPartConfig(PartConfig partConfig)
        {
            if (Exists(partConfig.PartID))
            {
                return;
            }
            _partConfigs.Add(partConfig);
        }

        /// <summary>
        /// 从Xml文件中加载零件配置
        /// </summary>
        /// <param name="sourFile"></param>
        public void LoadPartConfigFromXml(string sourFile)
        {
            using (XmlReader reader = new XmlTextReader(sourFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<PartConfig>));
                _partConfigs = serializer.Deserialize(reader) as List<PartConfig>;
            }
        }
        /// <summary>
        /// 保存零件配置到Xml文件
        /// </summary>
        /// <param name="destFile"></param>
        public void SavePartConfigToXml(string destFile)
        {
            using (XmlWriter writer = new XmlTextWriter(destFile, Encoding.UTF8))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<PartConfig>));
                serializer.Serialize(writer, _partConfigs);
            }
        } 
        #endregion

        #region 单件实例
        private static PartConfigManager _partConfigManager;

        public static PartConfigManager Instance
        {
            get {
                if (_partConfigManager == null)
                {
                    _partConfigManager = new PartConfigManager();
                }
                return _partConfigManager;
            }
        }
        //{
        //    if (_partConfigManager == null)
        //    {
        //        _partConfigManager = new PartConfigManager();
        //    }
        //    return _partConfigManager;
        //} 
        #endregion
    }
}
