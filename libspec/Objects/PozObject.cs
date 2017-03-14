using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.Objects
{
    public class PozObject
    {
        // поля записи num_refid из таблицы (lid, oid, cid etc...), на которую указывает num_kod
        public string obozn { get; set; }
        public string naimen { get; set; }
        public string descr { get; set; }
        public string kei { get; set; }
        public string marka { get; set; }
        public string gost { get; set; }
        // поля num_ заполняются первым запросом из lid по значению поля uid записи из _did
        private UInt32 m_id; // poz_id in ".as" sources id позиции в первой таблице раскрытия
        public int num_kod { get; set; }
        private UInt32 m_refid { get; set; } // refid in ".as" sources ссылка на таблицу раскрытия
        public double num_kol { get; set; }
        public double num_kfr { get; set; }
        public double num_knr { get; set; }

        public UInt32 id { get { return m_id; } }
        public UInt32 refid { get { return m_refid; } }
        public PozObject(object[] values)
        {
            m_refid = Convert.ToUInt32(values[0]);
            num_kol = Convert.ToDouble(values[1]);
            num_kod = Convert.ToInt32(values[2]);
            m_id = Convert.ToUInt32(values[3]);
            num_kfr = Convert.ToDouble(values[4]);
            num_knr = Math.Floor((num_kol / num_kfr) * 1000) / 1000;
            if (values.Length > 5)
            {
                obozn = (string)values[5];
                naimen = (string)values[6];
                descr = (string)values[7];
                kei = (string)values[8];
                marka = (string)values[9];
                gost = (string)values[10];
            }
        }
        public void FillReference(object[] values)
        {
            obozn = (string)values[0];
            naimen = (string)values[1];
            descr = (string)values[2];
            kei = (string)values[3];
            marka = (string)values[4];
            gost = (string)values[5];
        }
        public PozObject() { }
    }
}
