using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.View.Objects
{
    public class PozObject
    {
        // поля записи num_refid из таблицы (lid, oid, cid etc...), на которую указывает num_kod
        public string obozn { get; set; }
        public string naimen { get; set; }
        public string descr { get; set; }
        public string m_kei;
        public string keiString { get { return Utils.GetKeiNaimen(m_kei); }}
        public string marka { get; set; }
        public string gost { get; set; }
        // поля num_ заполняются первым запросом из lid по значению поля uid записи из _did
        private UInt32 m_id; // poz_id in ".as" sources id позиции в первой таблице раскрытия
        public int num_kod { get; set; }
        private UInt32 m_refid { get; set; } // refid in ".as" sources ссылка на таблицу раскрытия
        private double m_num_kol;
        private double m_num_kfr;
        private double m_num_knr;
        public double num_kol { get { return m_num_kol; } set { m_num_kol = value; set_num_knr(); } }
        public double num_kfr { get { return m_num_kfr; } set { m_num_kfr = value; set_num_knr(); } }
        public string num_knr { get { return double.IsNaN(m_num_knr) ? "-" : m_num_knr.ToString(); }}

        public UInt32 id { get { return m_id; } }
        public UInt32 refid { get { return m_refid; } }
        public PozObject(object[] values)
        {
            m_refid = Convert.ToUInt32(values[0]);
            m_num_kol = Convert.ToDouble(values[1]);
            num_kod = Convert.ToInt32(values[2]);
            m_id = Convert.ToUInt32(values[3]);
            m_num_kfr = Convert.ToDouble(values[4]);
            set_num_knr();
            if (values.Length > 5)
            {
                obozn = (string)values[5];
                naimen = (string)values[6];
                descr = (string)values[7];
                m_kei = (string)values[8];
                marka = (string)values[9];
                gost = (string)values[10];
            }
        }
        private void set_num_knr()
        {
            m_num_knr = Math.Floor((m_num_kol / m_num_kfr) * 1000) / 1000;
        }
        public void FillReference(object[] values)
        {
            obozn = (string)values[0];
            naimen = (string)values[1];
            descr = (string)values[2];
            m_kei = (string)values[3];
            marka = (string)values[4];
            gost = (string)values[5];
        }
        public PozObject Clone()
        {
            PozObject o = new PozObject();
            o.m_refid = m_refid;
            o.num_kol = m_num_kol;
            o.num_kod = num_kod;
            o.m_id = m_id;
            o.num_kfr = m_num_kfr;
            o.m_num_knr = m_num_knr;
            o.obozn = obozn;
            o.naimen = naimen;
            o.descr = descr;
            o.m_kei = m_kei;
            o.marka = marka;
            o.gost = gost;
            return o;
        }
        public void SetRootId(UInt32 id)
        {
            m_id = id;
        }
        public PozObject() { }
    }
}
