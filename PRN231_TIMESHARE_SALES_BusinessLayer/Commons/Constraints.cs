using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Commons
{
    public class Constraints
    {
        #region Generic
        public const string NOT_FOUND = "Infomation Not Found!";
        public const string LOAD_FAILED = "Load Failed!";
        public const string CREATE_SUCCESS = "Create Success";
        public const string CREATE_FAILED = "Create Failed!";
        public const string DELETE_SUCCESS = "Delete Success";
        public const string DELETE_FAILED = "Delete Failed";
        public const string UPDATE_SUCCESS = "Update Success";
        public const string UPDATE_FAILED = "Update Failed!";
        public const string INFORMATION = "Information: ";
        public const string INFORMATION_EXISTED = "Information Already Existed!";
        #endregion

        #region Page
        public const int DefaultPaging = 50;
        public const int LimitPaging = 500;
        public const int DefaultPage = 1;
        #endregion

        #region Project
        public const string NOT_FOUND_PROJECT = "Project Not Found!";
        #endregion

        #region Authenticate
        public const string INVALID_VERIFICATION_CODE = "Verification code is invalid!";
        #endregion
    }
}