using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Tms.Components.Data
{
    public abstract class DataPersist
    {
        #region "Fields"
        protected string _Icon;
        protected string _UtilisateurCreation;
        protected string _UtilisateurModification;
        protected DateTime? _DateModification;
        protected DateTime? _DateCreation;

        protected object _RowVersionKey;
        protected bool _isnew = true;
        #endregion
        private DataSource _db = new DataSource();

        #region "Properties"

        [DataDisplayStyle(DefaultWidth = 20, Text = "", Visible = true)]
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }
        public bool IsNew
        {
            get { return _isnew; }
            set { _isnew = value; }
        }

        public object RowVersionKey
        {
            get { return _RowVersionKey; }
            set { _RowVersionKey = value; }
        }

        [DataDisplayStyle(DefaultWidth = 150, Text = "Creation User", Visible = false, EnglishText = "Creation User")]
        public string UtilisateurCreation
        {
            get { return _UtilisateurCreation; }
            set { _UtilisateurCreation = value; }
        }

        [DataDisplayStyle(DefaultWidth = 150, Text = "Modification User", Visible = false, EnglishText = "Modification User")]
        public string UtilisateurModification
        {
            get { return _UtilisateurModification; }
            set { _UtilisateurModification = value; }
        }

        [DataDisplayStyle(DefaultWidth = 150, Text = "Creation Date", Visible = false, EnglishText = "Creation Date")]
        public DateTime? DateCreation
        {
            get { return _DateCreation; }
            set { _DateCreation = value; }
        }

        [DataDisplayStyle(DefaultWidth = 150, Text = "Modification Date", Visible = false, EnglishText = "Modification Date")]
        public DateTime? DateModification
        {
            get { return _DateModification; }
            set { _DateModification = value; }
        }

        protected void UpdateAuditFields()
        {
            System.DateTime maintenant = System.DateTime.Now;
            if (IsNew)
            {
                _DateCreation = maintenant;
                _UtilisateurCreation = Environment.UserDomainName + "\\" + Environment.UserName;
            }
            _DateModification = maintenant;
            _UtilisateurModification = Environment.UserDomainName + "\\" + Environment.UserName;
        }


        public DataSource db()
        {

            return _db;
        }

        public void SetDataSource(DataSource dataSource)
        {
            _db = dataSource;
        }
        #endregion

        #region "Members"
        public abstract bool fnGet(object Id);

        public abstract List<DataPersist> fnSelect();

        public abstract bool fnUpdate();

        public abstract bool fnActivate();

        public abstract bool fnDeActivate();

        public override abstract string ToString();
        #endregion

    }
}
